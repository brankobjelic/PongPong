using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusOne : MonoBehaviour
{
    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
