using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int health = 3;
    public int damage;
    public string[] enemyTypes = {"Sword", "Thrall", "Gargoyle", "Hound", "Warden", "Knight"};
    private string _enemyType;

    public bool isInvincible = false; // is it invincible?
    private float invincibleTimer=0.0f; //the timer for invincibility
    private float invincibleDuration=1.0f; //the duration of the invincibility
    
    private float thrust = 3f; // amount of thrust/knockback when hit

    public string enemyType
    {
        get
        {
            return _enemyType;
        }
        set
        {
            _enemyType = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    // Update is called once per frame
    void Update()
    {
        checkDamage();
        checkInvincible();
    }

    void checkDamage()
    {
        if(health <= 0)
        {
            die();
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        isInvincible = true;
        rb.AddForce(new Vector2(-1, 1) * thrust, ForceMode2D.Impulse);
    }

    void checkInvincible()
    {
        if(isInvincible)
        {
            Debug.Log("Invincibility: " + invincibleTimer);
            invincibleTimer += Time.deltaTime;
        }
        
        if(invincibleTimer > invincibleDuration)
        {
            invincibleTimer -= invincibleDuration;
            Time.timeScale = 1.0f;
            isInvincible = false;
            Debug.Log("No longer invincible");
        }
    }

    // void OnTriggerStay2D(Collider2D other)
    // {
    //     if(other.gameObject.tag == "PlayerAttack")
    //     {
    //         health -= 1;
    //         Debug.Log("Enemy damaged now at " + health +" hp");
    //     }
    // }

    void die()
    {
        //play animation stuff here
        Destroy(this.gameObject);
        Debug.Log("Dead");
    }
}
