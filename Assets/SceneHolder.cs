using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHolder : MonoBehaviour
{
    public static string sceneHeld;

    private void Awake()
    {
        sceneHeld = SceneManager.GetActiveScene().name;
        Debug.Log("Current scene: " + sceneHeld);
    }
}
