using System.Linq;

public class CreateDestroyMiniGame : MiniGameManager
{
	public Energy[] EnergyPieces;

	// Start is called before the first frame update
	void Start()
	{
		InitManager();

		System.Random random = new System.Random();	
		var randomEnergies = EnergyPieces.OrderBy(x => random.Next()).ToArray();
		for(int i = 0; i < EnergyPieces.Length / 2; ++i)
		{
			EnergyPieces[i].InitUsable(false);
		}
	}

	// Update is called once per frame
	void Update()
	{
		// Handle Win Conditions
		bool anyDestroyed = false;
		bool anyCreated = false;
		foreach(var energy in EnergyPieces)
		{
			if(energy.Usable)
			{
				anyCreated = true;
			}
			else
			{
				anyDestroyed = true;
			}
		}

		if(!anyDestroyed)
		{
			RoundOver(1);
		}

		if(!anyCreated)
		{
			RoundOver(2);
		}
	}
}
