using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoEnemyBattleSystem : BattleSystem
{

    public GameObject EnemyPrefab2;
    // public BattleHUD enemyHud2;
    public Transform secondEnemyBattleLocation;
    Unit enemyUnit2;

    IEnumerator SetUpBattleAgain()
    {
        GameObject enemyGO2 = Instantiate(enemyPrefab, secondEnemyBattleLocation);
        enemyUnit2 = enemyGO2.GetComponent<Unit>();

        enemyHud.SetHUD(enemyUnit2);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

}
