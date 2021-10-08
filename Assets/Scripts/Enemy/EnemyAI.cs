using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        state = States.Idle;
        player = FindObjectOfType<PlayerController>().playerCamera.transform;
    }

    void Update()
    {
        RaycastHit hitInfo;

        switch (state)
        {
            case States.Idle:
                ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(2, States.Patrolling));
                break;

            case States.Patrolling:
                if (agent.isActiveAndEnabled && agent.remainingDistance < 1)
                {
                    if (waypointIndex == patrollingWaypoints.Length - 1) waypointIndex = -1;
                    waypointIndex += 1;
                    agent.SetDestination(patrollingWaypoints[waypointIndex].position);
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
}
