using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GuardAI : EnemyAI
{
    private float idleTimeout = 15;
    private bool idleTimeoutEnded = true;

    protected override void HandleStates()
    {
        Vector3 pos;

        switch (state)
        {
            case States.Idle:
                agent.enabled = false;
                if (ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(Random.Range(1, 5), States.Patrolling));
                StartCoroutine(IdleTimeout());
                break;

            case States.Patrolling:
                playerStillDetected = false;
                if (!agent.isActiveAndEnabled)
                {
                    agent.enabled = true;
                    if (waypointIndex == -1) waypointIndex = 0;
                    agent.SetDestination(patrollingWaypoints[waypointIndex].position);
                }

                agent.speed = baseSpeed * patrollingSpeedModifier;

                if (agent.isActiveAndEnabled && agent.remainingDistance < .2f)
                {
                    if (idleTimeoutEnded && Random.Range(0, 100) < 50)
                        if (ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(0, States.Idle));
                    else
                    {
                        if (waypointIndex == patrollingWaypoints.Length - 1) waypointIndex = -1;
                        waypointIndex += 1;

                        agent.SetDestination(patrollingWaypoints[waypointIndex].position);
                    }
                }

                if (DetectPlayer(false, out pos) && ChangeStateCoroutine == null)
                {
                    lastKnownPlayerLocation = pos;
                    ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(1.5f, States.Targeting));
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
                        agent.enabled = false;
                        playerController.Teleport(playerSpawn.position, playerSpawn.rotation);
                        transform.position = patrollingWaypoints[0].position;
                        lastKnownPlayerLocation = Vector3.zero;
                        if (ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(0f, States.Patrolling));
                        return;
                    }

                    lastKnownPlayerLocation = pos;
                    if (ChangeStateCoroutine != null)
                    {
                        StopCoroutine(ChangeStateCoroutine);
                        ChangeStateCoroutine = null;
                    }
                }

                if (agent.isActiveAndEnabled && agent.destination != lastKnownPlayerLocation) agent.SetDestination(lastKnownPlayerLocation);

                if (agent.isActiveAndEnabled && agent.remainingDistance < .5)
                {
                    if (ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(2f, States.Patrolling));
                }
                break;
        }
    }
    IEnumerator IdleTimeout()
    {
        idleTimeoutEnded = false;
        yield return new WaitForSeconds(idleTimeout);
        idleTimeoutEnded = true;
    }
}
