using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour
{
	public float CountDownTime;
	public Text TimerText;
	public bool RoundOver;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		UpdateTimerText();
	}

	private void UpdateTimerText()
	{
		if (RoundOver)
			return;

		float timeDif = CountDownTime - GameManager.Instance.RoundTimer;
		if (timeDif < 0f)
		{
			RoundOver = true;
			timeDif = 0f;
		}
		TimerText.text = timeDif.ToString("0.00");
	}
}
