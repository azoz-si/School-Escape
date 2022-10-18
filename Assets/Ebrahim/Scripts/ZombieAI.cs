using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
  [Header("Wait Values")]
    [SerializeField] private float minWaitTime = 1.5f;
    [SerializeField] private float maxWaitTime = 7f;
    [Header("Wander Values")]
    [SerializeField] private float minWanderTime = 1.5f;
    [SerializeField] private float maxWanderTime = 7f;
    [SerializeField] private float minRotationDistance = 30f;
    [SerializeField] private float maxRotationDistance = 330f;
    [SerializeField] private float minDistance = -20f;
    [SerializeField] private float maxDistance = 20f;

    [Header("Chase Values")]
    [SerializeField] private float playerStartChaseDistance = 10f;
    [SerializeField] private float playerStopChaseDistance = 15f;

    [Header("Attack Values")]
    [SerializeField] private float AttackStartRange = 5f;
    [SerializeField] private float AttackStopRange = 9f;
    [SerializeField] private float attackCooldown = 4f;

    [Header("Death Values")]
    [SerializeField] private int hp = 1;


    public enum ZombieState
    {
        Wait,
        Chase,
        Wander,
        Attack,
        Death
    }

    [SerializeField]private ZombieState zombiestate = ZombieState.Wait;
    private bool isWaiting = false;
    private bool isWandring = false;
    private bool isChasing = false;
    private bool canAttack = true;
    private bool isDead = false;
    private Coroutine WaitingCorotine;
    private Coroutine WanderingCorotine;

    private Transform player;
    private ZombieMovement zombieMovement;


    // Start is called before the first frame update
    void Start()
    {
        zombieMovement = GetComponent<ZombieMovement>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(hp <= 0 && !isDead)
        {
            zombiestate = ZombieState.Death;
        }
        switch (zombiestate)
        {
            case ZombieState.Wait:
                if (ChasingPlayer())
                {
                    InitChase();
                }
                ZombieWait();
                break;
            case ZombieState.Wander:
                if (ChasingPlayer())
                {
                    InitChase();
                }
                ZombieWander();
                break;
            case ZombieState.Chase:
                if(PlayerDistance() <= AttackStartRange)
                {
                    zombieMovement.IsMoving = false;
                    zombiestate = ZombieState.Attack;
                }
                ZombieChase();
                break;
            case ZombieState.Attack:
                ZombieAttack();
                break;
            case ZombieState.Death:
                ZombieDeath();
                break;

        }
    }

    private void ZombieWait()
    {
    //Debug.Log($"zombieMovement.isArraived={zombieMovement.isArraived} && isWaiting={isWaiting} ");

        if (!isWaiting)
        {
            WaitingCorotine = StartCoroutine(GoWandering());
        }
    }
    IEnumerator GoWandering()
    {
        isWaiting = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(minWaitTime,maxWaitTime));
        zombiestate = ZombieState.Wander;
        isWaiting = false;
    }
    private void ZombieWander()
    {
         // Debug.Log($"zombieMovement.isArraived={zombieMovement.isArraived} && isWandring={isWandring} ");

        if (!isWandring)
        {
            isWandring = true;
            // transform.Rotate(0f,Random.Range(minRotationDistance,maxRotationDistance),0f);
                  zombiestate=ZombieState.Wander;
            float currentx = transform.position.x;
            float currentz = transform.position.z;
            float targetx = currentx + UnityEngine.Random.Range(minDistance,maxDistance);
            float targetz =  currentz + UnityEngine.Random.Range(minDistance, maxDistance);
            float targety = Terrain.activeTerrain.SampleHeight(new Vector3(targetx,180.54f,targetz));
            zombieMovement.SetDestination(new Vector3(targetx,targety,targetz));
            //WanderingCorotine = StartCoroutine(WalkTime());
        }
          if(zombieMovement.isArraived&&isWandring)
        {
            isWaiting = false;
            isWandring=false;
            zombiestate=ZombieState.Wait;
            Debug.Log("has arrived");
        }
    }
    IEnumerator WalkTime()
    {
        zombieMovement.IsMoving = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(minWanderTime,maxWanderTime));
        zombieMovement.IsMoving = false;
        isWaiting = false;
        zombiestate = ZombieState.Wait;
        isWandring = false;

    }
    private void ZombieChase()
    {
      Debug.Log($"ChasingPlayer()={ChasingPlayer()}");
        if (ChasingPlayer())
        {
            transform.LookAt(player);
            zombieMovement.SetDestination(player.position);
            Debug.Log("Chasing Player");
        }
        else
        {
            //zombieMovement.IsMoving = false;
            zombiestate = ZombieState.Wait;
            zombieMovement.SetDestination(transform.position);
            Debug.Log("Stop Chasing");  
        }
    }

    private float PlayerDistance()
    {
        return  Vector3.Distance(transform.position, player.position); 
    }
    private bool ChasingPlayer()
    {
        if (isChasing)
        {
            if(PlayerDistance() <= playerStopChaseDistance)
            {
                return true;
            }
            else
            {
                isChasing = false;
                return false;
            }
        }
        else
        {
            if(PlayerDistance() <= playerStartChaseDistance)
            {
                isChasing = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void InitChase()
    {
        StopCoroutine(WaitingCorotine);
       // StopCoroutine(WanderingCorotine);
        isWaiting = false;
        isWandring = false;
        zombieMovement.IsMoving = true;
        zombiestate = ZombieState.Chase;
    }
    private void ZombieAttack()
    {
        if (canAttack)
        {
            //attack code
            Debug.Log("Attacking the Player");
            if(PlayerDistance()<2)
            {
              if(player.rotation.z<=0)
              player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward*500);
              else 
              player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward*-500);

            }
            StartCoroutine(AttackCooldown());
        }
        else
        {
            if(PlayerDistance() > AttackStopRange)
            {
                InitChase();
            }
        }
    }

     IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    void ZombieDeath()
    {
        StopAllCoroutines();
        zombieMovement.IsMoving = false;
        isWaiting = false;
        isWandring = false;
        isChasing = false;
        canAttack = false;
        zombieMovement.SetDestination(transform.position);

    }
}