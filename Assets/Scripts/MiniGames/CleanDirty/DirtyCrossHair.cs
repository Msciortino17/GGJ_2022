using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyCrossHair : MonoBehaviour
{
	private Rigidbody MyRigidBody;
	public SpriteRenderer MySprite;
	private Animator MyAnimator;

	public float MoveSpeed;

	public int ScreenWidth;
	public int ScreenHeight;

	private float spawnDirtTimer;
	private float spawnDirtSpeed;
	public int MaxNearbySpots;
	public GameObject DirtSpotPrefab;
	public LayerMask DirtLayer;
	public CleanDirtyMiniGame MiniGameManagerRef;
	public AudioSource PigSound;
	private float soundTimer;

	// Start is called before the first frame update
	void Start()
	{
		MyRigidBody = GetComponent<Rigidbody>();
		MyAnimator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		UpdateMoveInput();
	}

	// Update is called once per frame
	void Update()
	{
		WrapScreen();
		UpdatePlaceDirt();
		UpdateSound();
	}

	/// <summary>
	/// The sponge will move in the cardinal directions with WASD
	/// </summary>
	private void UpdateMoveInput()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			MyRigidBody.AddForce(0f, MoveSpeed, 0f);
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			MyRigidBody.AddForce(-MoveSpeed, 0f, 0f);
			MySprite.flipX = true;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			MyRigidBody.AddForce(0f, -MoveSpeed, 0f);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			MyRigidBody.AddForce(MoveSpeed, 0f, 0f);
			MySprite.flipX = false;
		}

		float speed = MyRigidBody.velocity.magnitude;
		MyAnimator.SetFloat("Speed", speed);
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

	/// <summary>
	/// This will place more dirt around the crosshair if they're in a location without much nearby
	/// </summary>
	private void UpdatePlaceDirt()
	{
		RaycastHit[] hits = Physics.SphereCastAll(transform.position, 1.5f, Vector3.up, 0f, DirtLayer);
		int numHits = hits.Length;
		spawnDirtSpeed = Mathf.Max(0f, MaxNearbySpots - numHits);

		spawnDirtTimer -= Time.deltaTime * spawnDirtSpeed;
		if (spawnDirtTimer <= 0f)
		{
			Instantiate(DirtSpotPrefab, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity, MiniGameManagerRef.transform);
			spawnDirtTimer = 5f;
		}
	}

	/// <summary>
	/// Randomly plays the pig squeel.
	/// </summary>
	private void UpdateSound()
	{
		soundTimer -= Time.deltaTime;
		if (soundTimer <= 0f)
		{
			PigSound.pitch = Random.Range(0.8f, 1.5f);
			PigSound.Play();
			soundTimer = Random.Range(3f, 8f);
		}
	}
}
