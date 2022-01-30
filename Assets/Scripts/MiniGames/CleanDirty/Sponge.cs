using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponge : MonoBehaviour
{
	private Rigidbody MyRigidBody;

	public float MoveSpeed;
	public float CleanPower;

	public int ScreenWidth;
	public int ScreenHeight;

	public AudioSource MySound;

	// Start is called before the first frame update
	void Start()
	{
		MyRigidBody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		WrapScreen();
	}

	private void FixedUpdate()
	{
		UpdateMoveInput();
	}

	/// <summary>
	/// The sponge will move in the cardinal directions with WASD
	/// </summary>
	private void UpdateMoveInput()
	{
		if (Input.GetKey(KeyCode.W))
		{
			MyRigidBody.AddForce(0f, MoveSpeed, 0f);
		}
		if (Input.GetKey(KeyCode.A))
		{
			MyRigidBody.AddForce(-MoveSpeed, 0f, 0f);
		}
		if (Input.GetKey(KeyCode.S))
		{
			MyRigidBody.AddForce(0f, -MoveSpeed, 0f);
		}
		if (Input.GetKey(KeyCode.D))
		{
			MyRigidBody.AddForce(MoveSpeed, 0f, 0f);
		}

		float speed = MyRigidBody.velocity.magnitude;
		MySound.volume = Mathf.Min(speed / 10f, 0.3f);
	}

	/// <summary>
	/// This will wrap the sponge around the screen
	/// </summary>
	private void WrapScreen()
	{
		Vector3 position = transform.position;
		if (position.x > ScreenWidth * 0.5f)
		{
			position.x = -position.x + 0.25f;
		}
		if (position.x < -ScreenWidth * 0.5f)
		{
			position.x = -position.x - 0.25f;
		}
		if (position.y > ScreenHeight * 0.5f)
		{
			position.y = -position.y + 0.25f;
		}
		if (position.y < -ScreenHeight * 0.5f)
		{
			position.y = -position.y - 0.25f;
		}
		transform.position = position;
	}
}
