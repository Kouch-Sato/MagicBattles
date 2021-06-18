using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int maxHP;
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetDamage(int damage)
    {
        Debug.Log("aaaaaa");
        hp -= damage;
        Debug.Log("HP:" + hp);

        if (hp < 0)
        {
            hp = 0;
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
