using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
   [SerializeField] private LayerMask obs;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
             Vector3 DirictionTarget = (other.gameObject.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, other.gameObject.transform.position);
    
            if (Physics.Raycast(transform.position, DirictionTarget, obs))
            {
                Debug.Log("i see you");
            }
            else 
            {
                Debug.Log("where are you");
            }
        }
    }
}
