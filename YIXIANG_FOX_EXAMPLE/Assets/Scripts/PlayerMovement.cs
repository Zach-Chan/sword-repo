using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  
    [SerializeField] private Collider2D m_CrouchDisableCollider;
    [SerializeField] private Collider2D m_CrouchDisableCollider2;
    [SerializeField] private Collider2D m_CrouchDisableCollider3;


    public enum States
    {
        Sword,
        Possess,
    }

    public States state;
    public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
    public bool possessing = false;
    public bool state2 = false;
    bool changed = false;

	// Update is called once per frame
	void Update () {

        animator.SetBool("possessing", true);


        switch (state)
        {
            case States.Sword:


                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

                if (Input.GetButtonDown("Jump"))
                {
                    jump = true;
                    animator.SetBool("IsJumping", true);
                }

                if (Input.GetButtonDown("Crouch"))
                {
                    crouch = true;
                }
                else if (Input.GetButtonUp("Crouch"))
                {
                    crouch = false;
                }

                m_CrouchDisableCollider3.enabled = false;

                break;

            case States.Possess:

                runSpeed = 100;
                changed = true;
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

                m_CrouchDisableCollider3.enabled = true;

                if (m_CrouchDisableCollider != null && m_CrouchDisableCollider2 != null) { 
                    m_CrouchDisableCollider.enabled = false;
                m_CrouchDisableCollider2.enabled = false;

        }


        break;
            default:
                break;
        }

        if (changed = false)
        {
            state = PlayerMovement.States.Sword;
        }

      


        if (possessing == true && Input.GetButton("Fire1"))
        {

            animator.SetBool("possessing", false);
            state = PlayerMovement.States.Possess;


        }


    }

	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}


    public void AlertObservers(string message)
    {
        if (message.Equals("AttackAnimationEnded"))
        {
            state2 = true;
            animator.SetBool("state2", true);

            // Do other things based on an attack ending.
        }
    }



}
