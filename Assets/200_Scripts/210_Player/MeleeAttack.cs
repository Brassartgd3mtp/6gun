using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 2.0f; // Port�e de l'attaque

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
        // Obtenez tous les colliders dans la port�e de l'attaque
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            // V�rifiez si l'objet a le tag "Destroyable" et le layer "Enemy"
            if (hitCollider.CompareTag("Destroyable") && hitCollider.gameObject.layer == LayerMask.NameToLayer("Ennemy"))
            {
                // D�truisez imm�diatement l'ennemi
                Destroy(hitCollider.gameObject);
            }
        }
    }
}
