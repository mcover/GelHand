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
	public Transform wallL;
	public Transform wallR;
	public Transform ceilingCheck;
	[HideInInspector]
	public bool onWallR = false; //do I need one for each wall?
	[HideInInspector]
	public bool onWallL = false;
	[HideInInspector]
	public bool onCeiling = false;
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		onWallL = Physics2D.Linecast(transform.position, wallL.position, 1<< LayerMask.NameToLayer("Ground"));
		onWallR = Physics2D.Linecast (transform.position, wallR.position, 1 << LayerMask.NameToLayer ("Ground"));
		onCeiling = Physics2D.Linecast(transform.position, ceilingCheck.position, 1 << LayerMask.NameToLayer("Ground"));


		if (Input.GetKeyDown(KeyCode.Space) && onGround)
		{
			jumping = true;
		}
	}

	void FixedUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical"); //for use later on walls
		if (!onWallR && !onCeiling&&!onWallL) {
			normalMovement (horizontal);
		}
		if (onWallL && Input.GetKeyDown(KeyCode.A)==true) {
			//rb.gravityScale = 0;
			rb.AddForce(Vector2.up*movementForce*vertical);
		}
		if (onWallR && horizontal>0) {
			//rb.gravityScale = 0;
			rb.AddForce(Vector2.up*movementForce*vertical);
		}

		if (jumping) {
			rb.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
			jumping = false;
		}
	}

	private void normalMovement(float horizontal){
		if (horizontal * rb.velocity.x < maxSpeed) {
			rb.AddForce (Vector2.right * horizontal * movementForce);
		}
		if (Mathf.Abs (rb.velocity.x) > maxSpeed) {
			rb.velocity = new Vector2(Mathf.Sign (rb.velocity.x)*maxSpeed,rb.velocity.y);
		}
	}
	private void onWallMovement(float vertical, float horizontal){
		//up and down movement, holding the opposite force long enough should unstick you
		rb.gravityScale = 0;
		if (vertical *rb.velocity.y < maxSpeed){
			rb.AddForce(Vector2.up * vertical *movementForce);
		}
		if (Mathf.Abs (rb.velocity.y)> maxSpeed){
			rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y)*maxSpeed);
		}
	}
	private void onCeilingMovement(float vertical, float horizontal){
		//right and left movement, holding down should unstick you
	}
			                      
}
