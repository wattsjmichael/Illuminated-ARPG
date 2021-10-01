using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
  public class weapon : ScriptableObject
  {

    [SerializeField] AnimatorOverrideController animatorOverride = null;
    [SerializeField] GameObject equippedPrefab = null;
    [SerializeField] float weaponRange = 2f;
    [SerializeField] float weaponDamage = 5f;
    [SerializeField] bool isRightHanded = true;

     [SerializeField] float timeBetweenAttacks = 1f;
    public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
    {
      if (equippedPrefab != null)
      {
        Transform handtransform;
        if(isRightHanded) 
        {
          handtransform = rightHand;
        }
        else
        { 
          handtransform = leftHand;
        }
      Instantiate(equippedPrefab, rightHand);
      }
      if (animatorOverride != null)
      {
      animator.runtimeAnimatorController = animatorOverride;

      }
    }

    public float GetDamage()
    {
        return weaponDamage;
    }

      public float GetRange()
    {
        return weaponRange;
    }

       public float GetTimeBetweenAttack()
    {
        return timeBetweenAttacks;
    }
  }
}