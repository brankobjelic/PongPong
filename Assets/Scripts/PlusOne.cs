using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusOne : MonoBehaviour
{
    public void OnDestroy()
    {
        Spawner.pickupObjects.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
