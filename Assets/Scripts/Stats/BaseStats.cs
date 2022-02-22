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

    int currentlevel = 0;

    private void Start()
    {
      currentlevel = CalculateLevel();
    }

    public void Update()
    {
      int newLevel = CalculateLevel();
      if (newLevel > currentlevel)
      {
        currentlevel = newLevel;
        print("Leveled Up!");
      }
    }

    public float GetStat(Stat stat)
    {
        return progression.GetStat(stat, characterClass, startingLevel);
    }


   public int GetLevel()
   {
     return currentlevel;
   } 

   public int CalculateLevel()
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