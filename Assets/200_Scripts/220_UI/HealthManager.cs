using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public float health = 75f;
    public float maxHealth = 100f;
    public Image healthBarImage;
    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel; // R�f�rence au panneau Game Over

    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            healthBarImage.fillAmount = health / maxHealth;
            healthText.text = health + " / " + maxHealth;

            health = Mathf.Clamp(health, 0f, maxHealth);

            if (health <= 0)
            {
                // La sant� est tomb�e � 0, mettez le jeu en pause et affichez le panneau Game Over
                PauseGame();
            }
        }
    }

    public void DamageButton(int damageAmount)
    {
        if (!isGameOver)
        {
            health -= damageAmount;
        }
    }

    public void HealButton(int damageAmount)
    {
        if (!isGameOver)
        {
            health += damageAmount;
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Mettez le temps � z�ro pour mettre le jeu en pause
        isGameOver = true;

        // Affichez le panneau Game Over
        gameOverPanel.SetActive(true);
    }

    // Vous pouvez ajouter une m�thode pour reprendre le jeu si n�cessaire
    public void ResumeGame()
    {
        Time.timeScale = 1f; // R�tablissez le temps normal pour reprendre le jeu
        isGameOver = false;

        // D�sactivez le panneau Game Over
        gameOverPanel.SetActive(false);
    }
}