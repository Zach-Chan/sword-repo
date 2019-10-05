using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{

    private Rigidbody2D rbody;
    private Animator anim;

    //public vars
    public float speed;
    public float jumpSpeed;

    //private bool vars
    private bool inGround;
    private bool m_FacingRight;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //gets "Animator" component from object (can be seen in Inspector)
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
