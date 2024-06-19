using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private GameObject pickupObject;

    private void Start()
    {
        GameManager.instance.onReset += Spawn;  //assign ResetBall method to OnReset Action variable (delegate)
        GameManager.instance.gameUI.onStartGame += Spawn;
    }

    public void ResetSpawner()
    {
        if (pickupObject != null)
        {
            Destroy(pickupObject);
        }
        //Spawn();
    }

    private void Spawn()
    {
        pickupObject = Instantiate(objectToSpawn, new Vector3(0, Random.Range(-3f, 3f), 0), transform.rotation);
    }

}
