using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
  public  bool canBeSeen = true;
    bool Seen = false;

    [SerializeField] List<Items> ItemList = new List<Items>();



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Chair")
        {
            canBeSeen = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Chair")
        {
            canBeSeen = true;
        }
    }
}


