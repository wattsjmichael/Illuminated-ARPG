using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace RPG.UI.DamageText
{
public class DamageText : MonoBehaviour
{
   [SerializeField] Text damageText = null;
  

     public void setValue(float amount)
     {
       
       damageText.text = String.Format("{0}", amount);
     
     }
}
}