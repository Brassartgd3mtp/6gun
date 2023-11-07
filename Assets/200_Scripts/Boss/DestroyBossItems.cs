using UnityEngine;

public class DestroyBossItems : MonoBehaviour
{
    public GameObject particleEffectPrefab; // Pr�fab de l'effet de particules

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            // L'objet touch� a le tag "boss"
            Destroy(other.gameObject); // D�truit l'objet boss

            // Cr�ez un effet de particules � l'emplacement de la collision
            if (particleEffectPrefab != null)
            {
                Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
