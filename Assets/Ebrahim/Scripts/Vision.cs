using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
   [SerializeField] private LayerMask obs;
    private float timer=3;
    [SerializeField] private GameObject exclamationSprite;
    bool isshowing=true;
    //[SerializeField] EnemyAi techer;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.tag == "Player") 
        {
             Vector3 DirictionTarget = (other.gameObject.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, other.gameObject.transform.position);
            RaycastHit hit;
            Ray ray = new Ray(transform.position,DirictionTarget);
           
            if (Physics.Raycast(ray, out hit))
            {
                 Debug.DrawRay(transform.position,hit.transform.position,Color.red);
                Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("Player")) {
                    Debug.Log("i see you");
                    //
                    if (isshowing) {
                        isshowing = false;
                        exclamationSprite.active = true;
                        Invoke("ShowSprite", 1f);
                    }
                    timer -= 1* Time.deltaTime;
                    if (timer <= 0) 
                    {
                        Debug.Log("Stop");
                    }
                    
                }
                else
                {
                    isshowing = true;
                    if (!(timer <= 0))
                    {
                        timer = 3;
                    }
                    Debug.Log("where are you");
                }

            }
           
        }
    }

    private void ShowSprite()
    {
        exclamationSprite.active = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            if (!(timer<=0)) 
            {
                timer = 3;
            }
        }
    }
}
