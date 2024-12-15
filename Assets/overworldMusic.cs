using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class overworldMusic : MonoBehaviour
{
    public static overworldMusic musicPlay;
    public GameObject Music;

    private string firstLevel = "StartRoomScene";
    void Awake()
    {
        if (musicPlay == null)
        {
            DontDestroyOnLoad(gameObject);
            musicPlay = this;
        }
        else if (musicPlay != this)
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == firstLevel)
        {
            Stop();
        }
    }

    public void Stop()
    {
        if (Music != null)
        {
            Destroy(gameObject);
            
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Stop();
        }
    }
}


