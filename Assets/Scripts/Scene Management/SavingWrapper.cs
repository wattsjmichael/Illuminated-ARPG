﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagment
{
  public class SavingWrapper : MonoBehaviour
  {
    const string defaultSaveFile = "save";
    [SerializeField] float fadeInTime = 2f;

    IEnumerator Start()
    {
      Fader fader = FindObjectOfType<Fader>();
      fader.FadeOutImmediate();
      yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
      yield return fader.FadeIn(fadeInTime);
    }

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.L))
      {
        Load();
      }

      if (Input.GetKeyDown(KeyCode.S))
      {
        Save();
      }

        if (Input.GetKeyDown(KeyCode.Escape))
      {
        Delete();
      }



    }

    public void Load()
    {
      GetComponent<SavingSystem>().Load(defaultSaveFile);
    }  
    public void Save()
    {
      GetComponent<SavingSystem>().Save(defaultSaveFile);
    }

      public void Delete()
    {
      GetComponent<SavingSystem>().Delete(defaultSaveFile);
      Debug.Log(defaultSaveFile);
      
    }
  }
}