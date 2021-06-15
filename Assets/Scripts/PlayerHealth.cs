using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isInvincible = false;
    public SpriteRenderer graphics;
    public float invincibilityDuring;
    public float invincibilityTimeDelay;

    public HealthBar healthBar;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de PlayerHealth dans la scéne");
            return;
        }
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(60);
        }
    }
    public void TakeDamage(int damage)
    {
        if (!isInvincible) // n est pas invincible il prend des degats
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth); // permet d actualiser la vie avec HealthBar
            if (currentHealth <= 0)
            {
                Die();
                return;
            }
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(InvibilityDelay());
        }
    }
    public void Die()
    {
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.animator.SetTrigger("Die");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }
    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f,1f,1f,0f); // personnage transparent
            yield return new WaitForSeconds(invincibilityTimeDelay);
            graphics.color = new Color(1f,1f,1f,1f);// non transparent
            yield return new WaitForSeconds(invincibilityTimeDelay);
        }
    }
    public IEnumerator InvibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityDuring);
        isInvincible = false;
    }
}
