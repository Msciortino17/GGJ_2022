using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	private GameManager manager;

	public Text RounderTimerText;
	public Text Player1ScoreText;
	public Text Player2ScoreText;

	// Start is called before the first frame update
	void Awake()
	{
		manager = GameManager.Instance;
		InitHUDInfo();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateHUDInfo();
	}

	/// <summary>
	/// Fills in info for the hud that will be the same at the start of the mini game
	/// </summary>
	private void InitHUDInfo()
	{
		Player1ScoreText.text = "Player 1 - " + manager.Player1Score;
		Player2ScoreText.text = "Player 2 - " + manager.Player2Score;
	}

	/// <summary>
	/// Fills in info for the hud that changes each frame
	/// </summary>
	private void UpdateHUDInfo()
	{
		RounderTimerText.text = manager.RoundTimer.ToString("0.00");
	}
}
