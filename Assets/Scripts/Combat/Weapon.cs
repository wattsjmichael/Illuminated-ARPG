using System.Collections;
using System.Collections.Generic;
using RPG.Core;
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
    [SerializeField] Projectile projectile = null;

    [SerializeField] float timeBetweenAttacks = 1f;
    public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
    {
      if (equippedPrefab != null)
      {
        Transform handtransform = GetTransform(rightHand, leftHand);
        Instantiate(equippedPrefab, handtransform);
      }
      if (animatorOverride != null)
      {
        animator.runtimeAnimatorController = animatorOverride;

      }
    }

    private Transform GetTransform(Transform rightHand, Transform leftHand)
    {
      Transform handtransform;
      if (isRightHanded)
      {
        handtransform = rightHand;
      }
      else
      {
        handtransform = leftHand;
      }

      return handtransform;
    }

    public bool HasProjectile()
    {
      return projectile != null;
    }

    public float GetDamage()
    {
      return weaponDamage;
    }

    public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
    {
      Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity );
      projectileInstance.SetTarget(target);
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