using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class overworldMusic : MonoBehaviour
{
    private AudioSource _musicSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _musicSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    public void PlayMusic()
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Play();
        }
    }

    // Stops Music
    public void StopMusic()
    {
        _musicSource.Stop();
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Replace "YourSceneName" with the name of the scene where music should stop
        // Or use `scene.buildIndex` to check the build index instead of the name
        if (scene.name == "BattleSceneWolves")
        {
            StopMusic();
        }
        else
        {
            PlayMusic();
        }
    }
}


