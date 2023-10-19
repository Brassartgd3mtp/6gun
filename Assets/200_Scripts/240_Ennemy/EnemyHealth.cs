using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Points de vie maximum de l'ennemi
    private int currentHealth; // Points de vie actuels de l'ennemi

    void Start()
    {
        currentHealth = maxHealth;
    }

    // M�thode pour r�duire les points de vie de l'ennemi
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(); // L'ennemi est mort, appelez la m�thode Die
        }
    }

    // M�thode pour g�rer la mort de l'ennemi
    void Die()
    {
        // Vous pouvez impl�menter ici des actions � effectuer lorsque l'ennemi meurt, comme la destruction de l'objet
        Destroy(gameObject);
    }
}
