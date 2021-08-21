using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f;

    [SerializeField] float patrolSpeed = 1.5f;
    [SerializeField] float aggroSpeed = 5.0f;
    Fighter fighter;
    GameObject player;
    Health health;
    Mover mover;

    Vector3 guardPosition;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    int currentWaypointIndex = 0;



    void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
    void Start()
    {
      fighter = GetComponent<Fighter>();
      health = GetComponent<Health>();
      mover = GetComponent<Mover>();
      player = GameObject.FindWithTag("Player");
      guardPosition = transform.position;
    }

    void Update()
    {
      if (health.IsDead())
      {
        return;
      }

      if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
      {
        
        AttackBehavior();
      }
      else if (timeSinceLastSawPlayer < suspicionTime)
      {
        SuspicionBehavior();
      }
      else
      {
        PatrolBehavior();
      }
      UpdateTimers();
    }

    private void UpdateTimers()
    {
      timeSinceLastSawPlayer += Time.deltaTime;
      timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    private void PatrolBehavior()
    {
      Vector3 nextPosition = guardPosition;
      if (patrolPath != null)
      {
        if (AtWaypoint())
        {
          timeSinceArrivedAtWaypoint = 0;  
          GetComponent<NavMeshAgent>().speed = patrolSpeed;
          CycleWaypoint();
        }
        nextPosition = GetCurrentWaypoint();
      }
      if(timeSinceArrivedAtWaypoint > waypointDwellTime)
      mover.StartMoveAction(nextPosition);
    }




    private Vector3 GetCurrentWaypoint()
    {
      return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
      currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }
    private bool AtWaypoint()
    {
      float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
      return distanceToWayPoint < waypointTolerance;
    }

    private void SuspicionBehavior()
    {
      GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void AttackBehavior()
    {
      timeSinceLastSawPlayer = 0;  
      GetComponent<NavMeshAgent>().speed = aggroSpeed;
      fighter.Attack(player);
    }

    private bool InAttackRangeOfPlayer()
    {

      float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
      return distanceToPlayer < chaseDistance;
    }
  }
}
