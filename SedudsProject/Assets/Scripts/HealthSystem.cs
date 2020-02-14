using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public float playerHealth;
    public float playerMaxHealth;
    public float npcHealth;
    public float npcMaxHealth;

    public bool gameOver;
    public bool isDead;

    /*public HealthSystem(int playeMaxHealth)
    {
        this.playerMaxHealth = playeMaxHealth;
        playerHealth = playeMaxHealth;
    }*/

    // need formula to calculate health values

    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    public float GetNPCHealth()
    {
        return npcHealth;
    }

    public float GetPlayerHealthPercent()
    {
        return playerHealth / playerMaxHealth;
    }

    public float GetNPCHealthPercent()
    {
        return npcHealth / npcMaxHealth;
    }

    public void PlayerDamage(float DamageAmount)
    {
        npcHealth -= DamageAmount;
        if (npcHealth <= 0)
        {
            npcHealth = 0;
            isDead = true;
        }
    }

    public void NPCDamage(float DamageAmount)
    {
        playerHealth -= DamageAmount;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            gameOver = true;
        }
    }
}