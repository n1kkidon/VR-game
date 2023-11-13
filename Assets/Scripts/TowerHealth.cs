using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour, IAttackable
{
    public float maxHealth = 1000;
    public float currentHealth;
    public Slider sliderHP;
    //public GameManager manager;

    public void SetMaxHealth(float health)
    {
        sliderHP.maxValue = health;
        sliderHP.value = health;
    }

    public void SetHealth(float health)
    {
        sliderHP.value = health;
    }

    public float GetCurrentHealth() => currentHealth;
    Animator animator;
    //PlayerMovement playerMovement;
    bool isDead = false;

    [SerializeField] private AudioSource painSoundEffect;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        //playerMovement = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(20);
        //}
    }

    public void TakeDamage(float damage)
    {
        // if (!playerMovement.CanTakeDamage) //could also play some block sound, idk
        //     return;

        // animator.SetTrigger("GetHit");
        currentHealth -= damage;

        SetHealth(currentHealth);
        //playerMovement.flashImage.StartFlash(0.15f, 0.2f, Color.red);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            //animator.SetBool("Died", true);
            //manager.GameOver();
        }
        //painSoundEffect.Play();
    }

    public void Heal(int ammount)
    {
        if (currentHealth + ammount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else currentHealth += ammount;
        SetHealth(currentHealth);
    }
}
