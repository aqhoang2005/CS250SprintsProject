using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject akeruSword; 

    public Transform playerBattleSystem;
    public Transform enemyBattleSystem;

    public BattleHUD playerHud;
    public BattleHUD enemyHud;

    public Unit playerExp;

    //public string sceneToChangeTo = GameManager.instance.prevScene;

    //public TransitionToBattle transitionToBattle;

    public Vector3 lastPlayerposition;

    public string lastScene;

    private bool akeruTurn = false;

    public bool enemyDefeated = false;

    Unit playerUnit;
    Unit enemyUnit;

    ExperienceManager enemyManager;

    DeleteEnemy enemyMask;

    public Text dialogueText;

    public int levelNum = 1;
    public int expEarned = 200;
    public float RadNum = 0f;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        attackButton.SetActive(true);
        healButton.SetActive(true);
        akeruHealButton.SetActive(false);
        akeruAttackButton.SetActive(false);
        akeruSword.SetActive(false);
        meleeButton.SetActive(false);
        enemyManager = GameObject.Find("ExperienceManager").GetComponent<ExperienceManager>();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        enemyDefeated = false;

        //Amount of enemies
        //int enemyAmount

        GameObject playerGO = Instantiate(playerPrefab, playerBattleSystem);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleSystem);
        enemyUnit = enemyGO.GetComponent<Unit>();

        //if (enemyUnit.unitName == "SandBoy")
        // {
        //    enemyManager.expToGive = 300;
        //     expEarned = enemyManager.expToGive;
        // }


        if (enemyUnit.unitName == "Sandboy")
        {
            dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";
        }
        else if (enemyUnit.unitName == "Wolves")
        {
            dialogueText.text = "A hungry hungry " + enemyUnit.unitName + " things will get wild...";
        }
        else if (enemyUnit.unitName ==  "Thief") {
            dialogueText.text = "A sneaky " + enemyUnit.unitName + " has got you cornered!";
        }




        playerHud.SetHUD(playerUnit);
        enemyHud.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
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

    IEnumerator PlayerSwordAttack()
    {
        //damage enemy 
        dialogueText.text = "Sword slash engaged!";
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage + 15);
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
        RadNum = Random.Range(1, 4);

        yield return new WaitForSeconds(2f);

        //check if enemy is dead
        if (isDead)
        {
            

            
            //enemyManager.Instance.AddExperience(enemyExp.expToGive);
            dialogueText.text = "You earned " + expEarned + "xp!";
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            if(RadNum == 1 || RadNum == 3)
            {
                dialogueText.text = "You got a 1 More!";
                yield return new WaitForSeconds(2f);
                state = BattleState.PLAYERTURN;
                StartCoroutine(PlayerTurn());
            }
            else
            {
                //Change state based on what happened
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
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

    IEnumerator EnemyHeal()
    {
        enemyUnit.Heal(10);

        enemyHud.SetHP(enemyUnit.currentHP);
        dialogueText.text = enemyUnit.unitName + " has regained strength! Beware...";

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    IEnumerator EnemyTurn()
    {
        RadNum = Random.Range(1, 5);
        Debug.Log(RadNum);

        if (enemyUnit.currentHP >= 13)
        {
            if (RadNum == 1 || RadNum == 4)
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
                    StartCoroutine(PlayerTurn());
                }
            }
            else if (RadNum == 2 || RadNum == 5)
            {
                dialogueText.text = enemyUnit.unitName + " has healed!";

                yield return new WaitForSeconds(1f);

                StartCoroutine(EnemyHeal());

                yield return new WaitForSeconds(1f);

                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else
            {
                dialogueText.text = enemyUnit.unitName + " has used magic!";

                yield return new WaitForSeconds(1f);

                bool isDead = playerUnit.TakeDamage(enemyUnit.damage + 7);
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
                    StartCoroutine(PlayerTurn());
                }
            }
        }
        else if (enemyUnit.currentHP < 13)
        {
            RadNum = Random.Range(0, 5);
            Debug.Log(RadNum);

            if (RadNum == 1 || RadNum == 3 || RadNum == 5)
            {
                dialogueText.text = enemyUnit.unitName + " has healed!";

                yield return new WaitForSeconds(1f);

                StartCoroutine(EnemyHeal());

                yield return new WaitForSeconds(1f);

                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else if (RadNum == 0 || RadNum == 2)
            {
                dialogueText.text = enemyUnit.unitName + " has attacked!";

                yield return new WaitForSeconds(1f);

                bool isDead = playerUnit.TakeDamage(enemyUnit.damage - 3);
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
                    StartCoroutine(PlayerTurn());
                }
            }
            else
            {
                dialogueText.text = enemyUnit.unitName + " has used magic!";

                yield return new WaitForSeconds(1f);

                bool isDead = playerUnit.TakeDamage(enemyUnit.damage + 7);
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
                    StartCoroutine(PlayerTurn());
                }
            }

        }

        
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(2f);
    }


    //Function containing each condition for a battle to end
    void EndBattle()
    {
        if(state == BattleState.WON)
        {
           
            dialogueText.text = "You won the battle!";
            //yield return new WaitForSeconds(2f);
            //unloadScene();
            //SceneManager.UnloadSceneAsync("StartRoomScene");
            //transitionToBattle.
            //SceneManager.LoadSceneAsync(sceneToChangeTo);


            enemyDefeated = true;
            //gameManager

            if(enemyDefeated == true)
            {
                ExperienceManager.Instance.AddExperience(expEarned);
                if(enemyUnit.unitName == "Sandboy")
                {
                    SceneManager.LoadSceneAsync("StartRoomSceneNew");
                }
                if(enemyUnit.unitName == "Wolves")
                {
                    SceneManager.LoadSceneAsync("Scene 3 NEW");
                }
                //else if(enemyUnit.unitName == " ")
                //enemyMask.enemy.SetActive(false);
                //Destroy(enemyMask.enemy);
            }

        }
        else if(state == BattleState.LOST){
            dialogueText.text = "You were defeated!";
            SceneManager.LoadSceneAsync("GameOver");
        }
    }

    //Holds a variable for who is choosing an action
    public IEnumerator PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
        yield return new WaitForSeconds(2f);
    }

    //Public function to enable the options for party members to attack
    public void OnAttackButton()
    {
        healButton.SetActive(false);
        akeruHealButton.SetActive(false);
        attackButton.SetActive(false);
        akeruAttackButton.SetActive(true);
    }

    //Public function to enable the Akeru's attack options
    public void OnAkeruAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        akeruAttackButton.SetActive(false);
        meleeButton.SetActive(true);
        akeruSword.SetActive(true);
        akeruBlackMagic.SetActive(true);
        akeruTurn = true;
    }

    //Public function enable the melee attack button
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
            akeruSword.SetActive(false);
            akeruBlackMagic.SetActive(false);
            healButton.SetActive(true);
            akeruTurn = false;
        }
    }

    //Public function to enable the black magic button
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
            akeruSword.SetActive(false);
            attackButton.SetActive(true);
            healButton.SetActive(true);
            akeruTurn = false;
        }
    }


    public void OnSwordAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        if (akeruTurn == true)
        {
            StartCoroutine(PlayerSwordAttack());
            akeruBlackMagic.SetActive(false);
            meleeButton.SetActive(false);
            attackButton.SetActive(true);
            healButton.SetActive(true);
            akeruTurn = false;
        }
    }

    //Public function to enable the healing options
    public void OnHealButton()
    {
        healButton.SetActive(false);
        akeruHealButton.SetActive(true);
        attackButton.SetActive(false);
        dialogueText.text = "Choose a party member to heal:";

        //StartCoroutine(PlayerHeal());
    }

    //Public function to enable the heal button for Akeru
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
