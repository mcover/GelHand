using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	private Rigidbody2D rb;
	private bool onGround = false;

	[HideInInspector] public bool isFacingRight = true;
	[HideInInspector] public bool jumping = false;

	public float movementForce=365f;
	public float maxSpeed=5f;
	public float jumpForce=1000f;
	public float snapForce = 2000f;
	public Transform groundCheck;
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		Debug.Log (onGround);
		
		if (Input.GetKeyDown(KeyCode.Space) && onGround)
		{
			jumping = true;
			Debug.Log("jump");
		}
	}

	void FixedUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical"); //for use later on walls

		if (horizontal * rb.velocity.x < maxSpeed) {
			rb.AddForce (Vector2.right * horizontal * movementForce);
		}
		if (Mathf.Abs (rb.velocity.x) > maxSpeed) {
			rb.velocity = new Vector2(Mathf.Sign (rb.velocity.x)*maxSpeed,rb.velocity.y);
		}
		if (jumping) {
			rb.AddForce(new Vector2(0f, jumpForce));
			jumping = false;
		}
	}
			                      
}
