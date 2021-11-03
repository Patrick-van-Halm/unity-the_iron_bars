using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public enum States
    {
        Idle,
        Patrolling,
        Targeting
    }

    public Transform[] patrollingWaypoints;
    public Transform spotterOrigin;
    public float maxVisibleAngleX;
    public float maxDoorDetectionAngleX;
    public float spotDistance = 5;

    [Header("Audio")]
    public AudioClip soundDetectionClip;

    private NavMeshAgent agent;
    private States state;
    private States prevState;

    private int waypointIndex = -1;

    private Transform player;
    private Vector3 lastKnownPlayerLocation;

    private Coroutine ChangeStateCoroutine;
    private float idleTimeout = 15;
    private bool idleTimeoutEnded = true;

    private Door openedDoor;

    private AudioSource audioSource;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        state = States.Patrolling;
        player = FindObjectOfType<PlayerController>().playerCamera.transform;
    }

    void Update()
    {
        RaycastHit hitInfo;
        float enemyToPlayerAngleX;

        switch (state)
        {
            case States.Idle:
                agent.enabled = false;
                ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(Random.Range(1, 5), States.Patrolling));
                StartCoroutine(IdleTimeout());
                break;

            case States.Patrolling:
                if (!agent.isActiveAndEnabled) agent.enabled = true;

                if (agent.isActiveAndEnabled && agent.remainingDistance < .2f)
                {
                    if (idleTimeoutEnded && Random.Range(0, 100) < 50) 
                        ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(0, States.Idle));
                    else
                    {
                        if (waypointIndex == patrollingWaypoints.Length - 1) waypointIndex = -1;
                        waypointIndex += 1;

                        agent.SetDestination(patrollingWaypoints[waypointIndex].position);
                    }
                }

                enemyToPlayerAngleX = Vector3.Angle(spotterOrigin.forward, new Vector3((player.position - spotterOrigin.position).x, 0, (player.position - spotterOrigin.position).z));
                if (enemyToPlayerAngleX > maxVisibleAngleX || enemyToPlayerAngleX < -maxVisibleAngleX) break;

                if(Physics.Raycast(spotterOrigin.position, player.position - spotterOrigin.position, out hitInfo, spotDistance))
                {
                    if (hitInfo.collider.CompareTag("Player")) 
                    {
                        lastKnownPlayerLocation = hitInfo.collider.transform.position;
                        ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(.5f, States.Targeting));
                    }
                }
                break;

            case States.Targeting:
                if (prevState != state)
                {
                    if (animator) animator.SetTrigger("PlayerDetected");
                    if (audioSource && soundDetectionClip) audioSource.PlayOneShot(soundDetectionClip);
                }

                if (Physics.Raycast(spotterOrigin.position, player.position - spotterOrigin.position, out hitInfo, spotDistance))
                {
                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        enemyToPlayerAngleX = Vector3.Angle(spotterOrigin.forward, new Vector3((player.position - spotterOrigin.position).x, 0, (player.position - spotterOrigin.position).z));
                        if (enemyToPlayerAngleX < maxVisibleAngleX && enemyToPlayerAngleX > -maxVisibleAngleX)
                        {
                            if (hitInfo.distance < .2f) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                            lastKnownPlayerLocation = hitInfo.collider.transform.position;
                            if (ChangeStateCoroutine != null)
                            {
                                StopCoroutine(ChangeStateCoroutine);
                                ChangeStateCoroutine = null;
                            }
                        }
                    }
                }                

                if (agent.destination != lastKnownPlayerLocation) agent.SetDestination(lastKnownPlayerLocation);

                if (agent.isActiveAndEnabled && agent.remainingDistance < 1)
                {
                    ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(2f, States.Patrolling));
                }
                break;
        }

        // Open closed doors in front of enemy.
        if(agent.enabled && Physics.Raycast(spotterOrigin.position, transform.forward, out RaycastHit hit, .35f))
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

        // Update previous state
        prevState = state;
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

    IEnumerator ChangeStateAfterSeconds(float seconds, States newState)
    {
        if (ChangeStateCoroutine != null) yield break;
        yield return new WaitForSeconds(seconds);
        state = newState;
        ChangeStateCoroutine = null;
    }

    IEnumerator IdleTimeout()
    {
        idleTimeoutEnded = false;
        yield return new WaitForSeconds(idleTimeout);
        idleTimeoutEnded = true;
    }
}
