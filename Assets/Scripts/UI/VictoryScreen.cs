using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
	public GameObject Player1VictoryText;
	public GameObject Player2VictoryText;

	// Start is called before the first frame update
	void Start()
	{
		DisplayWinner();
	}

	/// <summary>
	/// This will check the manager's winner and display the appropriate text
	/// </summary>
	private void DisplayWinner()
	{
		GameManager manager = GameManager.Instance;
		if (manager == null)
		{
			// Should only end up in here if we start the editor in this scene
			return;
		}

		if (manager.Winner == 1)
		{
			Player1VictoryText.gameObject.SetActive(true);
		}
		else if (manager.Winner == 2)
		{
			Player2VictoryText.gameObject.SetActive(true);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Returns to the main menu.
	/// </summary>
	public void ExitToMenuButton()
	{
		GameManager.Instance.ExitToMenuButton();
	}
}
