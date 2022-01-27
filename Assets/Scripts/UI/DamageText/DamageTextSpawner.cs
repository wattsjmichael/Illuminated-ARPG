using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.UI.DamageText
{
  public class DamageTextSpawner : MonoBehaviour
  {
    //Component that is in the root of the prefab
    [SerializeField] DamageText damageTextPrefab = null;


    public void Spawn(float damageAmount)

    {
    
      {
        DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        instance.setValue(damageAmount);
      }
    }

  }
}