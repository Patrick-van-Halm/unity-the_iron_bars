using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyAI : MonoBehaviour
{
    public enum States
    {
        Idle,
        Patrolling,
        Targeting
    }

    public Transform[] patrollingWaypoints;
    public Transform spotterOrigin;
    public Transform playerSpawn;
    public LayerMask detectableLayers;
    public float maxVisibleAngleX;
    public float maxDoorDetectionAngleX;
    public float spotDistance = 5;
    public float hearingDistance = 5;
    public float patrollingSpeedModifier = .75f;

    [Header("Audio")]
    public AudioClip soundDetectionClip;
    public AudioClip[] footstepClips;

    protected NavMeshAgent agent;
    protected States state;
    protected States prevState;

    protected int waypointIndex = -1;

    protected Transform player;
    protected PlayerController playerController;
    protected CharacterController playerCharacter;

    protected Vector3 lastKnownPlayerLocation;

    protected Coroutine ChangeStateCoroutine;

    protected float baseSpeed;

    private Door openedDoor;

    protected AudioSource audioSource;
    protected Animator animator;
    private Coroutine footstepSounds;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        state = States.Patrolling;
        playerController = FindObjectOfType<PlayerController>();
        player = playerController.playerCamera.transform;
        playerCharacter = playerController.GetComponent<CharacterController>();

        audioSource.volume = .8f;
        baseSpeed = agent.speed;
    }

    protected virtual void Update()
    {
        HandleStates();
        HandleDoorInteractions();

        // Walking sound
        if(agent.velocity.magnitude > 0 && footstepSounds == null)
        {
            footstepSounds = StartCoroutine(PlayFootstepSounds());
        }

        // Update previous state
        prevState = state;
    }

    protected abstract void HandleStates();

    private void HandleDoorInteractions()
    {
        // Open closed doors in front of enemy.
        if (agent.enabled && Physics.Raycast(spotterOrigin.position, transform.forward, out RaycastHit hit, .35f))
        {
            if (hit.collider.CompareTag("Door"))
            {
                openedDoor = hit.collider.GetComponent<Door>();
                if (openedDoor && openedDoor.State == Door.States.Closed) openedDoor.SetDoor(true);
            }
        }

        // Close door if enemy passed the door and the initial state was closed.
        if (openedDoor)
        {
            var enemyToOpenedDoorAngleX = Vector3.Angle(-spotterOrigin.forward, new Vector3((openedDoor.transform.position - spotterOrigin.position).x, 0, (openedDoor.transform.position - spotterOrigin.position).z));

            if (enemyToOpenedDoorAngleX < maxDoorDetectionAngleX && enemyToOpenedDoorAngleX > -maxDoorDetectionAngleX && new Vector3((openedDoor.transform.position - spotterOrigin.position).x, 0, (openedDoor.transform.position - spotterOrigin.position).z).magnitude > 1f)
            {
                if (openedDoor.State == Door.States.Open && openedDoor.initialState == Door.States.Closed) openedDoor.SetDoor(false);
                openedDoor = null;
            }
        }
    }

    protected bool DetectPlayer(bool isAllowedToHear, out Vector3 position)
    {
        bool canHearPlayer = Physics.OverlapSphere(spotterOrigin.position, hearingDistance).Any(c => c.CompareTag("Player") && !playerController.isCrouched && playerCharacter.velocity.magnitude > 0f);
        float enemyToPlayerAngleX = Vector3.Angle(spotterOrigin.forward, new Vector3((player.position - spotterOrigin.position).x, 0, (player.position - spotterOrigin.position).z));
        if ((enemyToPlayerAngleX > maxVisibleAngleX || enemyToPlayerAngleX < -maxVisibleAngleX) && !canHearPlayer)
        {
            position = Vector3.zero;
            return false;
        }

        if (Physics.Raycast(spotterOrigin.position, player.position - spotterOrigin.position, out RaycastHit hitInfo, spotDistance, detectableLayers) || (isAllowedToHear && canHearPlayer))
        {
            if ((hitInfo.collider && hitInfo.collider.CompareTag("Player")) || (isAllowedToHear && canHearPlayer))
            {
                position = player.position;
                return true;
            }
        }

        position = Vector3.zero;
        return false;
    }

    private IEnumerator PlayFootstepSounds()
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        audioSource.clip = footstepClips.Random();
        audioSource.Play();
        yield return new WaitForSeconds(state == States.Targeting ? .3f : .5f);
        footstepSounds = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        var enemyToPlayerAngleX = Vector3.Angle(spotterOrigin.forward, new Vector3((player.position - spotterOrigin.position).x, 0, (player.position - spotterOrigin.position).z));
        if (enemyToPlayerAngleX < maxVisibleAngleX && enemyToPlayerAngleX > -maxVisibleAngleX) 
        {
            //print(enemyToPlayerAngleX);
            Gizmos.color = Color.green;
        }

        Gizmos.DrawLine(spotterOrigin.position, player.position);

        Gizmos.color = Color.green;

        if (openedDoor)
        {
            Gizmos.color = Color.red;
            var enemyToOpenedDoorAngleX = Vector3.Angle(-spotterOrigin.forward, new Vector3((openedDoor.transform.position - spotterOrigin.position).x, 0, (openedDoor.transform.position - spotterOrigin.position).z));
            if (enemyToOpenedDoorAngleX < maxDoorDetectionAngleX && enemyToOpenedDoorAngleX > -maxDoorDetectionAngleX)
            {
                //print(enemyToPlayerAngleX);
                Gizmos.color = Color.green;
            }

            Gizmos.DrawLine(spotterOrigin.position, openedDoor.transform.position);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(spotterOrigin.position, spotterOrigin.position + (Quaternion.Euler(0, maxDoorDetectionAngleX, 0) * -spotterOrigin.forward * spotDistance));
            Gizmos.DrawLine(spotterOrigin.position, spotterOrigin.position + (Quaternion.Euler(0, -maxDoorDetectionAngleX, 0) * -spotterOrigin.forward * spotDistance));
        }

        Gizmos.DrawLine(spotterOrigin.position, spotterOrigin.position + (Quaternion.Euler(0, maxVisibleAngleX, 0) * spotterOrigin.forward * spotDistance));
        Gizmos.DrawLine(spotterOrigin.position, spotterOrigin.position + (Quaternion.Euler(0, -maxVisibleAngleX, 0) * spotterOrigin.forward * spotDistance));

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, agent.destination);
        //print((player.position - transform.position).normalized);
        //print(Vector3.Angle(SpotterOrigin.forward, new Vector3((player.position - SpotterOrigin.position).normalized.x, 0, 0)));
    }

    protected IEnumerator ChangeStateAfterSeconds(float seconds, States newState)
    {
        if (ChangeStateCoroutine != null) yield break;
        yield return new WaitForSeconds(seconds);
        state = newState;
        ChangeStateCoroutine = null;
    }
}
