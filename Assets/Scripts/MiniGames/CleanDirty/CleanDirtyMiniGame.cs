using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanDirtyMiniGame : MonoBehaviour
{
	public Text DirtyRatingText;

	public float MaximumDirty;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		UpdateDirtyRatingText();
	}

	/// <summary>
	/// Calculates how dirty the level is.
	/// </summary>
	private void UpdateDirtyRatingText()
	{
		DirtSpot[] allDirt = GetComponentsInChildren<DirtSpot>();
		float totalDirtLevel = 0f;
		foreach (DirtSpot dirt in allDirt)
		{
			totalDirtLevel += dirt.DirtLevel;
		}
		float dirtRatio = totalDirtLevel / MaximumDirty;
		dirtRatio = Mathf.Clamp(dirtRatio, 0f, 1f);
		DirtyRatingText.text = "Dirty rating: " + (dirtRatio * 100).ToString("0.00") + "%";
	}
}
