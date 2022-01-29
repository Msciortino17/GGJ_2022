using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public Sprite UsableEnergySprite;
    public Sprite UnusableEnergySprite;
    public bool Usable;

    // Start is called before the first frame update
    void Start() {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        UpdateSprite();
    }

    void UpdateSprite() {
        if(Usable) {
            mSpriteRenderer.sprite = UsableEnergySprite;
        } else {
            mSpriteRenderer.sprite = UnusableEnergySprite;
        }
    }

    void OnTriggerEnter(Collider other) {
        
    }

    private SpriteRenderer mSpriteRenderer;
}
