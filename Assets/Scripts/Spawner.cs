using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private GameObject pickupObject;
    bool isCouroutineRunning = false;

    private void Start()
    {
        GameManager.instance.onReset += Spawn;
        GameManager.instance.gameUI.onStartGame += Spawn;
    }

    public void ResetSpawner()
    {
        if (pickupObject != null)
        {
            Destroy(pickupObject);
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
        isCouroutineRunning=true;
        yield return new WaitForSeconds(5);
        pickupObject = Instantiate(objectToSpawn, new Vector3(0, Random.Range(-3f, 3f), 0), transform.rotation);
        isCouroutineRunning = false;
    }

}
