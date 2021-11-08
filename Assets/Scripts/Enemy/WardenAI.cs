using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class WardenAI : EnemyAI
{
    public Transform officeWaypoint;

    private bool isWalkingToOffice;

    protected override void HandleStates()
    {
        Vector3 pos;

        switch (state)
        {
            case States.Idle:
                agent.enabled = false;
                isWalkingToOffice = false;
                if(ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(Random.Range(20, 30), States.Patrolling));

                if (DetectPlayer(false, out pos))
                {
                    agent.enabled = true;
                    lastKnownPlayerLocation = pos;
                    if (ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(.5f, States.Targeting));
                }
                break;

            case States.Patrolling:
                playerStillDetected = false;
                if (!agent.isActiveAndEnabled) agent.enabled = true;

                agent.speed = baseSpeed * patrollingSpeedModifier;

                if (agent.isActiveAndEnabled && agent.remainingDistance < .2f)
                {
                    if (isWalkingToOffice && ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(0, States.Idle));
                    else
                    {
                        if (waypointIndex == patrollingWaypoints.Length - 1)
                        {
                            waypointIndex = -1;
                            isWalkingToOffice = true;
                            agent.SetDestination(officeWaypoint.position);
                        }
                        waypointIndex += 1;

                        agent.SetDestination(patrollingWaypoints[waypointIndex].position);
                    }
                }

                if (DetectPlayer(false, out pos))
                {
                    lastKnownPlayerLocation = pos;
                    if (ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(.5f, States.Targeting));
                }
                break;

            case States.Targeting:
                if (!playerStillDetected && state != prevState)
                {
                    if (animator) animator.SetTrigger("PlayerDetected");
                    if (audioSource && soundDetectionClip) audioSource.PlayOneShot(soundDetectionClip);
                    playerStillDetected = true;
                }

                agent.speed = baseSpeed;

                if (DetectPlayer(true, out pos))
                {
                    if (Vector3.Distance(player.position, spotterOrigin.position) < .8)
                    {
                        agent.SetDestination(patrollingWaypoints[waypointIndex].position);
                        playerController.Teleport(playerSpawn.position, playerSpawn.rotation);
                        agent.enabled = false;
                        transform.position = patrollingWaypoints[waypointIndex].position;
                        agent.enabled = true;
                        if (ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(0f, States.Patrolling));
                        break;
                    }

                    lastKnownPlayerLocation = pos;
                    if (ChangeStateCoroutine != null)
                    {
                        StopCoroutine(ChangeStateCoroutine);
                        ChangeStateCoroutine = null;
                    }
                }

                if (agent.destination != lastKnownPlayerLocation) agent.SetDestination(lastKnownPlayerLocation);

                if (agent.isActiveAndEnabled && agent.remainingDistance < .5)
                {
                    if (ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(2f, States.Patrolling));
                }
                break;
        }
    }
}
