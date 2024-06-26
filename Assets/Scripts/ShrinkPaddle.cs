using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPaddle : MonoBehaviour
{

    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
