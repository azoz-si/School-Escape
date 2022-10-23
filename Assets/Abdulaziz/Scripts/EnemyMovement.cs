using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public NavMeshAgent enemyAgent;
    [SerializeField] EnemyAi enemyAi;
    public bool Arrived;
    
    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAi = GetComponent<EnemyAi>();
    }
    public void SetDestination(Vector3 Dest)
    {
        enemyAgent.SetDestination(Dest);
        Arrived = false;
    }

    private void LateUpdate()
    {
        float Distance = Vector3.Distance(transform.position,enemyAgent.destination);

        //  print($"the Distance = {Distance},  and the destination = {enemyAgent.destination}");
        if (Distance < 2 && !Arrived)
        {
            enemyAi.sound.TeatcherStop();
            enemyAi.anim.SetBool("walking", false);
            Arrived = true;
            enemyAi.StateToLookAround();
        }
        if (!Arrived) 
        {
            enemyAi.sound.TeacherWallkSound();
        }
    }
}
