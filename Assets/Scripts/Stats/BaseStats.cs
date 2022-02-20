using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Stats
{
  public class BaseStats : MonoBehaviour
  {
    [Range(1, 99)]
    [SerializeField] int startingLevel = 1;
    [SerializeField] CharacterClass characterClass;
    [SerializeField] Progression progression = null;

    public void Update()
    {
      if(gameObject.tag == "Player")
      {
      print(GetLevel());
      }
    }

    public float GetStat(Stat stat)
    {
        return progression.GetStat(stat, characterClass, startingLevel);
    }

   public int GetLevel()
   {
     Experience experience = GetComponent<Experience>();
     if (experience == null) return startingLevel;
     
     float currentXP = GetComponent<Experience>().GetPoints();
     int penulatimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
     for(int level =1; level < penulatimateLevel; level++)
     {
       float xpToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
       if (xpToLevelUp > currentXP)
       {
         return level;
       }
     }
     return penulatimateLevel +1;
   }
   
  }
}