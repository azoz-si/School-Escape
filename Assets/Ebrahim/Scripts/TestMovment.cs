using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMovment : MonoBehaviour
{
    private NavMeshAgent Navtest;
   [SerializeField] private GameObject patrol;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        Navtest = GetComponent<NavMeshAgent>();
        Navtest.SetDestination(patrol.transform.position);
        distance = Vector2.Distance(transform.position, patrol.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
