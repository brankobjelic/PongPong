using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject[] objectsToSpawn;
    public List<GameObject> objectsWaiting;
    private GameObject pickupObject;
    static public List<GameObject> pickupObjects = new List<GameObject>();
    bool isCouroutineRunning = false;
    int randomIndex = 0;
    Collider2D overlapingCollider;
    Vector3 spawnPosition;

    private void Start()
    {
        GameManager.instance.onReset += Spawn;
        GameManager.instance.gameUI.onStartGame += Spawn;
    }

    public void ResetSpawner()
    {
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

    private void Spawn()
    {
        StartCoroutine(SpawnAfterTime());
    }

    IEnumerator SpawnAfterTime()
    {
        isCouroutineRunning = true;
        int randomWaitTime = Random.Range(3, 6);
        yield return new WaitForSeconds(randomWaitTime);

        randomIndex = Random.Range(0, objectsToSpawn.Length);
        var found = pickupObjects.Find(po => objectsToSpawn[randomIndex].CompareTag(po.tag));
        if (found == null)
        {
            do 
            {
                spawnPosition = new Vector3(0, Random.Range(-3f, 3f), 0);
                overlapingCollider = Physics2D.OverlapBox(spawnPosition, objectsToSpawn[randomIndex].GetComponent<BoxCollider2D>().size, 0);                
            } while (overlapingCollider != null);
            pickupObject = Instantiate(objectsToSpawn[randomIndex], spawnPosition, transform.rotation);
            pickupObjects.Add(pickupObject);
        }
    
        isCouroutineRunning = false;
        StartCoroutine(SpawnAfterTime());
    }

}
