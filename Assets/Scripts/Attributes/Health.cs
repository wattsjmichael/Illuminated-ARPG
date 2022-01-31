using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Attributes
{
  public class Health : MonoBehaviour, ISaveable
  {
    [SerializeField] float healthPoints = 100f;
    [SerializeField] TakeDamageEvent takeDamage;

    [System.Serializable]
    public class TakeDamageEvent : UnityEvent<float>
    {
    }

private void Start()
{
    healthPoints = GetComponent<BaseStats>().GetHealth();
}
    public bool isDead = false;


    public bool IsDead()
    {
      return isDead;
    }

    public void TakeDamage(float damage)
    
    {
      if(IsDead()) return;
      healthPoints = Mathf.Max(healthPoints - damage, 0);


      if (healthPoints <= 0)
      {
        DeathAnimation();
        takeDamage.Invoke(damage);
      }
      else
      {
        takeDamage.Invoke(damage);
      }

    }

    public void DeathAnimation()
    {
      
      if (isDead) return;
      isDead = true;
      GetComponent<Animator>().SetTrigger("die");
      GetComponent<ActionScheduler>().CancelCurrentAction();

    }

    public object CaptureState()
    {
      return healthPoints;
    }

    public void RestoreState(object state)
    {
      healthPoints = (float)state;
      if (healthPoints == 0)
      {
        DeathAnimation();
      }
    }
  }
}