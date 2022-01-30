using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Minion : MonoBehaviour
{
	private int FullHealth;
	public int Health;
	public int OffensePower;
	public int DefensePower;
	public int Cost;
	public float Speed;

	public MinionState MyState;
	public Vector3 FightPosition;
	public float FightDistance;

	public Transform MySpriteTransform;
	public Transform MyHealthBar;
	private float fullHealthScale;
	private float fightShakeSpeed;
	
	private SpriteRenderer myHealthBarRenderer;

	public AudioSource MyFightSound;
	public GameObject DeathParticles;

	// Start is called before the first frame update
	void Start()
	{
		myHealthBarRenderer = MyHealthBar.GetComponent<SpriteRenderer>();
		fullHealthScale = MyHealthBar.localScale.x;
		FullHealth = Health;
		fightShakeSpeed = Random.Range(5f, 10f);
	}

	// Update is called once per frame
	void Update()
	{
		switch (MyState)
		{
			case MinionState.Standby:
				break;
			case MinionState.Moving:
				UpdateMoving();
				break;
			case MinionState.Fighting:
				UpdateFighting();
				UpdateHealthBar();
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Moves towards the fight position and then begins fighting once close.
	/// </summary>
	private void UpdateMoving()
	{
		Vector3 toPosition = FightPosition - transform.position;
		if (toPosition.sqrMagnitude < FightDistance * FightDistance)
		{
			StartFighting();
		}
		transform.Translate(toPosition.normalized * Speed * Time.deltaTime);
	}

	/// <summary>
	/// Bobs the sprite up and down. Actual fighting logic is handled in manager.
	/// </summary>
	private void UpdateFighting()
	{
		MySpriteTransform.Translate(0f, (0.5f - Mathf.PingPong(Time.time * fightShakeSpeed, 1f)) * Time.deltaTime, 0f);
	}

	/// <summary>
	/// Scales the healthbar according to how low health is.
	/// </summary>
	private void UpdateHealthBar()
	{
		Vector3 scale = MyHealthBar.transform.localScale;
		scale.x = ((float)Health / (float)FullHealth) * fullHealthScale;
		MyHealthBar.transform.localScale = scale;

		float H, S, V;
		Color.RGBToHSV(myHealthBarRenderer.color, out H, out S, out V);

		float mappedHue = map(scale.x, 0f, 1f, 0f, 0.33f);
		myHealthBarRenderer.color = Color.HSVToRGB(mappedHue, S, V);
	}

	/// <summary>
	/// Used by the manager to have an idle minion begin moving to the fight position.
	/// </summary>
	public void StartMoving(Vector3 position)
	{
		FightPosition = position;
		MyState = MinionState.Moving;
		MyFightSound.Play();
	}

	/// <summary>
	/// Begins actual fighting.
	/// </summary>
	public void StartFighting()
	{
		MyState = MinionState.Fighting;
	}
	
	float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}

}

public enum MinionState
{
	Standby,
	Moving,
	Fighting
}
