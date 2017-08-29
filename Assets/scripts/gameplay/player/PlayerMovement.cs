using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (PlayerState))]
public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float walkSpeed = 10.0f;
	[SerializeField] private float runningSpeed = 20.0f;
	[SerializeField] [Range(0.0f, 100.0f)] private float staminaReductionScaleWhileRunning = 1.0f;
	[SerializeField] [Range(0.0f, 100.0f)]  private float staminaRecoveryScaleWhileWalking = 1.0f;
	[SerializeField] private float jumpForce = 250.0f;
	[SerializeField] private float rotationSpeed = 1.0f;
	[SerializeField] private float verticalRotationMin = -90.0f;
	[SerializeField] private float verticalRotationMax = 90.0f;
	[SerializeField] private bool isOnGround = false;
	[SerializeField] private float stunRecoveryTime = 1.0f;
	[SerializeField] private bool lockCursorAtStart = true;

	private PlayerState myPlayerState;
	private Rigidbody myRigidbody;
	private CapsuleCollider myCapsuleCollider;
	private bool onStun;
	private bool jumpAxisInUse;
	private bool recoveryStaminaEnabled = false;

	void Start () {
		myPlayerState = gameObject.GetComponent<PlayerState> ();
		myRigidbody = gameObject.GetComponent<Rigidbody> ();
		myCapsuleCollider = gameObject.GetComponent<CapsuleCollider> ();

		if (lockCursorAtStart) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

		onStun = false;

		jumpAxisInUse = false;
	}

	void FixedUpdate () {

		CheckIfGrounded ();

		if (myPlayerState.isAlive){
			if (onStun == false) {
				ProcessWalkMovement ();
				ProcessJump ();
			}
		}

		ProcessRotation ();

		//Debug
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	private void CheckIfGrounded(){
		float distanceToGround = myCapsuleCollider.bounds.extents.y;
		isOnGround = Physics.Raycast (transform.position, Vector3.up * -1.0f, distanceToGround + 0.1f);
	}

	private void ProcessWalkMovement(){
		
		float horizontalAxis = Input.GetAxis ("Horizontal");
		float verticalAxis = Input.GetAxis ("Vertical");
		float fire3Axis = Input.GetAxis ("Fire3"); //running

		if (horizontalAxis == 0.0f && verticalAxis == 0.0f) {
			recoveryStaminaEnabled = true;
		}

		float movementSpeed = 0.0f;

		if (fire3Axis > 0.0f && myPlayerState.GetStaminaState().isTired == false) {
			movementSpeed = runningSpeed;

			myPlayerState.GetStaminaState().UpdateStaminaPoints (staminaReductionScaleWhileRunning * Time.fixedDeltaTime * -1.0f);

			recoveryStaminaEnabled = false;

		}else{
			
			movementSpeed =  walkSpeed;

			if (recoveryStaminaEnabled) {
				myPlayerState.GetStaminaState().UpdateStaminaPoints (staminaRecoveryScaleWhileWalking * Time.fixedDeltaTime);
			}
		}



		//velocity update

		Vector3 gravityAcceleration = new Vector3 (0.0f, myRigidbody.velocity.y, 0.0f);

		Vector3 movementOnForward = transform.forward * verticalAxis * movementSpeed;

		Vector3 movementOnRight = transform.right * horizontalAxis * movementSpeed;

		myRigidbody.velocity = movementOnForward + movementOnRight + gravityAcceleration;

	}

	private void ProcessJump(){

		float jumpAxis = Input.GetAxis ("Jump");

		if (jumpAxis != 0.0f && isOnGround == true && jumpAxisInUse == false) {
			jumpAxisInUse = true;
			FXAudio.PlayClip ("Jump");
			myRigidbody.AddForce (Vector3.up * jumpForce);
		}

		jumpAxisInUse = jumpAxis > 0.0f;
	}

	private void ProcessRotation(){

		//rotates entire player
		float mouseXAxis = Input.GetAxis ("Mouse X");
		Quaternion playerRotation = transform.localRotation;
		playerRotation *= Quaternion.Euler (0.0f , mouseXAxis * rotationSpeed, 0.0f);
		transform.localRotation = playerRotation;

		//rotates camera but player forward still points to the same direction
		float mouseYAxis = Input.GetAxis ("Mouse Y");
		Quaternion cameraRotation = Camera.main.transform.localRotation;
		cameraRotation *= Quaternion.Euler (mouseYAxis * rotationSpeed * -1, 0.0f, 0.0f);
		cameraRotation = ClampRotationAroundXAxis (cameraRotation);
		Camera.main.transform.localRotation = cameraRotation;

	}

	Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

		angleX = Mathf.Clamp (angleX, verticalRotationMin, verticalRotationMax);

		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}

	public void ReceiveForce(Vector3 force) {
		onStun = true;
		myRigidbody.AddForce (force);
		StopCoroutine (RecoverFromStun());
		StartCoroutine (RecoverFromStun());
	}

	public IEnumerator RecoverFromStun() {
		yield return new WaitForSeconds (stunRecoveryTime);
		onStun = false;
	}
}
