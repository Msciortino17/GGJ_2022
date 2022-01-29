using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject CreditsMenu;
	public GameObject TitleMenu;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Starts the game by loading the main gameplay scene.
	/// </summary>
	public void PlayGameButton()
	{
		SceneManager.LoadScene("GamePlay");
	}

	/// <summary>
	/// Toggles the credits menu, used for main credits button and back button in credits menu.
	/// </summary>
	public void ToggleCreditsButton()
	{
		CreditsMenu.gameObject.SetActive(!CreditsMenu.gameObject.activeInHierarchy);
		TitleMenu.gameObject.SetActive(!TitleMenu.gameObject.activeInHierarchy);
	}

	/// <summary>
	/// Closes the application.
	/// </summary>
	public void ExitButton()
	{
		Application.Quit();
	}
}
