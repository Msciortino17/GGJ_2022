using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public ParticleSystem CreatedParticles;
    public ParticleSystem DestroyedParticles;
    public bool Usable;
    public float Health;
    public float MaxHealth;
    public float CreateRate;
    public float DestroyRate;
    public float HealthBarScale;
    public Transform HealthBar;
    public Transform HealthBarBg;
    public Sprite UsableEnergySprite;

    public void InitUsable(bool usable)
    {
        Usable = usable;
        if(usable)
        {
            SetHealth(MaxHealth);
        }
        else
        {
            SetHealth(0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
        UpdateHealthBar();
    }

    void UpdateSprite()
    {
        if(Usable)
        {
            if(!CreatedParticles.isPlaying) CreatedParticles.Play();
            if(DestroyedParticles.isPlaying) DestroyedParticles.Stop();
            spriteRenderer.sprite = UsableEnergySprite;
        }
        else
        {
            if(!DestroyedParticles.isPlaying) DestroyedParticles.Play();
            if(CreatedParticles.isPlaying) CreatedParticles.Stop();
            spriteRenderer.sprite = null;
        }
        
    }

    void UpdateHealthBar()
    {
        // If 0 or Max, Hide
        if(WithinThreshold(Health, 0) || WithinThreshold(Health, MaxHealth))
        {
            HealthBar.gameObject.SetActive(false);
            HealthBarBg.gameObject.SetActive(false);
        }
        // Else Scale health bar
        else
        {
            float healthRatio = Health / MaxHealth;
            float healthBarScale = healthRatio * HealthBarScale; 
            
            Vector3 healthBarLocalScale = HealthBar.transform.localScale;
            healthBarLocalScale.x = healthBarScale;

            HealthBar.transform.localScale = healthBarLocalScale;

            HealthBar.gameObject.SetActive(true);
            HealthBarBg.gameObject.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Destroyer")
        {
            ChangeHealth( -DestroyRate, Time.fixedDeltaTime);
        }
        else if (other.tag == "Creator")
        {
            ChangeHealth( CreateRate, Time.fixedDeltaTime);
        }
    }

    private void ChangeHealth(float change, float dt)
    {
        SetHealth(Health + change * dt);
    }

    private void SetHealth(float health)
    {
        Health = Mathf.Min(MaxHealth, Mathf.Max(health, 0));

        if(WithinThreshold(Health, 0))
        {
            Usable = false;
        }
        else if (WithinThreshold(Health, MaxHealth))
        {
            Usable = true;
        }
    }

    private bool WithinThreshold(float a, float b)
    {
        return Mathf.Abs(a - b) < .00001f;
    }

    private SpriteRenderer spriteRenderer;
}
