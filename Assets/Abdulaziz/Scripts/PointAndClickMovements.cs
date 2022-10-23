using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PointAndClickMovements : MonoBehaviour
{

    [Header("References")]
    [SerializeField] NavMeshAgent playerAgent;
    [SerializeField] private Animator anim;
    float Distance=1;
    float distance2 = 1;
    Vector3 hitpoint;
    public bool Arrived;
    [SerializeField] private SoundManger sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        Movements();
        

    }
    private void LateUpdate()
    {
        distance2 = CheckDistance(this.transform.position, hitpoint);

        if (distance2 < 1 && !Arrived)
        {
            sound.playerStop();
           
                 
            anim.SetBool("Walk", false);
            Arrived = true;
        }
        if (!Arrived) { sound.PlayerWallkSound(); }
        
    }

    private void Movements()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("true");
            Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                
             Distance = CheckDistance(this.transform.position,hit.point);
                Debug.Log("dest is : "+Distance);
                if (Distance > 1)
                {
                    hitpoint = hit.point;
                    anim.SetBool("Walk", true);
                    playerAgent.SetDestination(hit.point);
                    Arrived=false;
                   

}
             
                
            }
        }
    }

    float CheckDistance(Vector3 firstPoint,Vector3 SecondPoint)
    {
        return Vector3.Distance(firstPoint,SecondPoint);
    }

}
