using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    //Button GameObject Variables
    public GameObject healButton;
    public GameObject akeruHealButton;
    public GameObject attackButton;
    public GameObject akeruAttackButton;
    public GameObject meleeButton;
    public GameObject akeruBlackMagic;

    public Transform playerBattleSystem;
    public Transform enemyBattleSystem;

    public BattleHUD playerHud;
    public BattleHUD enemyHud;

    private bool akeruTurn = false;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        attackButton.SetActive(true);
        healButton.SetActive(true);
        akeruHealButton.SetActive(false);
        akeruAttackButton.SetActive(false);
        meleeButton.SetActive(false);
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleSystem);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleSystem);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches..."; 
        
        playerHud.SetHUD(playerUnit);
        enemyHud.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        //damage enemy 
        dialogueText.text = "That was SUPER effective!";
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHud.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        //check if enemy is dead
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //Change state based on what happened
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerBlackMagic()
    {
        //damage enemy 
        dialogueText.text = "Feel the power of the dark side!";
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage + 20);
        enemyHud.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        //check if enemy is dead
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //Change state based on what happened
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(10);

        playerHud.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " has attacked!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage); 
        playerHud.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";

        }
        else if(state == BattleState.LOST){
            dialogueText.text = "You were defeated!";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
    }

    public void OnAttackButton()
    {
        healButton.SetActive(false);
        akeruHealButton.SetActive(false);
        attackButton.SetActive(false);
        akeruAttackButton.SetActive(true);
    }

    public void OnAkeruAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        akeruAttackButton.SetActive(false);
        meleeButton.SetActive(true);
        akeruBlackMagic.SetActive(true);
        akeruTurn = true;
    }

    public void OnMeleeAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        if (akeruTurn == true)
        {
            StartCoroutine(PlayerAttack());
            meleeButton.SetActive(false);
            attackButton.SetActive(true);
            healButton.SetActive(true);
            akeruTurn = false;
        }
    }

    public void OnBlackMagicAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        if (akeruTurn == true)
        {
            StartCoroutine(PlayerBlackMagic());
            akeruBlackMagic.SetActive(false);
            meleeButton.SetActive(false);
            attackButton.SetActive(true);
            healButton.SetActive(true);
            akeruTurn = false;
        }
    }


    public void OnHealButton()
    {
        healButton.SetActive(false);
        akeruHealButton.SetActive(true);
        attackButton.SetActive(false);
        dialogueText.text = "Choose a party member to heal:";

        //StartCoroutine(PlayerHeal());
    }

    public void OnHealAkeruButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
        healButton.SetActive(true);
        akeruHealButton.SetActive(false);
        attackButton.SetActive(true);

    }
}
