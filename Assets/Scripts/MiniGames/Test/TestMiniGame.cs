using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMiniGame : MiniGameManager
{
	public Text RandomText;

	// Start is called before the first frame update
	void Start()
	{
		InitManager();
		// Only doing this so I can easily tell if the we've moved onto another round
		RandomText.text = "Random! " + Random.Range(0, 10000);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			RoundOver(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			RoundOver(2);
		}
	}
}
