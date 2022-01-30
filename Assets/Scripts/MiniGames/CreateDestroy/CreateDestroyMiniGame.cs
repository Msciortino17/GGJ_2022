using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateDestroyMiniGame : MiniGameManager
{
	public Energy[] EnergyPieces;

	public float MaxRemainingTime;
	public float EndTimer = 3.0f;
	public int CreatorScore;
	public int DestroyerScore;

	public Text TimeText;
	public Text CreatorScoreText;
	public Text DestroyerScoreText;
	public Text VictoryText;

	private float remainingTime;
	private bool roundOver;
	private int winner;
	private float endTimer;

	// Start is called before the first frame update
	void Start()
	{
		InitManager();

		System.Random random = new System.Random();	
		var randomEnergies = EnergyPieces.OrderBy(x => random.Next()).ToArray();
		for(int i = 0; i < randomEnergies.Length / 2; ++i)
		{
			randomEnergies[i].InitUsable(false);
		}

		remainingTime = MaxRemainingTime;
	}

	// Update is called once per frame
	void Update()
	{
		// Check created and destroyed
		bool anyDestroyed = false;
		bool anyCreated = false;
		int destroyedCount = 0;
		int createCount = 0;
		foreach(var energy in EnergyPieces)
		{
			if(energy.Usable)
			{
				anyCreated = true;
				createCount++;
			}
			else
			{
				anyDestroyed = true;
				destroyedCount++;
			}
		}

		// Update Score
		CreatorScore = createCount;
		CreatorScoreText.text = CreatorScore.ToString();
		
		DestroyerScore = destroyedCount;
		DestroyerScoreText.text = DestroyerScore.ToString();

		// Handle Win Conditions

		// Time win condition
		remainingTime = Mathf.Max(remainingTime - Time.deltaTime, 0);
		TimeText.text = remainingTime.ToString("0.0");

		if(remainingTime <= 0)
		{
			winner = destroyedCount >= 5 ? 2 : 1;
			VictoryText.gameObject.SetActive(true);
			VictoryText.text = "Player " + winner + " Wins!";
			
			endTimer += Time.deltaTime;
			if(endTimer >= EndTimer)
			{
				RoundOver(winner);
			}
		}


		// ALL CREATED condition
		if(!anyDestroyed)
		{
			RoundOver(1);
		}

		// ALL DESTROYED condition
		if(!anyCreated)
		{
			RoundOver(2);
		}
	}
}
