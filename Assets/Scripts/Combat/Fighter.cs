using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{

  public class Fighter : MonoBehaviour, IAction
  {




    [SerializeField] Transform rightHandTransform = null;
    [SerializeField] Transform leftHandTransform = null;

    [SerializeField] weapon weapon = null;
    weapon currentWeapon = null;
    Health target;

    float timeSinceLastAttack = 0;

    private void Start()
    {
      EquipWeapon(weapon);

    }


    private void Update()
    {
      
      timeSinceLastAttack += Time.deltaTime;

      if (target == null) return;
      if (target.IsDead())
      {
       GetComponent<Animator>().SetTrigger("stopAttack");
       return;

              };

      if (!GetIsInRange())
      {
        GetComponent<Mover>().MoveTo(target.transform.position);
      }
      else
      {
       
        GetComponent<Mover>().Cancel();
        AttackBehavior();
      }

    }

    private void AttackBehavior()
    {
      transform.LookAt(target.transform);
      if (timeSinceLastAttack > weapon.GetTimeBetweenAttack())
      {
        TriggerAttack();
        timeSinceLastAttack = Mathf.Infinity;

      }
    }

    private void TriggerAttack()
    {
      GetComponent<Animator>().ResetTrigger("stopAttack");//This will trigger HIT() event
      GetComponent<Animator>().SetTrigger("attack");
    }

    void Hit()
    {
      if (target == null || target.IsDead())
      {
        return;
      }
      if (currentWeapon.HasProjectile())
      {
        currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
      }
      else
      {
        target.TakeDamage(currentWeapon.GetDamage());
      }

    }
//adding code
    void Shoot()
    {
      Hit();
    }
    public void EquipWeapon(weapon weapon)
    {
      currentWeapon = weapon;
      Animator animator = GetComponent<Animator>();
      weapon.Spawn(rightHandTransform, leftHandTransform, animator);
    }

    private bool GetIsInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
    }

    public void Attack(GameObject combatTarget)
    {
      GetComponent<ActionScheduler>().StartAction(this);
       //Might be the magic ticket!
      target = combatTarget.GetComponent<Health>();

    }

    public void Cancel()
    {
      
      TriggerStopAttack();
      target = null;
      GetComponent<Mover>().Cancel();
    }

    private void TriggerStopAttack()
    {
      GetComponent<Animator>().ResetTrigger("attack");
      GetComponent<Animator>().SetTrigger("stopAttack");
    }


    public bool CanAttack(GameObject combatTarget)
    {
      if (combatTarget == null)
      {
        return false;
      }
      Health targetToTest = combatTarget.GetComponent<Health>();
      return targetToTest != null && !targetToTest.IsDead();
    }
    //ANIMATION EVENT

  }
}
