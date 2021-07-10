using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonManager : MonoBehaviour
{
    public Collider weaponCollider;

    Transform target;
    NavMeshAgent agent;
    Animator animator;
    public GameObject magicSwordPrefab;
    public GameObject magicSwordInstantiateSound;
    private float timeOutForMagicSword;
    private float timeElapsedForMagicSword = 0;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        animator = GetComponent<Animator>();
        timeOutForMagicSword = new System.Random().Next(10, 14);

        HideColliderWeapon();

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        transform.LookAt(target.position);
        animator.SetFloat("Distance", agent.remainingDistance);

        timeElapsedForMagicSword += Time.deltaTime;
        // 遠距離攻撃はplayerとの距離が離れている場合のみ実施
        if((timeElapsedForMagicSword >= timeOutForMagicSword) && (agent.remainingDistance >= 15))
        {
            animator.SetTrigger("MagicSword");
            var magicSwordList = InstantiateMagicSword();
            // 引数のintervalは、skeltonが剣を突き出すアニメーションに依存
            StartCoroutine (ReleaseAndDestroyMagicSword(magicSwordList, 3.6f));

            timeElapsedForMagicSword = 0.0f;
            timeOutForMagicSword = new System.Random().Next(10, 14);
        }
    }

    List<GameObject> InstantiateMagicSword()
    {
        GameObject soundObject = Instantiate(magicSwordInstantiateSound, transform.position, transform.rotation) as GameObject;
        Destroy(soundObject, 2.0f);

        Vector3 positionYAxisOffset = new Vector3(0.0f, 2.5f, 0.0f);
        int instantiateRadius = 3;
        Vector3 targetPosition = target.transform.position + new Vector3(0, -0.5f, 0);

        var directionList = new List<Vector3>();
        directionList.Add((transform.forward + transform.right).normalized);
        directionList.Add((transform.forward * 2 + transform.right).normalized);
        directionList.Add(transform.forward);
        directionList.Add((transform.forward * 2 - transform.right).normalized);
        directionList.Add((transform.forward - transform.right).normalized);

        var magicSwordList = new List<GameObject>();

        for (int i=0; i < directionList.Count; i++)
        {
            Vector3 position = transform.position + directionList[i] * instantiateRadius + positionYAxisOffset;
            Quaternion rot = Quaternion.AngleAxis(30 - 15 * i, transform.up);
            GameObject magicSword = Instantiate(magicSwordPrefab, position, transform.rotation);
            magicSword.transform.LookAt(targetPosition);
            magicSword.transform.rotation *= rot;
            magicSwordList.Add(magicSword);
        }

        return magicSwordList;
    }

    IEnumerator ReleaseAndDestroyMagicSword(List<GameObject> magicSwordList, float interval)
    {
        yield return new WaitForSeconds(interval);
        foreach(GameObject magicSword in magicSwordList)
        {
            magicSword.GetComponent<Rigidbody>().AddForce(magicSword.transform.forward * 2000);
            Destroy(magicSword, 3.0f);
        }
    }

    public void ShowColliderWeapon()
    {
        weaponCollider.enabled = true;
    }

    public void HideColliderWeapon()
    {
        weaponCollider.enabled = false;
    }
}
