using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyUIManager : MonoBehaviour
{
    public Slider HPSlider;
    public GameObject skinBody;
    
    public void Init(EnemyManager enemyManager)
    {
        HPSlider.maxValue = enemyManager.maxHP;
        HPSlider.value = enemyManager.maxHP;
        var bodyHeight = skinBody.GetComponent<SkinnedMeshRenderer>().bounds.size.y;
        transform.position = transform.position + new Vector3(0, bodyHeight + 0.2f, 0);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void GetDamage(int HP)
    {
        HPSlider.DOValue(HP, 0.3f);
    }
}
