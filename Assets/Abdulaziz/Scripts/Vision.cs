using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Vision : MonoBehaviour
{
   [SerializeField] private LayerMask obs;
    private float timer=3;
    [SerializeField] EnemyAi Ai;
    bool looking = false;
    //Start is called before the first frame update
    private void Start()
    {
       // Ai = GetComponent<EnemyAi>();
    }
    private void OnTriggerStay(Collider other)
    {
      
        if (other.gameObject.tag == "Player") 
        {
          
            Debug.Log("Trigger");
            Vector3 DirictionTarget = (other.gameObject.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, other.gameObject.transform.position);
            RaycastHit hit;
            Ray ray = new Ray(transform.position,DirictionTarget);
            
            //check what infront of us
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit");
                Debug.DrawLine(transform.position, hit.point, Color.red);

                // if it was the player 
                if (hit.transform.CompareTag("Player")) 
                {
                    //then do this
                    Debug.Log("i see you");
                    looking = true;
                    Ai.Staringtarget = other.transform;
                    Ai.StaringState();
                    //
                    timer -= 1* Time.deltaTime;

                    //if teacher look to the player for 3 seconds the teacher will return the player to the class
                    if (timer <= 0) 
                    {
                        Debug.Log("You dead");
                        Ai.target = other.transform;
                        other.GetComponent<NavMeshAgent>().SetDestination(other.transform.position);
                        other.GetComponent<PointAndClickMovements>().enabled = false;
                        
                        timer = 3;
                    }
                    
                }
                // if what we hit was not the player then
                else
                {

                    if (!(timer <= 0))
                    {
                        timer = 3;
                    }
                    Debug.Log("where are you");
                    if (looking)
                    {
                        looking = false;
                        Ai.DefualtState();

                    }
                }

            }
           
        }
    }
 

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
