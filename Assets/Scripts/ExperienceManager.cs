using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;

    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChanged;

    public int unitLevel;
    public int currentExperience;
    public int maxExperience;
    public int maxHealth;
    public int currentHealth;
    //public int expToGive;

    Unit player;


    //Use the awake function to check if the game object
    //is used once in a single scene
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        maxHealth += 10;
        player.currentHP = player.maxHP;

        unitLevel++;
        currentExperience = 0;
        maxExperience += 100;
    }
}
