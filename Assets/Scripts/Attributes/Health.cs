using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Attributes
{
  public class Health : MonoBehaviour, ISaveable
  {
    float healthPoints = -1f;
    [SerializeField] TakeDamageEvent takeDamage;

    [System.Serializable]
    public class TakeDamageEvent : UnityEvent<float>
    {
    }

private void Start()
{
    if (healthPoints < 0)
    {
      healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
    }
}
    public bool isDead = false;


    public bool IsDead()
    {
      return isDead;
    }

    public void TakeDamage(GameObject instigator, float damage)
    
    {
      if(IsDead()) return;
      healthPoints = Mathf.Max(healthPoints - damage, 0);


      if (healthPoints <= 0)
      {
        DeathAnimation();
        takeDamage.Invoke(damage);
        AwardExperience(instigator);
      }
      else
      {
        takeDamage.Invoke(damage);
      }

    }

    private void AwardExperience(GameObject instigator)
    {
      Experience experience = instigator.GetComponent<Experience>();
      if(experience == null) return;
      experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
    }

    public float GetPercentage()
{
 return  100 * (healthPoints/GetComponent<BaseStats>().GetStat(Stat.Health));
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