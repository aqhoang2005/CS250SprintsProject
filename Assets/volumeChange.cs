using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeChange : MonoBehaviour
{
    public GameObject go = GameObject.Find("throneRedBG");
    void Update()
    {
        go.GetComponent<AudioSource>().volume = 0.5f;
    }
}
