using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public string sceneToLoad;
    public float fadeWait;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Cutscene#1");
        }
    }

    public IEnumerator FadeCoContinue()
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

    public IEnumerator FadeCoQuit()
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
}
