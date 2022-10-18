using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    //this will not be a singleton
    //each zombie we make will have it's own movement script

    //we'll enable and disable this with our ZombieAI script.
    public bool IsMoving = false;
    //this will control the zombie movement speed.
    [SerializeField] private float MoveSpeed = 5f;

    //our rigidbody for movement
    private CharacterController charController;
    public NavMeshAgent navMeshAgent;
    public bool isArraived=false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float newPositon=Vector3.Distance(transform.position,navMeshAgent.destination);
        if(newPositon<=1)
        isArraived=true;
        else
        isArraived=false;
        //if we're not moving, then exit the update
      //  if (!IsMoving)
    //    {
            //exit the Update
     //       return;
     //   }

        //move the zombie forward in the z axis only
       // charController.SimpleMove(transform.TransformDirection(Vector3.forward) * MoveSpeed * Time.deltaTime);
//if(isArraived)
//{
    //if(navMeshAgent.isStopped)
    //{
        
    //isArraived=true;
    //}else  {isArraived=false;
    
  //  }

//}
    }
    public void SetDestination(Vector3 dest)
    {
  //    print("dest:"+dest);
        navMeshAgent.SetDestination(dest);
//        Debug.Log("navMesh dest:"+navMeshAgent.destination);
    isArraived=true;       
    }
}
