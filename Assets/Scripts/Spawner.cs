using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject[] objectsToSpawn;
    private GameObject pickupObject;
    private List<GameObject> pickupObjects = new List<GameObject>();
    bool isCouroutineRunning = false;

    private void Start()
    {
        GameManager.instance.onReset += Spawn;
        GameManager.instance.gameUI.onStartGame += Spawn;
    }

    public void ResetSpawner()
    {
        //if (pickupObjects != null)
        //{
        //    Destroy(pickupObjects);
        //}
        if (pickupObjects != null)
        {
            foreach (GameObject go in pickupObjects)
            {
                Destroy(go);
            }
        }
        if (isCouroutineRunning)
        {
            StopAllCoroutines();
        }
    }

    public void RemoveElement(PlusOne element)
    {
        //Destroy(element);
        foreach (GameObject go in pickupObjects)
        {
            if(go != null && ReferenceEquals(go, element)) 
            {
                Destroy(go);
                Destroy(element);
            }
        }
    }

    private void Spawn()
    {
        StartCoroutine(SpawnAfterTime());
    }

    IEnumerator SpawnAfterTime()
    {
        isCouroutineRunning=true;
        yield return new WaitForSeconds(5);
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        pickupObject = Instantiate(objectsToSpawn[randomIndex], new Vector3(0, Random.Range(-3f, 3f), 0), transform.rotation);
        pickupObjects.Add(pickupObject);
        isCouroutineRunning = false;
    }

}
