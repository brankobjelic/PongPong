using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendPaddle : MonoBehaviour
{
    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
