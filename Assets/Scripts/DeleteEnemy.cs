using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{
    public GameObject enemy;
    private BattleSystem enemyDeletion;
    // Start is called before the first frame update
    void Start()
    {
        enemy.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       // if(enemyDeletion.enemyDefeated == true)
       // {
        //    enemy.SetActive (false);
       // }
    }
}
