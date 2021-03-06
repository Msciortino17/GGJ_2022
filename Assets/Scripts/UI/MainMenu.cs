using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[Header("References")]
	public GameObject CreditsMenu;
	public GameObject TitleMenu;
	public AudioSource ButtonSound;

	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	void Start()
	{

	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{

	}

	/// <summary>
	/// Starts the game by loading the main gameplay scene.
	/// </summary>
	public void PlayGameButton()
	{
		ButtonSound.Play();
		SceneManager.LoadScene("Gameplay");
	}

	/// <summary>
	/// Toggles the credits menu, used for main credits button and back button in credits menu.
	/// </summary>
	public void ToggleCreditsButton()
	{
		ButtonSound.Play();
		CreditsMenu.gameObject.SetActive(!CreditsMenu.gameObject.activeInHierarchy);
		TitleMenu.gameObject.SetActive(!TitleMenu.gameObject.activeInHierarchy);
	}

	/// <summary>
	/// Closes the application.
	/// </summary>
	public void ExitButton()
	{
		ButtonSound.Play();
		Application.Quit();
	}
}
