using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelName;
    public Slider hpSilder;

    public void SetHUD(Unit Unit)
    {
        nameText.text = Unit.unitName;
        levelName.text = "Lvl " + Unit.unitLevel;
        hpSilder.maxValue = Unit.maxHP;
        hpSilder.value = Unit.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSilder.value = hp;
    }
}
