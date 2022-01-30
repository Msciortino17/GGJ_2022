using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtSpot : MonoBehaviour
{
	public float DirtLevel;
	private float startingDirtAmount;
	public SpriteRenderer MySprite;

	public List<Sprite> RandomSprites;

	// Start is called before the first frame update
	void Start()
	{
		float size = Random.Range(1f, 3f);
		DirtLevel = size;
		startingDirtAmount = DirtLevel;
		Vector3 scale = transform.localScale;
		scale.x = scale.y = scale.z = size;
		transform.localScale = scale;
		MySprite.sprite = RandomSprites[Random.Range(0, RandomSprites.Count)];
		transform.Rotate(0f, 0f, Random.Range(0f, 360f));
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
