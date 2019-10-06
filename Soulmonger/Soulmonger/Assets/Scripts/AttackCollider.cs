using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{

    public GameObject Enemy;
    public GameObject Player;
    public int damage;

    // Start is called before the first frame update
    void Awake()
    {
        damage = Player.GetComponent<PlayerAttack>().damage;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }   

    void checkCollision(Collider2D other)
    {
        Debug.Log("AttackCollider hit an enemy");
        if(other.tag == "Enemy" && other.GetComponent<EnemyController>().isInvincible == false)
        {
            other.GetComponent<EnemyController>().takeDamage(damage);
            Debug.Log("Enemy damaged now at " + other.GetComponent<EnemyController>().health +" hp");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        checkCollision(other);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        checkCollision(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        checkCollision(other);
    }

}
