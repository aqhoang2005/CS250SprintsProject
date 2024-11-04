using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
   // public int currentExperience;
    //public int maxExperience;
    public int expToGive = 300;
    public int damage;
    public int blackMagic;
    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0) {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP) { 
            currentHP = maxHP;
        }
    }

    /*
    private void OnEnable()
    {
        //Subscribe to the event
        ExperienceManager.Instance.OnExperienceChanged += HandleExperienceChange;
    }

    private void OnDisable()
    {
        //Unsubscribe to the event
        ExperienceManager.Instance.OnExperienceChanged -= HandleExperienceChange;
    }

    private void HandleExperienceChange(int newExperience)
    {
        currentExperience += newExperience;

        if (currentExperience >= maxExperience) {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        maxHP += 10;
        currentHP = maxHP;

        unitLevel ++;
        currentExperience = 0;
        maxExperience += 100;
    }
    */
}
