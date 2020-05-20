using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomSpawner : MonoBehaviour
{
    [Header("Spawnable Prefabs")]
    [SerializeField] GameObject Item1;
    [SerializeField] GameObject Item2;
    [SerializeField] GameObject Item3;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            float itemNumber = UnityEngine.Random.Range(0, 3);
            print(itemNumber);

            if (itemNumber < 1)
            {
                GameObject pickUp = Instantiate(Item1, transform.position, Quaternion.identity) as GameObject;
            }
            else if (itemNumber >= 1 && itemNumber < 2)
            {
                GameObject pickUp = Instantiate(Item2, transform.position, Quaternion.identity) as GameObject;
            }
            else if (itemNumber >= 2)
            {
                GameObject pickUp = Instantiate(Item3, transform.position, Quaternion.identity) as GameObject;
            }
        }
        catch (UnassignedReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
