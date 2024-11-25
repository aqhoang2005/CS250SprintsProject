using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;

    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChanged;
    //public static event Action<int> OnExperienceChanged;

    public int unitLevel = 1;
    public int currentExperience;
    public int maxExperience;
    public int maxHealth;
    public int currentHealth;
    //public int expToGive;

    Unit player;
    BattleSystem battleSystem;

    //Use the awake function to check if the game object
    //is used once in a single scene
    private void Awake()
    {
        
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int getLevelNum()
    {
        return unitLevel;
    }

    public void AddExperience(int amount)
    {
        //Using the question mark here prevents a null value fronm being passed in
        OnExperienceChanged?.Invoke(amount);
    }

    private void OnEnable()
    {
        //Subscribe to the event
        OnExperienceChanged += HandleExperienceChange;
    }

    private void OnDisable()
    {
        //Unsubscribe to the event
        OnExperienceChanged -= HandleExperienceChange;
    }

    private void HandleExperienceChange(int newExperience)
    {
        currentExperience += newExperience;

            //AddExperience(300);
            // OnExperienceChanged(currentExperience);
            if (currentExperience == 0)
            {
                Debug.Log("You gained xp");
                LevelUp();
            }
            else
            {
                Debug.Log("Uh-uh-uh! This ain't working.");
            }
    }

    private void LevelUp()
    {
        maxHealth += 10;
        //battleSystem.playerPrefab.
        //player.currentHP = player.maxHP;
        currentHealth += maxHealth;

        unitLevel++;
        currentExperience = 0;
        maxExperience += 100;
    }
}