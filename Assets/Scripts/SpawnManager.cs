using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{


    public GameObject spawnpwnPoint;
    public GameObject bueyrPrefab;

    bool generated = false;


    void Start()
    {
       StartCoroutine(NewBuyer());
    }

    IEnumerator NewBuyer()
    {
        generated = true;
        yield return new WaitForSeconds(1);
        Instantiate(bueyrPrefab, spawnpwnPoint.transform.position, spawnpwnPoint.transform.rotation);
        generated = false;
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        BuyerController custumer = other.GetComponent<BuyerController>();

        Destroy(custumer);
        
        if (generated == false)
        {
            StartCoroutine(NewBuyer());
        }
        

    }
}
