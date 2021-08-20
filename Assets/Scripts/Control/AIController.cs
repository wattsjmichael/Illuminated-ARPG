﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;


namespace RPG.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float suspicionTime = 3f;
    Fighter fighter;
    GameObject player;
    Health health;
    Mover mover;

    Vector3 guardPosition;
    float timeSinceLastSawPlayer = Mathf.Infinity;



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
        timeSinceLastSawPlayer = 0;
        AttackBehavior();
      }
      else if (timeSinceLastSawPlayer < suspicionTime)
      {
        SuspicionBehavior();
      }
      else
      {
        GuardBehavior();
      }
      timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void GuardBehavior()
    {
      mover.StartMoveAction(guardPosition);
    }

    private void SuspicionBehavior()
    {
      GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void AttackBehavior()
    {
      fighter.Attack(player);
    }

    private bool InAttackRangeOfPlayer()
    {

      float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
      return distanceToPlayer < chaseDistance;
    }
  }
}
