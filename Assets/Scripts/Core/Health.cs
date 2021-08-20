﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace RPG.Core
{
  public class Health : MonoBehaviour
  {
    [SerializeField] float healthPoints = 100f;
    bool isDead = false;
    
    public bool IsDead()
    {
      return isDead;
    }

    public void TakeDamage(float damage)
    {
      healthPoints = Mathf.Max(healthPoints - damage, 0);
      if(healthPoints == 0)
      {
        DeathAnimation();
      }
      
    }

    public void DeathAnimation()
    {
      
      if(isDead) return;
      isDead = true;
      GetComponent<Animator>().SetTrigger("die");
      GetComponent<ActionScheduler>().CancelCurrentAction();
      
    }

    
  }
}