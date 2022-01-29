using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtSpot : MonoBehaviour
{
	public float DirtLevel;
	private float startingDirtAmount;
	public SpriteRenderer MySprite;

	// Start is called before the first frame update
	void Start()
	{
		float size = Random.Range(0.5f, 2f);
		DirtLevel = size;
		startingDirtAmount = DirtLevel;
		Vector3 scale = transform.localScale;
		scale.x = scale.y = scale.z = size;
		transform.localScale = scale;
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Clean up the dirt depending on the sponges speed.
	/// </summary>
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Sponge"))
		{
			DirtLevel -= Time.deltaTime * other.GetComponent<Rigidbody>().velocity.magnitude * other.GetComponent<Sponge>().CleanPower;
			if (DirtLevel < 0f)
			{
				Destroy(gameObject);
				return;
			}

			float fadeRatio = DirtLevel / startingDirtAmount;
			Color color = MySprite.color;
			color.a = Mathf.Max(fadeRatio, 0f);
			MySprite.color = color;
		}
	}
}
