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
    public GameObject rightHandFireOrbitSphere;
    private OVRInput.Controller leftCon;
    private OVRInput.Controller rightCon;
    private bool holdMissile = false;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        MP = maxMP;
        playerUIManager.Init(this);
        isDie = false;
        rightCon = OVRInput.Controller.RTouch;
        leftCon = OVRInput.Controller.LTouch;
    }

    // Update is called once per frame
    void Update()
    {
        if (HoldMissileTrigger())
        {
            holdMissile = true;
            rightHandFireOrbitSphere.SetActive(true);
        }

        if (holdMissile)
        {
            OVRInput.SetControllerVibration(0.3f, 0.3f, rightCon);
        }
        
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) && holdMissile)
        {
            MissileAttack(rightControllerAnchor);
            holdMissile = false;
            rightHandFireOrbitSphere.SetActive(false);
            StartCoroutine (MagicReleaseVibration());

            IEnumerator MagicReleaseVibration()
            {
                OVRInput.SetControllerVibration(1, 1, rightCon);
                yield return new WaitForSeconds (0.2f);
                OVRInput.SetControllerVibration(0, 0, rightCon);
            }
        }

        if (RainAttackTrigger())
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

    private bool HoldMissileTrigger()
    {
        bool trigger = OVRInput.Get(OVRInput.RawButton.RIndexTrigger);
        var rAngularAcc = OVRInput.GetLocalControllerAngularAcceleration(rightCon);
        if (rAngularAcc.magnitude >= 200 && trigger)
        {
            return true;
        }
        return false;
    }

    private void MissileAttack(Transform controllerAnchor)
    {
        float MPAmount = 20.0f;
        if (MP >= MPAmount)
        {
            MP -= MPAmount;
            playerUIManager.UpdateMP(MP);

            GameObject missileObject = Instantiate(missilePrefab, controllerAnchor.position, controllerAnchor.rotation) as GameObject;
            missileObject.GetComponent<Rigidbody>().AddForce(missileObject.transform.forward * 1000);
            Destroy(missileObject, 4.0f);
        }
    }

    private bool RainAttackTrigger()
    {
        bool rTrigger = OVRInput.Get(OVRInput.RawButton.RIndexTrigger);
        bool lTrigger = OVRInput.Get(OVRInput.RawButton.LIndexTrigger);
        Vector3 rightVelocity = OVRInput.GetLocalControllerVelocity(rightCon);
        Vector3 leftVelocity = OVRInput.GetLocalControllerVelocity(leftCon);
        
        if (rTrigger && lTrigger)
        {
            if (rightVelocity.y <= -2 && leftVelocity.y <= -2)
            {
                return true;
            }
        }
        return false;
    }

    private void RainAttack()
    {   
        float MPAmount = 51;
        if (MP < MPAmount)
        {
            return;
        }

        MP -= MPAmount;
        float instantiateRadius = 10.0f;

        float cos60 = 1.0f / 2.0f;
        float sin60 = 1.732f / 2.0f;
        Vector3 positionYAxisOffset = new Vector3(0, 1.2f, 0);

        Vector3 tp = transform.position;
        Vector3 tf = transform.forward;
        Vector3 tr = transform.right;

        // 正六角形の頂点を時計の12の位置において、時計回りにListに追加
        var positionList = new List<Vector3>();
        positionList.Add(tp);
        positionList.Add(tp + tf * instantiateRadius);
        positionList.Add(tp + (tf * cos60 + tr * sin60) * instantiateRadius);
        positionList.Add(tp + (tf * (-1) * cos60 + tr * sin60) * instantiateRadius);
        positionList.Add(tp - tf * instantiateRadius);
        positionList.Add(tp + (tf * (-1) * cos60 + tr * (-1) * sin60) * instantiateRadius);
        positionList.Add(tp + (tf * cos60 - tr * sin60) * instantiateRadius);

        for (int i=0; i < positionList.Count; i++)
        {
            GameObject rainObject = Instantiate(rainPrefab, positionList[i] - positionYAxisOffset, Quaternion.Euler(-90, 0, 0)) as GameObject; 
            Destroy(rainObject, 4.0f);
        }
    }
}
