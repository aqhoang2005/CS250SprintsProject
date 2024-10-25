using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameStates { Overworld, BattleGrounds, Idle}
    public GameStates gameStates;

    public bool gotAttacked;
    public static GameManager instance;
    public GameObject leadCharacter;
    public string sceneName;
    public string prevScene;
   
    public string sceneToLoad;
    public Vector2 playerPosition;
    public Vector2 lastPlayerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public GameObject enemyCheck;
    public float fadeWait;
    public bool isNextScene = true;

    public string lastScene;

    BattleSystem battleSystem;

    public List <GameObject> enemiesInArea = new List <GameObject>();

    public List<SceneAsset> scenes = new List<SceneAsset>();

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            gotAttacked = true;
            lastPlayerPosition = GameObject.Find("Player").gameObject.transform.position;
            playerStorage.initialValue = playerPosition;
        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            // enemyCheck.SetActive(false);
            // enemyCurrentStateWin = true;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;


        switch (gameStates)
        {
            case (GameStates.Overworld):
                if (gotAttacked == true)
                {
                    StartCoroutine(FadeCo());
                    gameStates = GameStates.BattleGrounds;
                }

                break;
            case (GameStates.BattleGrounds):
                break;
            case (GameStates.Idle):
                break;
        }

    }
}

