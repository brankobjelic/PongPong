using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickUI : MonoBehaviour
{

    public GameObject joysticksGameObject;
    
    void Awake()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            joysticksGameObject.SetActive(false);
        }
    }

}
