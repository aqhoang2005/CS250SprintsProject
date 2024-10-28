using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiveToValue : MonoBehaviour
{
    public static bool myCondition;
    public GiveToValue instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
       bool newBool = StaticData.arguementToCheck;
       myCondition = newBool;
    }
}
