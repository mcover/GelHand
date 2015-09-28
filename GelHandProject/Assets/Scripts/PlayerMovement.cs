using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	private Rigidbody2D rb;
	private bool onGround = false;

	[HideInInspector] public bool isFacingRight = false;
	[HideInInspector] public bool jumping = false;
	[HideInInspector] public bool wallJumping = false;
	public float upForce = 300f;
	public float movementForce=365f;
	public float maxSpeed=5f;
	public float jumpForce=1000f;
	public float snapForce = 2000f;
	public Transform groundCheck;
	public Transform wallL;
	public Transform wallR;
	public Transform ceilingCheck;
	[HideInInspector]
	public bool onWall = false; //do I need one for each wall?
	[HideInInspector]
	public bool onCeiling = false;
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		onWall = Physics2D.Linecast (transform.position, wallR.position, 1 << LayerMask.NameToLayer ("Ground"));
		onCeiling = Physics2D.Linecast(transform.position, ceilingCheck.position, 1 << LayerMask.NameToLayer("Ground"));


		if (Input.GetKeyDown(KeyCode.Space) && onGround)
		{
			jumping = true;
		}
		/*if (Input.GetKeyDown (KeyCode.Space) && onWall&&!onGround) {
			wallJumping = true;
		}*/


	}

	void FixedUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical"); //for use later on walls
		if (!onWall && !onCeiling&&!onWall) {
			normalMovement (horizontal);
		}
		if (onWall && horizontal<0) {

			//rb.gravityScale =0;
			onWallMovement(vertical, horizontal);
			//rb.AddForce(Vector2.up*upForce*vertical);
		}
		if (onWall && horizontal>0) {
			//Debug.Log ("R");
			//rb.gravityScale = 0;
			//rb.AddForce(Vector2.up*upForce*vertical);
			onWallMovement(vertical, horizontal);
		}
		if (horizontal > 0 && !isFacingRight)
			Flip ();
		else if (horizontal < 0 && isFacingRight)
			Flip ();

		if (jumping) {
			rb.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
			jumping = false;
		}
		/*if (wallJumping) {
			rb.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
			Debug.Log("WALL");
			wallJumping = false;
		}*/
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
		//rb.gravityScale = 0;
		if (vertical *rb.velocity.y < maxSpeed){
			rb.AddForce(Vector2.up * vertical*upForce);
		}
		if (Mathf.Abs (rb.velocity.y)> maxSpeed){
			rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y)*maxSpeed);
		}
	}
	private void onCeilingMovement(float vertical, float horizontal){
		//right and left movement, holding down should unstick you
	}

	private void Flip(){
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
