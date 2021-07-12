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
    public GameObject magicPrefab;
    public GameObject rainMagicPrefab;
    public Transform leftControllerAnchor;
    public Transform rightControllerAnchor;
    public Transform PlayerSight;


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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RightAttack();
        }

        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            OVRInput.Controller RightCon = OVRInput.Controller.RTouch;
            Vector3 accRight = OVRInput.GetLocalControllerAcceleration(RightCon);

            if (accRight.magnitude > 2)
            {
                RightAttack();
            }
        }

        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
        {
            OVRInput.Controller LeftCon = OVRInput.Controller.LTouch;
            Vector3 accLeft = OVRInput.GetLocalControllerAcceleration(LeftCon);

            if (accLeft.magnitude > 2)
            {
                LeftAttack();
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            RainAttack();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            // TODO:仕様検討
            // RainAttack();
        }

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

    private void LeftAttack()
    {
        GameObject magicGameObject = Instantiate(magicPrefab, leftControllerAnchor.position, leftControllerAnchor.rotation) as GameObject;
        magicGameObject.GetComponent<Rigidbody>().AddForce(PlayerSight.transform.forward * 1000);
    }

    private void RightAttack()
    {
        if (MP >= 20)
        {
            MP -= 20.0f;
            playerUIManager.UpdateMP(MP);
            GameObject magicGameObject = Instantiate(magicPrefab, rightControllerAnchor.position, rightControllerAnchor.rotation) as GameObject;
            magicGameObject.GetComponent<Rigidbody>().AddForce(PlayerSight.transform.forward * 1000);
        }
    }

    private void RainAttack()
    {
        Instantiate(rainMagicPrefab, transform.position + new Vector3(0, -1.0f, 0), Quaternion.Euler(-90, 0, 0));
    }
}
