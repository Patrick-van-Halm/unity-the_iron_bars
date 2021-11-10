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
    private Coroutine idleTimeoutCoro;

    protected override void HandleStates()
    {
        Vector3 pos;

        switch (state)
        {
            case States.Idle:
                agent.enabled = false;
                if (ChangeStateCoroutine == null && patrollingWaypoints.Length > 1) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(Random.Range(1, 5), States.Patrolling));
                if (idleTimeoutCoro == null && patrollingWaypoints.Length > 1) idleTimeoutCoro = StartCoroutine(IdleTimeout());
                if (DetectPlayer(true, out pos) && ChangeStateCoroutine == null)
                {
                    lastKnownPlayerLocation = pos;
                    ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(.5f, States.Targeting));
                    if (animator) animator.SetTrigger("PlayerDetected");
                    if (audioSource && soundDetectionClip) audioSource.PlayOneShot(soundDetectionClip);
                }
                break;

            case States.Patrolling:
                if (!agent.isActiveAndEnabled)
                {
                    agent.enabled = true;
                    if (waypointIndex == -1) waypointIndex = 0;
                    agent.SetDestination(patrollingWaypoints[waypointIndex].position);
                }

                agent.speed = baseSpeed * patrollingSpeedModifier;

                if (DetectPlayer(patrollingWaypoints.Length == 1, out pos) && ChangeStateCoroutine == null)
                {
                    agent.SetDestination(transform.position);
                    lastKnownPlayerLocation = pos;
                    ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(.5f, States.Targeting));
                    if (animator) animator.SetTrigger("PlayerDetected");
                    if (audioSource && soundDetectionClip) audioSource.PlayOneShot(soundDetectionClip);
                }

                if (agent.isActiveAndEnabled && agent.remainingDistance < .5f && ChangeStateCoroutine == null)
                {
                    if ((patrollingWaypoints.Length > 1 && idleTimeoutEnded && Random.Range(0, 100) < 50) || (patrollingWaypoints.Length == 1 && Vector3.Distance(transform.position, patrollingWaypoints[0].position) < .3f))
                    {
                        if (ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(0, States.Idle));
                    }
                    else
                    {
                        waypointIndex += 1;
                        if (waypointIndex >= patrollingWaypoints.Length) waypointIndex = 0;

                        agent.SetDestination(patrollingWaypoints[waypointIndex].position);
                    }
                }
                break;

            case States.Targeting:
                if (!agent.isActiveAndEnabled) agent.enabled = true;
                agent.speed = baseSpeed;

                if (DetectPlayer(true, out pos))
                {
                    if (Vector3.Distance(player.position, spotterOrigin.position) < .8)
                    {
                        agent.enabled = false;
                        playerController.Teleport(playerTeleportTo.position, playerTeleportTo.rotation);
                        transform.position = patrollingWaypoints[0].position;
                        lastKnownPlayerLocation = Vector3.zero;
                        CloseOpenedDoor();
                        if (ChangeStateCoroutine == null && patrollingWaypoints.Length > 0) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(0f, States.Patrolling));
                        else if(ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(0f, States.Idle));
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
                    if (ChangeStateCoroutine == null && patrollingWaypoints.Length > 0) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(2f, States.Patrolling));
                    else if(ChangeStateCoroutine == null) ChangeStateCoroutine = StartCoroutine(ChangeStateAfterSeconds(0f, States.Idle));
                }
                break;
        }
    }
    IEnumerator IdleTimeout()
    {
        idleTimeoutEnded = false;
        yield return new WaitForSeconds(idleTimeout);
        idleTimeoutEnded = true;
        idleTimeoutCoro = null;
    }
}
