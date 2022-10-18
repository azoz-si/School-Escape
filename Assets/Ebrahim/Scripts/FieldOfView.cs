using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
     public float angel;
     public GameObject Player;
     public LayerMask obsticalMask;
     public LayerMask TargetMask;
     public bool CanSeePlayer;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(angel);
    }
    private IEnumerator VOFRoutine()  
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (true) 
        {
            yield return wait;
            fieldOfView();
        }
    }

    private void fieldOfView()
    {
       
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position,radius,TargetMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 DirictionTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, DirictionTarget) < angel / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, DirictionTarget, distance, obsticalMask))
                {

                    CanSeePlayer = true;
                    print("i see you");

                }
                else
                {
                    CanSeePlayer = false;
                    print("Where are you hiding");
                }
            }
            else CanSeePlayer = false;
        }
        else if (CanSeePlayer) { CanSeePlayer = false; }
    }
}
