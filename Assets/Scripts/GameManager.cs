using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[Header("States")]
	public bool Paused;

	[Header("References")]
	public GameObject PauseMenu;

	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	void Start()
	{
		SetupInstance();
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
