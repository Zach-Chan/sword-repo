using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput; //-1 is left 1 is right

    private string playerDirection; 

    public int health;

    public string[] swordForms = {"Sword", "Thrall", "Gargoyle", "Hound", "Warden", "Knight"};

    private string _currentForm;
    public string currentForm
    {
        get
        {
            return _currentForm;
        }
        set
        {
            _currentForm = value;
        }
    }

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public bool isInvincible; //is it invincible?
    private float invincibleTimer=0.0f; //the timer for invincibility
    private float invincibleDuration=5.0f; //the duration of the invincibility

    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        currentForm = "Sword";
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        setDirection(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        Debug.Log("Facing: " + playerDirection);
    }

    void setDirection(float input) //check moveInput and set string to Left or Right
    {
        if(input == -1)
        {
            playerDirection = "Left";
        }

        if(input == 1)
        {
            playerDirection = "Right";
        }
    }

    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
            
        }
        else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetKey(KeyCode.Space) && isJumping==true)
        {
            if(jumpTimeCounter > 0)
            {

            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

            if(Input.GetKeyUp(KeyCode.Space)){
                isJumping = false;
            }

        }

    }

    void checkHealth()
    {
        if(health <= 0)
        {
            die();
        }
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

    public void takeDamage(int damage)
    {
        health -= damage;
        isInvincible = true;
        rb.AddRelativeForce(new Vector3(-1,1,0));
        // rb.AddForce(new Vector2)
    }

    void die()
    {
        Debug.Log("Player has died");
    }

}
