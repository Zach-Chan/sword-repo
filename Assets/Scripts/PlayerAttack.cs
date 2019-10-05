using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{

    private float defaultAttackTimer = 1.0f;
    private float currentAttackTimer;
    private bool attackCooldown;

    private bool attackAlive = false;
    private float attackAliveDuration = 0.3f;// how long the attack hitbox is active for
    private float attackAliveTimer = 0.0f; //the timer for the attack active hitbox
    public int damage;

    public GameObject player;

    public BoxCollider2D attackCollider;

    // Start is called before the first frame update
    void Start()
    {
        // attackCollider = this.gameObject.GetComponent("AttackCollider") as BoxCollider2D;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        MakeAttackAlive();
        ResetAttackState();
    }

    void Attack()
    {
        if(Input.GetButtonDown("Attack") && attackCooldown == false)
        {
            currentAttackTimer = defaultAttackTimer;
            attackAlive = true;
            
            Debug.Log("Attack" + " Current timer = " + currentAttackTimer + " attackCooldown " + attackCooldown);
            attackCooldown = true;
        }
    }

    void ResetAttackState()
    {
        if(attackCooldown)
        {
            currentAttackTimer -= Time.deltaTime;
            if(currentAttackTimer <=0f)
            {
                attackCooldown = false;
                currentAttackTimer = defaultAttackTimer;
            }
        }
    }

    void MakeAttackAlive()
    {
        if(attackAlive)
        {
            attackAliveTimer += Time.deltaTime;   
            attackCollider.enabled = true;
            // attackCollider.
            
        }
        else
        {
         attackCollider.enabled = false;   
        }

        if(attackAliveTimer > attackAliveDuration)
        {
            attackAliveTimer -= attackAliveDuration;
            Time.timeScale = 1.0f;
            attackAlive = false;
        }
    }
}
