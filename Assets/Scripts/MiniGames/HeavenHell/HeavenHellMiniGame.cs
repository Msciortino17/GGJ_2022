using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeavenHellMiniGame : MiniGameManager
{
	[Header("Game Logic")]
	public int HeavenMoney;
	public int HellMoney;
	public List<Minion> HeavenMinions;
	public List<Minion> HellMinions;
	public Minion HeavenFighter;
	public Minion HellFighter;
	HeavenHellState CurrentState;
	public int Winner;
	private float victoryTimer;
	private float fightTimer;

	[Header("Minion Prefabs")]
	public GameObject MinionHeavenBig;
	public GameObject MinionHeaven;
	public GameObject MinionHeavenSmall;
	public GameObject MinionHellBig;
	public GameObject MinionHell;
	public GameObject MinionHellSmall;
	private List<int> MinionCosts;

	[Header("References")]
	public Transform HeavenFightSpot;
	public Transform HellFightSpot;
	public Transform HeavenSpawnSpot;
	public Transform HellSpawnSpot;
	public GameObject InstructionsRef;
	public Text HeavenMoneyText;
	public Text HellMoneyText;
	public Text VictoryText;
	public GameObject TitleCard;
	public GameObject CanvasRef;

	// Start is called before the first frame update
	void Start()
	{
		InitManager();
		LoadCosts();
		CurrentState = HeavenHellState.Buying;
		TitleCard.SetActive(true);
		CanvasRef.SetActive(false);
		GameManager.Instance.HUDMenu.gameObject.SetActive(false);
		//GameManager.Instance.PlayBeepBoop();
	}

	/// <summary>
	/// Reads the costs on the prefabs and loads them into the local list.
	/// Since this is an annoying process to do, we do it once at the beginning and then easily access the list during game logic.
	/// </summary>
	private void LoadCosts()
	{
		MinionCosts = new List<int>();
		MinionCosts.Add(MinionHeavenBig.GetComponent<Minion>().Cost);
		MinionCosts.Add(MinionHeaven.GetComponent<Minion>().Cost);
		MinionCosts.Add(MinionHeavenSmall.GetComponent<Minion>().Cost);
		MinionCosts.Add(MinionHellBig.GetComponent<Minion>().Cost);
		MinionCosts.Add(MinionHell.GetComponent<Minion>().Cost);
		MinionCosts.Add(MinionHellSmall.GetComponent<Minion>().Cost);
	}

	// Update is called once per frame
	void Update()
	{
		if (TitleCard.gameObject.activeInHierarchy && ((Input.anyKey && GameManager.Instance.RoundTimer > 1.5f) || GameManager.Instance.RoundTimer > 3f))
		{
			TitleCard.SetActive(false);
			CanvasRef.SetActive(true);
			GameManager.Instance.HUDMenu.gameObject.SetActive(true);
			GameManager.Instance.PlayBeepBoop();
		}

		switch (CurrentState)
		{
			case HeavenHellState.Buying:
				UpdateBuying();
				break;
			case HeavenHellState.Combat:
				UpdateCombat();
				break;
			case HeavenHellState.Victory:
				UpdateVictory();
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Checks input for buying minions, and then starts the round once all money has been spent.
	/// </summary>
	private void UpdateBuying()
	{
		// Once both players have spent their money, time to begin
		if (HeavenMoney <= 0 && HellMoney <= 0)
		{
			InstructionsRef.SetActive(false);
			CurrentState = HeavenHellState.Combat;
		}

		// Up, left, right buys minions
		if (Input.GetKeyDown(KeyCode.A) && HeavenMoney >= MinionCosts[0])
		{
			HeavenMoney -= MinionCosts[0];
			Minion bigHeaven = Instantiate(MinionHeavenBig, HeavenSpawnSpot.position, Quaternion.identity, transform).GetComponent<Minion>();
			//bigHeaven.transform.Translate(Random.Range(-7f, 7f), 0f, 0f);
			HeavenMinions.Add(bigHeaven);
			GameManager.Instance.PlayButtonSound();
		}
		if (Input.GetKeyDown(KeyCode.W) && HeavenMoney >= MinionCosts[1])
		{
			HeavenMoney -= MinionCosts[1];
			Minion heaven = Instantiate(MinionHeaven, HeavenSpawnSpot.position, Quaternion.identity, transform).GetComponent<Minion>();
			//heaven.transform.Translate(Random.Range(-7f, 7f), 0f, 0f);
			HeavenMinions.Add(heaven);
			GameManager.Instance.PlayButtonSound();
		}
		if (Input.GetKeyDown(KeyCode.D) && HeavenMoney >= MinionCosts[2])
		{
			HeavenMoney -= MinionCosts[2];
			Minion smallHeaven = Instantiate(MinionHeavenSmall, HeavenSpawnSpot.position, Quaternion.identity, transform).GetComponent<Minion>();
			//smallHeaven.transform.Translate(Random.Range(-7f, 7f), 0f, 0f);
			HeavenMinions.Add(smallHeaven);
			GameManager.Instance.PlayButtonSound();
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) && HellMoney >= MinionCosts[3])
		{
			HellMoney -= MinionCosts[3];
			Minion bigHell = Instantiate(MinionHellBig, HellSpawnSpot.position, Quaternion.identity, transform).GetComponent<Minion>();
			//bigHell.transform.Translate(Random.Range(-7f, 7f), 0f, 0f);
			HellMinions.Add(bigHell);
			GameManager.Instance.PlayButtonSound();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) && HellMoney >= MinionCosts[4])
		{
			HellMoney -= MinionCosts[4];
			Minion hell = Instantiate(MinionHell, HellSpawnSpot.position, Quaternion.identity, transform).GetComponent<Minion>();
			//hell.transform.Translate(Random.Range(-7f, 7f), 0f, 0f);
			HellMinions.Add(hell);
			GameManager.Instance.PlayButtonSound();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) && HellMoney >= MinionCosts[5])
		{
			HellMoney -= MinionCosts[5];
			Minion smallHell = Instantiate(MinionHellSmall, HellSpawnSpot.position, Quaternion.identity, transform).GetComponent<Minion>();
			//smallHell.transform.Translate(Random.Range(-7f, 7f), 0f, 0f);
			HellMinions.Add(smallHell);
			GameManager.Instance.PlayButtonSound();
		}

		HeavenMoneyText.text = "$" + HeavenMoney;
		HellMoneyText.text = "$" + HellMoney;
	}

	/// <summary>
	/// This will pit minions against each other one at a time until the last man standing.
	/// </summary>
	private void UpdateCombat()
	{
		bool heavenFighterNull = HeavenFighter == null;
		bool hellFighterNull = HellFighter == null;

		// First do the win check. If a side has no fighter and is out of standby minions
		if (heavenFighterNull && HeavenMinions.Count <= 0)
		{
			Winner = 2;
			CurrentState = HeavenHellState.Victory;
			VictoryText.text = "Hell wins!!";

			if (!hellFighterNull)
			{
				HellFighter.StartMoving(HeavenSpawnSpot.position);
			}

			return;
		}
		else if (hellFighterNull && HellMinions.Count <= 0)
		{
			Winner = 1;
			CurrentState = HeavenHellState.Victory;
			VictoryText.text = "Heaven wins!!";

			if (!heavenFighterNull)
			{
				HeavenFighter.StartMoving(HellSpawnSpot.position);
			}

			return;
		}

		// Next, check if fighter slots are empty. If so, pull a fighter from standby.
		if (heavenFighterNull)
		{
			HeavenFighter = HeavenMinions[0];
			HeavenMinions.RemoveAt(0);
			HeavenFighter.StartMoving(HellSpawnSpot.position);
		}
		if (hellFighterNull)
		{
			HellFighter = HellMinions[0];
			HellMinions.RemoveAt(0);
			HellFighter.StartMoving(HeavenSpawnSpot.position);
		}

		// If both sides have fighters, wait until they're both in position before having them fight
		if (!heavenFighterNull && !hellFighterNull)
		{
			HeavenFighter.FightPosition = HellFighter.transform.position;
			HellFighter.FightPosition = HeavenFighter.transform.position;
			float distance = (HeavenFighter.MySpriteTransform.transform.localScale.x + HellFighter.MySpriteTransform.transform.localScale.x) * 0.5f;
			HeavenFighter.FightDistance = distance;
			HellFighter.FightDistance = distance;

			if (HeavenFighter.MyState == MinionState.Fighting && HellFighter.MyState == MinionState.Fighting)
			{
				fightTimer -= Time.deltaTime;
				if (fightTimer <= 0f)
				{
					fightTimer = 0.5f;

					int hellDamage = HellFighter.OffensePower - HeavenFighter.DefensePower + Random.Range(-2, 2);
					int heavenDamage = HeavenFighter.OffensePower - HellFighter.DefensePower + Random.Range(-2, 2);
					HeavenFighter.Health -= Mathf.Max(hellDamage, 1);
					HellFighter.Health -= Mathf.Max(heavenDamage, 1);

					if (HeavenFighter.Health <= 0)
					{
						Destroy(Instantiate(HeavenFighter.DeathParticles, HeavenFighter.transform.position, Quaternion.identity), 3f);
						Destroy(HeavenFighter.gameObject);
						HeavenFighter = null;
						//HellFighter?.StartMoving(HeavenSpawnSpot.position);
					}

					if (HellFighter.Health <= 0)
					{
						Destroy(Instantiate(HellFighter.DeathParticles, HellFighter.transform.position, Quaternion.identity), 3f);
						Destroy(HellFighter.gameObject);
						HellFighter = null;
						//HeavenFighter?.StartMoving(HellSpawnSpot.position);
					}
				}
			}
		}
	}

	bool victoryFlag = false;

	/// <summary>
	/// Take some time to display the winner before moving onto the next minigame
	/// </summary>
	private void UpdateVictory()
	{
		if (victoryFlag)
			return;

		victoryTimer += Time.deltaTime;
		if (victoryTimer >= 3f || Input.anyKey)
		{
			RoundOver(Winner);
			victoryFlag = true;
		}
	}
}

public enum HeavenHellState
{
	Buying,
	Combat,
	Victory,
}
