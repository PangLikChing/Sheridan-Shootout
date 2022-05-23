using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionExit(Collision collision)
    {
        //prevent crazy things happen
        gameObject.tag = "Untagged";
    }

    void OnEnable()
    {
        //using hide to prevent instantiating new GameObjects
        Invoke("HideSelf", 5);
    }

    void HideSelf()
    {
        gameObject.SetActive(false);
    }
}
