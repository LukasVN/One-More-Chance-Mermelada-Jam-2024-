using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRandomObject : MonoBehaviour
{
    public GameObject[] items;
    void Start()
    {
        items[Random.Range(0,items.Length)].SetActive(true);
    }


}
