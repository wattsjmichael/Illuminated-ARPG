using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace RPG.Stats
{

  public class LevelDisplay : MonoBehaviour

  {
    BaseStats stats;
    // Start is called before the first frame update
    void Awake()
    {
      stats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
    }

    // Update is called once per frame
    void Update()
    {
      GetComponent<Text>().text = String.Format("{0:0}", stats.GetLevel()); ;
    }
  }
}