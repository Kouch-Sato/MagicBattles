using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHP = 1000;
    int HP;
    public float maxMP = 100.0f;
    float MP;
    public bool isDie;
    public PlayerUIManager playerUIManager;
    public GameObject missilePrefab;
    public GameObject rainPrefab;
    public Transform leftControllerAnchor;
    public Transform rightControllerAnchor;


    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        MP = maxMP;
        playerUIManager.Init(this);
        isDie = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            MissileAttack(rightControllerAnchor);
        }

        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            MissileAttack(leftControllerAnchor);
        }

        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            RainAttack();
        }

        // START: PCデバッグ用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MissileAttack(rightControllerAnchor);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            RainAttack();
        }
        // END: PCデバッグ用

        if (MP < maxMP)
        {
            MP += 0.1f;
            playerUIManager.MPSlider.value = MP;
        }

    }

    private void OnTriggerEnter(Collider other) {
        EnemyDamager damager = other.GetComponent<EnemyDamager>();
        if (damager)
        {
            GetDamage(damager.damage);
        }
    }

    public void GetDamage(int damage)
    {
        if (isDie)
        {
            return;
        }

        HP -= damage;
        playerUIManager.GetDamage(HP);

        if ( HP <= 0 )
        {
            HP = 0;
            Die();
        }
    }

    private void Die()
    {
        isDie = true;
        GameObject.Find("IngameSceneManager").GetComponent<IngameSceneManager>().isPlayerDie = isDie;
    }

    private void MissileAttack(Transform controllerAnchor)
    {
        if (MP >= 20)
        {
            MP -= 20.0f;
            playerUIManager.UpdateMP(MP);
            GameObject missileObject = Instantiate(missilePrefab, controllerAnchor.position, controllerAnchor.rotation) as GameObject;
            missileObject.GetComponent<Rigidbody>().AddForce(missileObject.transform.forward * 1000);
            Destroy(missileObject, 4.0f);
        }
    }

    private void RainAttack()
    {
        float instantiateRadius = 8.0f;

        float cos72 = (float)System.Math.Cos(72 * System.Math.PI / 180);
        float sin72 = (float)System.Math.Sin(72 * System.Math.PI / 180);
        float cos36 = (float)System.Math.Cos(36 * System.Math.PI / 180);
        float sin36 = (float)System.Math.Sin(36 * System.Math.PI / 180);
        Vector3 positionYAxisOffset = new Vector3(0, 1.2f, 0);

        // 正五角形の頂点を時計の12の位置において、時計回りにListに追加
        var positionList = new List<Vector3>();
        positionList.Add(transform.position + transform.forward * instantiateRadius);
        positionList.Add(transform.position + (transform.forward * cos72 + transform.right * sin72) * instantiateRadius);
        positionList.Add(transform.position + (transform.forward * (-1) * cos36 + transform.right * sin36) * instantiateRadius);
        positionList.Add(transform.position - (transform.forward * cos36 + transform.right * sin36) * instantiateRadius);
        positionList.Add(transform.position + (transform.forward * cos72 - transform.right * sin72) * instantiateRadius);

        for (int i=0; i < positionList.Count; i++)
        {
            GameObject rainObject = Instantiate(rainPrefab, positionList[i] - positionYAxisOffset, Quaternion.Euler(-90, 0, 0)) as GameObject; 
            Destroy(rainObject, 4.0f);
        }
    }
}
