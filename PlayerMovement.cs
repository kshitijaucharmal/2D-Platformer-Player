using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]private float speed = 10f;
	[Header("Jump Controls")]
	[SerializeField]private float jumpForce = 300f;
	[SerializeField]private Transform feetPos;
	[SerializeField]private float groundCheckRadius = 0.01f;
	[SerializeField]private LayerMask whatIsGround;
	[SerializeField]private float jumpStopFactor = 0.3f;

	private Rigidbody2D rb;
	private float _speed;
	private float movement;
	private bool isGrounded = true;
	private bool isJumping = false;
	private SpriteRenderer sr;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
	}

    // Start is called before the first frame update
    void Start(){
        _speed = speed;
    }

    // Update is called once per frame
    void Update(){
        movement = Input.GetAxisRaw("Horizontal");

        if(movement > 0) sr.flipX = false;
        if(movement < 0) sr.flipX = true;

        isGrounded = Physics2D.OverlapCircleAll(feetPos.position, groundCheckRadius, whatIsGround).Length > 0;

        if(Input.GetButtonDown("Jump") && isGrounded){
        	isJumping = true;
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0.01f){
        	rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpStopFactor);
        }
    }

    void FixedUpdate(){
    	rb.velocity = new Vector2(movement * speed * Time.fixedDeltaTime, rb.velocity.y);

    	if(isJumping){
    		rb.AddForce(transform.up * jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    		isJumping = false;
    	}
    }
}
