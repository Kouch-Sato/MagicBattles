using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIManager : MonoBehaviour
{
    public Slider HPSlider;
    public Slider MPSlider;
    public Image damageImage;

    public void Init(PlayerManager playerManager)
    {
        HPSlider.maxValue = playerManager.maxHP;
        HPSlider.value = playerManager.maxHP;
        MPSlider.maxValue = playerManager.maxMP;
        MPSlider.value = playerManager.maxMP;
        damageImage.enabled = true;
        damageImage.color = Color.clear;
    }

    private void Update()
    {
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, Time.deltaTime);
    }

    public void GetDamage(int HP)
    {
        HPSlider.DOValue(HP, 0.3f);
        damageImage.color = new Color(0.8f, 0, 0, 0.5f);
    }

    public void UpdateMP(float MP)
    {
        MPSlider.DOValue(MP, 0.3f);
    }
}
