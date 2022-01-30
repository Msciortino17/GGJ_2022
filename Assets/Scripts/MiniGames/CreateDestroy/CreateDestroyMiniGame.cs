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
	public Transform TitleCard;
	public Transform CanvasRef;
	public int WinningFontSize;
	public int LosingFontSize; 

	private float remainingTime;
	private bool roundOver;
	private int winner;
	private float endTimer;

	// Start is called before the first frame update
	void Start()
	{
		InitManager();

		// Title Card
		TitleCard.gameObject.SetActive(true);
		CanvasRef.gameObject.SetActive(false);
		GameManager.Instance.HUDMenu.gameObject.SetActive(false);

		// Randomize Order of Energy Pieces
		System.Random random = new System.Random();	
		var randomEnergies = EnergyPieces.OrderBy(x => random.Next()).ToArray();
		for(int i = 0; i < randomEnergies.Length / 2; ++i)
		{
			randomEnergies[i].InitUsable(false);
		}

		// Set Time
		remainingTime = MaxRemainingTime;
	}

	// Update is called once per frame
	void Update()
	{
		if (TitleCard.gameObject.activeInHierarchy && (Input.anyKey || GameManager.Instance.RoundTimer > 3f))
		{
			TitleCard.gameObject.SetActive(false);
			CanvasRef.gameObject.SetActive(true);
			GameManager.Instance.HUDMenu.gameObject.SetActive(true);
			GameManager.Instance.PlayBeepBoop();
		}

		// Check created and destroyed
		int destroyedCount = 0;
		int createCount = 0;
		foreach(var energy in EnergyPieces)
		{
			if(energy.Usable)
			{
				createCount++;
			}
			else
			{
				destroyedCount++;
			}
		}

		// Update Score
		float scale = Mathf.Abs(Mathf.Sin(GameManager.Instance.RoundTimer * 2.0f)) + 0.75f;

		bool creatorWinning = CreatorScore >= 6;
		bool destroyerWinning = !creatorWinning;

		CreatorScore = createCount;
		CreatorScoreText.text = CreatorScore.ToString();
		// CreatorScoreText.fontSize = creatorWinning ? WinningFontSize : LosingFontSize;
		CreatorScoreText.transform.localScale = creatorWinning ? Vector3.one * scale : Vector3.one;
		
		DestroyerScore = destroyedCount;
		DestroyerScoreText.text = DestroyerScore.ToString();
		// DestroyerScoreText.fontSize = DestroyerScore >= 5 ? WinningFontSize : LosingFontSize;
		DestroyerScoreText.transform.localScale = destroyerWinning ? Vector3.one * scale : Vector3.one;

		// Handle Win Conditions

		// Time win condition
		remainingTime = Mathf.Max(remainingTime - Time.deltaTime, 0);
		TimeText.text = remainingTime.ToString("0.0");

		if(remainingTime <= 0)
		{
			winner = destroyedCount >= 5 ? 2 : 1;
			VictoryText.gameObject.SetActive(true);

			if(winner == 1)
			{
				VictoryText.text = "Creation Wins!";
			}
			else
			{
				VictoryText.text = "Destruction Wins";
			}
			
			endTimer += Time.deltaTime;
			if(endTimer >= EndTimer)
			{
				RoundOver(winner);
			}
		}
	}
}
