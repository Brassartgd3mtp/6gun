using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 2.0f; // Port�e de l'attaque
    public GameObject particlePrefab; // Pr�fab du syst�me de particules
    public float particleDuration = 1.0f; // Dur�e d'affichage des particules en secondes

    private void OnDrawGizmos()
    {
        // Dessinez une sph�re gizmo pour visualiser la port�e de l'attaque
        Gizmos.color = Color.red; // Couleur du gizmo
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PerformMeleeAttack();
        }
    }

    void PerformMeleeAttack()
    {
        Debug.Log("on commence00");
        // Obtenez tous les colliders dans la port�e de l'attaque
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            // V�rifiez si l'objet a le tag "Enemy" et le layer "Enemy"
            if (hitCollider.CompareTag("Enemy") && hitCollider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("of course");
                // D�truisez imm�diatement l'ennemi
                // Obtenez la r�f�rence � l'ennemi s'il est touch� par le raycast
                EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    Debug.Log("touched");
                    // Appel de la fonction TakeDamage pour r�duire les points de vie de l'ennemi
                    enemyHealth.TakeDamage(10);
                    Debug.Log("-10PV");

                    // Instanciez les particules
                    if (particlePrefab != null)
                    {
                        GameObject particleInstance = Instantiate(particlePrefab, hitCollider.transform.position, Quaternion.identity);

                        // D�sactivez les particules apr�s la dur�e sp�cifi�e
                        Destroy(particleInstance, particleDuration);
                    }
                }
            }
        }
    }
}
