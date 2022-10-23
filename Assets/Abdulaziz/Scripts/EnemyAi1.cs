using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi1 : MonoBehaviour
{
    public Transform target;
   public Transform Staringtarget;
    [SerializeField] EnemyMovement1 enemyMovement;
    [SerializeField] private Animator anim;
    [SerializeField] float LookingAroundSpeed = 40;
    public SoundManger soud;
  public  enum EnemyState
    {
       
        Wandering,
        GoToTarget,
        GoToItem,
        LookAround,
        Staring
    }

    public enum Place
    {
        placeA,
        placeB,
        placeC,
        placeD
    }


    [SerializeField] List<Transform> Places = new List<Transform>();

    public EnemyState enemyState = EnemyState.LookAround;
    public Place place = Place.placeA;


    bool Wandring = false;
    bool LookingAround = false;
    bool isStaring = false;
    bool GoingToTarget = false;

    private float dgreeleft;
    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement1>();
    }

    public void StaringState()
    {
        enemyState = EnemyState.Staring;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();
        switch (enemyState)
        {
            
            case EnemyState.Wandering:
                GoWander();
                break;
            case EnemyState.GoToTarget:
                GoToTargetState();
                break;
            case EnemyState.GoToItem:
                break;
            case EnemyState.LookAround:
                if (!LookingAround)
                {
                    StopCoroutine(GoLookAround());
                    StartCoroutine(GoLookAround());
                }
                break;
            case EnemyState.Staring:
                startStaring();
                break;
        }
    }

    void GoToTargetState()
    {
        if (!GoingToTarget)
        {
            GoingToTarget = true;
            
            anim.SetBool("walking", true);
            enemyMovement.SetDestination(target.position);
        }
    }
    void startStaring()
    {
        StopAllCoroutines();
        if (!isStaring)
        {
            isStaring = true;
           
            transform.LookAt(Staringtarget);
        }
    }
     public void DefualtState()
        {
            isStaring = false;
            Wandring = false;
            LookingAround = false;
        StateToLookAround();
        }

    void GoWander()
    {
        if (!Wandring)
        {
            Wandring = true;
            Vector3 targetPlace = Vector3.zero;
            switch (place)
            {
                case Place.placeA:
                    targetPlace = Places[0].position;
                    place = place + 1;

                    break;
                case Place.placeB:
                    targetPlace = Places[1].position;
                    place = place + 1;
                     break;
                case Place.placeC:
                    targetPlace = Places[2].position;
                    place = place + 1;

                    break;
                case Place.placeD:
                    targetPlace = Places[3].position;
                    place = Place.placeA;

                    break;
            }
            enemyMovement.SetDestination(targetPlace);

        }
    }

   

    void CheckTarget()
    {
        if(target != null)
        {
            enemyState = EnemyState.GoToTarget;
        }
    }

    public void StateToLookAround()
    {
        enemyState = EnemyState.LookAround;
        Wandring = false;
    }

    IEnumerator GoLookAround()
    {
        
        {
            LookingAround = true;
            float currentRotation = transform.rotation.eulerAngles.y;
            print(currentRotation);
            float yRotation = transform.rotation.eulerAngles.y;
            float DegreesThisFrame = (180 * Time.deltaTime) / 10;
           

            while (yRotation < currentRotation + 59f)
            {

                yRotation += (LookingAroundSpeed * Time.deltaTime);
              //print($"yRotation must be {yRotation} and the value added {20 * Time.deltaTime}");
                yRotation = Mathf.Clamp(yRotation, currentRotation - 60f, currentRotation + 60f);
                transform.rotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);
                yield return null;
                if (yRotation >= currentRotation + 62f) { break; }
            } 

            yield return new WaitForSeconds(2f);

            do
            {
                yRotation -= (LookingAroundSpeed * Time.deltaTime);
                yRotation = Mathf.Clamp(yRotation, currentRotation - 60f, currentRotation + 60f);
                transform.rotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);
                yield return null;
            } while (yRotation > currentRotation - 59f);



            yield return new WaitForSeconds(2f);

          // enemyState = EnemyState.Wandering;
            LookingAround = false;
            Wandring = false;
        }
    }
}
