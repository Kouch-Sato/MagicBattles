using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIManager : MonoBehaviour
{
    public Slider hpSlider;
    public Image damageImage;

    public void Init(PlayerManager playerManager)
    {
        hpSlider.maxValue = playerManager.maxHP;
        hpSlider.value = playerManager.maxHP;
        damageImage.enabled = true;
        damageImage.color = Color.clear;
    }

    private void Update()
    {
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, Time.deltaTime);
    }

    public void GetDamage(int HP)
    {
        hpSlider.DOValue(HP, 0.3f);
        damageImage.color = new Color(0.8f, 0, 0, 0.5f);
    }
}
