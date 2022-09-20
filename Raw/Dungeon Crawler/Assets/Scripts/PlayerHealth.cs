using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private PlayerAnimation playerAnimation;
    public float maxHealth = 3;
    [SerializeField] private Life life;

    private void Start()
    {

        currentHealth = maxHealth;

        playerAnimation = GetComponent<PlayerAnimation>();
    }

    public override void ChangeHealth(float amount)
    {
        base.ChangeHealth(amount);
        life.HeartsUpdate();
    }
    protected override void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }
        else
        {
            playerAnimation.SetPlayerState(PlayerState.idle);
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    protected override void Kill()
    {
        base.Kill();
        if(playerAnimation.playerState != PlayerState.dead)
        {
            playerAnimation.TriggerDeathAnimation();
        }
        //play audio
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<Octorok>() != null)
            {
                Octorok enemy = collision.gameObject.GetComponent<Octorok>();
                float damage = enemy.damageAmount;
                ChangeHealth(damage);
                playerAnimation.Hit();
            }
            else if (collision.gameObject.GetComponent<EndBoss>() != null)
            {
                EndBoss enemy = collision.gameObject.GetComponent<EndBoss>();
                float damage = enemy.damageAmount;
                ChangeHealth(damage);
                playerAnimation.Hit();
            }
            else if (collision.gameObject.GetComponent<Projectile>() != null)
            {
                Projectile enemy = collision.gameObject.GetComponent<Projectile>();
                float damage = enemy.damageAmount;
                playerAnimation.Hit();
                ChangeHealth(damage);
            }
            else
            {
                Debug.LogError("er is hier een probleem");
            }
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void AddMoreMaxHealth(float extraHealthAmount)
    {
        maxHealth = maxHealth + extraHealthAmount;
    }
 }
