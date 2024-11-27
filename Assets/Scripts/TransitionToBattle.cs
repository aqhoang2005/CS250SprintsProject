
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToBattle : MonoBehaviour
{
    //Made public to allow access from other scripts - jon
    public static TransitionToBattle instance;

    public string sceneToLoad;
    public Vector2 playerPosition;
    public Vector2 lastPlayerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public GameObject enemyCheck;
    public float fadeWait;
    public bool isNextScene = true;
    public bool isEnemyTriggered = false;

    public GameObject enemyHolder;


    public string lastScene;

    BattleSystem battleSystem;

    //[SerializeField]
    //public SceneInfo sceneInfo;
    // public bool enemyCurrentStateWin;

    private void Start()
    {
        //enemyCheck.SetActive(true);
        // enemyCurrentStateWin = false;
    }

    private void Awake()
    {
        instance = this;

        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void SceneToLoadAfterBattle()
    {
        SceneManager.LoadScene(battleSystem.lastScene);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            lastPlayerPosition = GameObject.Find("Player").gameObject.transform.position;
            //lastScene = SceneManager.GetActiveScene().name;
            playerStorage.initialValue = playerPosition;

            //sceneInfo.isNextScene = isNextScene;
            StartCoroutine(FadeCo());
            //SceneManager.LoadScene(sceneToLoad);

        }
    }

    //Added this method to trigger battle from other scripts - jon
    public void StartBattle()
    {
        StartCoroutine(FadeCo());
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
            isEnemyTriggered = true;
            //StaticData.arguementToCheck = isEnemyTriggered;
            yield return null;
        }
    }

    private void Update()
    {
        isEnemyTriggered = GiveToValue.myCondition;
        if (isEnemyTriggered == true)
        {
            Destroy(enemyHolder);
        }
    }
}
