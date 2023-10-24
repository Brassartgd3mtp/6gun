using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    public int damageAmount = 10; // Montant de d�g�ts inflig�s lors de la collision avec un ennemi
    private HealthManager healthManager; // R�f�rence au script HealthManager

    void Start()
    {
        // Obtenez la r�f�rence au script HealthManager attach� au m�me GameObject
        healthManager = GetComponent<HealthManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // V�rifiez si la collision a eu lieu avec un ennemi (vous pouvez d�finir des balises ou d'autres m�thodes de d�tection)
        if (collision.gameObject.CompareTag("Destroyable") && collision.gameObject.layer == 10)
        {
            Debug.Log("OUCH");
            // Infligez des d�g�ts au joueur en utilisant le script HealthManager
            healthManager.DamageButton(damageAmount);
        }
    }

}
