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
    [SerializeField] private GameObject exclamationSprite;
    private bool run = false;
    private GameObject player;
    [SerializeField] private SoundManger sound;
    bool issoundRun = true;
    //Start is called before the first frame update
    private void Start()
    {
       // Ai = GetComponent<EnemyAi>();
    }
    private void OnTriggerStay(Collider other)
    {
      
        if (other.gameObject.tag == "Player") 
        {
          
           
           
        }
    }
 

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collison");
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(3);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            run = false;
            issoundRun = true;
            exclamationSprite.active = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            run = true;
            player = other.gameObject;
        }

    }
    private void LateUpdate()
    {
        if (run) 
        {
            Debug.Log("Trigger");
            Vector3 DirictionTarget = (player.gameObject.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, player.gameObject.transform.position);
            RaycastHit hit;
            Ray ray = new Ray(transform.position, DirictionTarget);

            //check what infront of us
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit");
                Debug.DrawLine(transform.position, hit.point, Color.red);

                // if it was the player 
                if (hit.transform.CompareTag("Player"))
                {
                    if (issoundRun) {
                        sound.exmSound();
                        issoundRun = false;
                    }
                    Debug.Log(hit.transform.name);
                    //then do this
                    exclamationSprite.active = true;
                    Debug.Log("i see you");
                    looking = true;
                    Ai.Staringtarget = player.transform;
                    Ai.StaringState();
                    //
                    timer -= 1 * Time.deltaTime;
                    // Ai.enemyMovement.enemyAgent.speed = 0;
                    //if teacher look to the player for 3 seconds the teacher will return the player to the class
                    if (timer <= 0)
                    {
                        //Ai.enemyMovement.enemyAgent.speed = 2;
                        Debug.Log("You dead");
                        Ai.target = player.transform;
                        player.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
                        player.GetComponent<PointAndClickMovements>().enabled = false;

                        timer = 3;
                    }

                }
                // if what we hit was not the player then
                else
                {
                    issoundRun = true;
                    Ai.enemyMovement.enemyAgent.speed = 2;
                    exclamationSprite.active = false;
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
}
