using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanDirtyMiniGame : MiniGameManager
{
	public RoundTimer RoundTimerRef;
	public float DirtyVictoryAmount;
	public float MaximumDirty;
	public float DirtyRating;
	public bool GameOver;
	public int Winner;
	private float victoryTimer;

	public Text DirtyRatingText;
	public Text VictoryText;
	public Text InstructionsText;
	public GameObject TitleCard;
	public GameObject CanvasRef;

	// Start is called before the first frame update
	void Start()
	{
		victoryTimer = 5f;
		TitleCard.SetActive(true);
		CanvasRef.SetActive(false);
		GameManager.Instance.HUDMenu.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (TitleCard.gameObject.activeInHierarchy && ((Input.anyKey && GameManager.Instance.RoundTimer > 1.5f) || GameManager.Instance.RoundTimer > 3f))
		{
			TitleCard.SetActive(false);
			CanvasRef.SetActive(true);
			GameManager.Instance.HUDMenu.gameObject.SetActive(true);
			GameManager.Instance.PlayBeepBoop();
		}

		UpdateDirtyRatingText();
		UpdateVictoryCheck();
	}

	/// <summary>
	/// Calculates how dirty the level is.
	/// </summary>
	private void UpdateDirtyRatingText()
	{
		DirtSpot[] allDirt = GetComponentsInChildren<DirtSpot>();
		float totalDirtLevel = 0f;
		foreach (DirtSpot dirt in allDirt)
		{
			totalDirtLevel += dirt.DirtLevel;
		}
		DirtyRating = totalDirtLevel / MaximumDirty;
		DirtyRating = Mathf.Clamp(DirtyRating, 0f, 1f);
		DirtyRatingText.text = "Dirty rating: " + (DirtyRating * 100).ToString("0.00") + "%";
	}

	/// <summary>
	/// Once the round's timer runs out, see how dirty the level is and declare a winner.
	/// Also handle displaying the winner for a bit before transitioning to the next game.
	/// </summary>
	private void UpdateVictoryCheck()
	{
		if (!GameOver && RoundTimerRef.RoundOver)
		{
			if (DirtyRating > DirtyVictoryAmount)
			{
				Winner = 2;
				VictoryText.text = "The forces of dirtiness win!";
			}
			else
			{
				Winner = 1;
				VictoryText.text = "The forces of cleanliness win!";
			}
			GameOver = true;
		}

		if (GameOver)
		{
			victoryTimer -= Time.deltaTime;
			if (victoryTimer <= 0f)
			{
				RoundOver(Winner);
			}
		}

		if (GameManager.Instance.RoundTimer > 7f)
		{
			InstructionsText.gameObject.SetActive(false);
		}
	}
}
