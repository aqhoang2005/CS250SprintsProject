using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDown_Timer : MonoBehaviour
{
    public string levelToLoad;
    public float countDown = 110f;



    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0)
        {
            SceneManager.LoadSceneAsync(levelToLoad);
        }
    }
}
