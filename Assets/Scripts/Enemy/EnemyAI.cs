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
    public Transform SpotterOrigin;
    public float maxVisibleAngleX;
    public float spotDistance = 5;

    private NavMeshAgent agent;
    private States state;

    private int waypointIndex = -1;

    private Transform player;
    private Vector3 lastKnownPlayerLocation;

    private Coroutine ChangeStateCoroutine;
    private float idleTimeout = 15;
    private bool idleTimeoutEnded = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        state = States.Patrolling;
        player = FindObjectOfType<PlayerController>().playerCamera.transform;
    }

    void Update()
    {
        RaycastHit hitInfo;

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

                var enemyToPlayerAngleX = Vector3.Angle(SpotterOrigin.forward, new Vector3((player.position - SpotterOrigin.position).x, 0, (player.position - SpotterOrigin.position).z));
                if (enemyToPlayerAngleX > maxVisibleAngleX || enemyToPlayerAngleX < -maxVisibleAngleX) break;

                if(Physics.Raycast(SpotterOrigin.position, player.position - SpotterOrigin.position, out hitInfo, spotDistance))
                {
                    //print(hitInfo.collider.name);
                    if (hitInfo.collider.CompareTag("Player")) 
                    {
                        lastKnownPlayerLocation = hitInfo.collider.transform.position;
                        ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(.5f, States.Targeting));
                    }
                }
                break;

            case States.Targeting:
                if (Physics.Raycast(SpotterOrigin.position, player.position - SpotterOrigin.position, out hitInfo, spotDistance))
                {
                    if (hitInfo.collider.CompareTag("Player"))
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

                if (agent.destination != lastKnownPlayerLocation) agent.SetDestination(lastKnownPlayerLocation);

                if (agent.isActiveAndEnabled && agent.remainingDistance < 1)
                {
                    ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(2f, States.Patrolling));
                }
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        var enemyToPlayerAngleX = Vector3.Angle(SpotterOrigin.forward, new Vector3((player.position - SpotterOrigin.position).x, 0, (player.position - SpotterOrigin.position).z));
        if (enemyToPlayerAngleX < maxVisibleAngleX && enemyToPlayerAngleX > -maxVisibleAngleX) 
        {
            //print(enemyToPlayerAngleX);
            Gizmos.color = Color.green;
        }

        Gizmos.DrawLine(SpotterOrigin.position, player.position);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(SpotterOrigin.position, SpotterOrigin.position + (Quaternion.Euler(0, maxVisibleAngleX, 0) * SpotterOrigin.forward * spotDistance));
        Gizmos.DrawLine(SpotterOrigin.position, SpotterOrigin.position + (Quaternion.Euler(0, -maxVisibleAngleX, 0) * SpotterOrigin.forward * spotDistance));

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
