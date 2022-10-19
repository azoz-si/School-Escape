using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
   public enum Item
    {
        Lighter,
        phone,
        key,
        Remote
    }

    public string ItemName;
    public Item ItemType = Item.Lighter;
    public GameObject ItemPrefab;
}
