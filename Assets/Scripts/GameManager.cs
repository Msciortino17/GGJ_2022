using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[Header("States")]
	public bool Paused;

	[Header("Main Gameplay Stuff")]
	public int ScoreToWin;
	public int Player1Score;
	public int Player2Score;
	public int Winner; // 0 = no one won yet
	public int CurrentRound;
	public List<string> MinigameNames;

	[Header("Timers")]
	public float TotalGameTimer;
	public float RoundTimer;

	[Header("References")]
	public GameObject MainCanvas;
	public GameObject PauseMenu;
	public GameObject HUDMenu;

	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	void Start()
	{
		SetupInstance();
		CurrentRound = 1;
		HUDMenu.gameObject.SetActive(true);
		LoadMiniGame();
	}

	/// <summary>
	/// We want to mark this object as don't destroy on load so it can work between scenes.
	/// We also setup a public static reference to it so other scripts can easily access if need be.
	/// This also comes in handy to prevent duplicates of this object when the main gameplay scene gets reloaded.
	/// </summary>
	private void SetupInstance()
	{
		DontDestroyOnLoad(gameObject);

		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		UpdateControls();
		UpdateTimers();
	}

	/// <summary>
	/// Handles logic for checking general game manager controls
	/// </summary>
	private void UpdateControls()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePaused();
		}
	}

	/// <summary>
	/// Increments the game timers by delta time (unless paused)
	/// </summary>
	private void UpdateTimers()
	{
		if (Paused)
			return;

		TotalGameTimer += Time.deltaTime;
		RoundTimer += Time.deltaTime;
	}

	/// <summary>
	/// Called by minigame managers when their round is over.
	/// This will update the score and move onto the next game.
	/// </summary>
	public void RoundOver(int winner)
	{
		if (winner == 1)
		{
			Player1Score++;
		}
		else if (winner == 2)
		{
			Player2Score++;
		}

		bool victory = VictoryCheck();
		if (!victory)
		{
			CurrentRound++;
			RoundTimer = 0f;
			LoadMiniGame();
		}
	}

	/// <summary>
	/// This will simply check running score to see if there's a winner.
	/// If so, we transition to the victory screen.
	/// Returns true/false if the victory has been had.
	/// </summary>
	private bool VictoryCheck()
	{
		if (Player1Score >= ScoreToWin)
		{
			Winner = 1;
		}
		else if (Player2Score >= ScoreToWin)
		{
			Winner = 2;
		}

		if (Winner != 0)
		{
			HUDMenu.SetActive(false);
			SceneManager.LoadScene("VictoryScreen");
			return true;
		}

		return false;
	}

	/// <summary>
	/// Will load the next minigame
	/// By checking the remainder of the current round, this will cycle through the levels that we add to the list.
	/// </summary>
	private void LoadMiniGame()
	{
		int levelToLoad = CurrentRound % MinigameNames.Count;
		SceneManager.LoadScene(MinigameNames[levelToLoad]);
	}

	/// <summary>
	/// Toggles if the game is paused, primarily by toggling the pause menu but also toggline the Paused state which other parts of the game should check for.
	/// </summary>
	public void TogglePaused()
	{
		PauseMenu.gameObject.SetActive(!PauseMenu.gameObject.activeInHierarchy);
		Paused = !Paused;
	}

	/// <summary>
	/// Returns to the main menu (used by the pause menu's exit button)
	/// Also cleans up the main instance reference, since we don't want this in the main menu.
	/// </summary>
	public void ExitToMenuButton()
	{
		Instance = null;
		Destroy(gameObject);
		SceneManager.LoadScene("MainMenu");
	}
}
