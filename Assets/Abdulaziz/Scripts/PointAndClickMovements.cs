using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PointAndClickMovements : MonoBehaviour
{

    [Header("References")]
    [SerializeField] NavMeshAgent playerAgent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movements();
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
                float Distance = CheckDistance(this.transform.position,hit.point);

                if(Distance > 1)
                {
                    playerAgent.SetDestination(hit.point);
                }
            }
        }
    }

    float CheckDistance(Vector3 firstPoint,Vector3 SecondPoint)
    {
        return Vector3.Distance(firstPoint,SecondPoint);
    }

}
