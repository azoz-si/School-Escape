using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public NavMeshAgent enemyAgent;
    [SerializeField] EnemyAi1 enemyAi;
    public bool Arrived;

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAi = GetComponent<EnemyAi1>();
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
        if(Distance < 2 && !Arrived)
        {
            enemyAi.soud.TeatcherStop();
            Arrived = true;
            enemyAi.StateToLookAround();
        }
        if (!Arrived) { enemyAi.soud.TeacherWallkSound(); }
    }
}
