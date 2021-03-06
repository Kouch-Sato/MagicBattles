using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHP = 1000;
    public int HP;
    public float maxMP = 100.0f;
    float MP;
    public bool isDie;
    public PlayerUIManager playerUIManager;
    public GameObject missilePrefab;
    public GameObject rainPrefab;
    public Transform leftControllerAnchor;
    public Transform rightControllerAnchor;
    public GameObject leftHandFireOrbitSphere;
    public GameObject rightHandFireOrbitSphere;
    public Transform PlayerSight;
    private OVRInput.Controller leftCon;
    private OVRInput.Controller rightCon;
    private bool leftHoldMissile = false;
    private bool rightHoldMissile = false;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        MP = 0.0f;
        playerUIManager.Init(this);
        isDie = false;
        rightCon = OVRInput.Controller.RTouch;
        leftCon = OVRInput.Controller.LTouch;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {
            leftHoldMissile = true;
            leftHandFireOrbitSphere.SetActive(true);
        }

        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            rightHoldMissile = true;
            rightHandFireOrbitSphere.SetActive(true);
        }

        if (leftHoldMissile)
        {
            if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
            {
                MissileAttack(leftControllerAnchor, leftCon);
                leftHoldMissile = false;
                leftHandFireOrbitSphere.SetActive(false);
            }
        }

        if (rightHoldMissile)
        {
            if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
            {
                MissileAttack(rightControllerAnchor, rightCon);
                rightHoldMissile = false;
                rightHandFireOrbitSphere.SetActive(false);
            }
        }

        if (RainAttackTrigger())
        {
            RainAttack();
        }

        // START: PC???????????????
        if (Input.GetKeyDown(KeyCode.K))
        {
            RainAttack();
        }
        // END: PC???????????????

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

        if (HP <= 0)
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

    private void MissileAttack(Transform controllerAnchor, OVRInput.Controller controller)
    {
        StartCoroutine(MagicReleaseVibration());
        IEnumerator MagicReleaseVibration()
        {
            OVRInput.SetControllerVibration(1, 1, controller);
            yield return new WaitForSeconds(0.2f);
            OVRInput.SetControllerVibration(0, 0, controller);
        }

        GameObject missileObject = Instantiate(missilePrefab, controllerAnchor.position, controllerAnchor.rotation) as GameObject;

        Vector3 controllerAcc = OVRInput.GetLocalControllerAcceleration(controller);
        if (controllerAcc.magnitude > 2)
        {
            missileObject.GetComponent<Rigidbody>().AddForce(PlayerSight.transform.forward * 1000);
        }
        else
        {
            missileObject.GetComponent<Rigidbody>().useGravity = true;
        }

        Destroy(missileObject, 4.0f);
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
        float MPAmount = 100;
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

        // ?????????????????????????????????12???????????????????????????????????????List?????????
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
