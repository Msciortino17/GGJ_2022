using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will serve as the base object for each minigame's main manager.
/// It will handle integrating the game into the overarching structrure.
/// Simply extend this class and call the main methods where they should be called.
/// </summary>
public abstract class MiniGameManager : MonoBehaviour
{
	protected GameManager manager;

	/// <summary>
	/// This should be called in Start
	/// </summary>
	protected void InitManager()
	{
		manager = GameManager.Instance;
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Should be called once the mini game's win condition is met and pass along the winner.
	/// </summary>
	protected void RoundOver(int winner)
	{
		manager.RoundOver(winner);
	}
}
