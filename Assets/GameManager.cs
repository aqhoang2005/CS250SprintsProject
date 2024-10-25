using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject leadCharacter;
    public string sceneName;
    public string prevScene; 

    public List <GameObject> enemiesInArea = new List <GameObject>();

    public List<SceneAsset> scenes = new List<SceneAsset>();


BattleSystem battleSystem;

    private void Awake()
    {
        if (instance == null) { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        prevScene = sceneName;
    }

    public void BattleWon()
    {
        if(battleSystem.state == BattleState.WON)
        {
            SceneManager.LoadSceneAsync(prevScene);
        }
    }


    // Update is called once per frame
    void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
}
