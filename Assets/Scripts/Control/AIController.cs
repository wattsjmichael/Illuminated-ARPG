using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;


namespace RPG.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance = 5f;
    Fighter fighter;
    GameObject player;
    Health health;

/// <summary>
/// Start is called on the frame when a script is enabled just before
/// any of the Update methods is called the first time.
/// </summary>
void Start()
{
    fighter = GetComponent<Fighter>();
    health = GetComponent<Health>();
    player = GameObject.FindWithTag("Player");
}

    void Update()
    {
        if(health.IsDead())
        {
            return;
        }
      
      if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
      {
        GetComponent<Fighter>().Attack(player);
      }
      else
      {
          fighter.Cancel();
      }
    }

      private bool InAttackRangeOfPlayer()
      {

      float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
      return distanceToPlayer < chaseDistance;
      }
    }
  }
