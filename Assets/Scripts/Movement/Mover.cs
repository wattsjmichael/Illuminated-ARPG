﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;


namespace RPG.Movement
{
  public class Mover : MonoBehaviour, IAction, ISaveable
  {


    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;
    Health health;

 
    void Start()
    {

       navMeshAgent = GetComponent<NavMeshAgent>();
       health = GetComponent<Health>(); 
    }


    void Update()
    {
      navMeshAgent.enabled = !health.IsDead();
      UpdateAnimator();
    }


public void StartMoveAction(Vector3 destination)
{
  GetComponent<ActionScheduler>().StartAction(this);
   MoveTo(destination);
}
    public void MoveTo(Vector3 destination)
    {
      GetComponent<NavMeshAgent>().destination = destination;
      navMeshAgent.isStopped = false;

    }

    public void Cancel()
    {
      navMeshAgent.isStopped = true;
    }

 

    private void UpdateAnimator()
    {
      Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
      Vector3 localVelocity = transform.InverseTransformDirection(velocity);
      float speed = localVelocity.z;
      GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    public object CaptureState()
    {
      return new SerializableVector3(transform.position);
    }

      public void RestoreState(object state)
    {
      SerializableVector3 position = (SerializableVector3)state;
      GetComponent<NavMeshAgent>().enabled = false;
      transform.position = position.ToVector();
      GetComponent<NavMeshAgent>().enabled = true;
    }
  }

}
