using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(PlayerManager playerManager)
    {
        hpSlider.maxValue = playerManager.maxHP;
        hpSlider.value = playerManager.maxHP;
    }

    public void UpdateHP(int HP)
    {
        hpSlider.DOValue(HP, 0.3f);
    }
}
