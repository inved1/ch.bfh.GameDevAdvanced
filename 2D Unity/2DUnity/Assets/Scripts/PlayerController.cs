using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float maxSpeed = 400f;
    bool facingRight = true;
    Rigidbody2D rigidbody2D;
    Animator anim;

    bool bGrounded = false;
    bool bCrouch = false;
    bool bRoll = false;
    bool bJump = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;


    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
    void Update () {

        bGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        bCrouch = Input.GetKeyDown(KeyCode.C);
        bRoll = Input.GetKeyDown(KeyCode.R);
        bJump = Input.GetButtonDown("Jump");

        

        anim.SetBool("Grounded", bGrounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        if (bGrounded)
        {
            if (bRoll) { Roll(); }
            if (bCrouch) { Crouch(); }
            if (bJump) { Jump(); }
        }


        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move),0.1f, Time.deltaTime);
        rigidbody2D.velocity = new Vector2( move * Time.deltaTime * maxSpeed* (0.1f + Time.deltaTime), 
                                            rigidbody2D.velocity.y);
        if (move < 0 && facingRight ||
            move > 0 && !facingRight)
            Flip();
    }
    
    // flip the character by scaling it by -1 along the x axis
    void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    void Jump()
    {
        anim.SetBool("Grounded", false);
        anim.SetBool("Crouch", false);
        anim.SetBool("Roll", false);
        
        rigidbody2D.AddForce(new Vector2(0, jumpForce));

    }

    void Roll()
    {
        anim.SetBool("Grounded", false);
        anim.SetBool("Crouch", false);
        anim.SetBool("Roll", true);

    }

    void Crouch()
    {
        anim.SetBool("Grounded", false);

        anim.SetBool("Roll", false);
        anim.SetBool("Crouch", true);
    }
}