using UnityEngine;
using UnityEngine.UI;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public float particleDuration = 1.0f;
    public float attackCooldown = 2.0f;
    public Image cooldownImage;
    public GameObject particlePrefab;

    private bool canAttack = true;

    private void Update()
    {
        // V�rifiez si le joueur peut attaquer et si la touche est press�e
        if (canAttack && Input.GetKeyDown(KeyCode.V))
        {
            // D�marrez la coroutine d'attaque
            StartCoroutine(PerformMeleeAttack());
        }
    }

    private System.Collections.IEnumerator PerformMeleeAttack()
    {
        // D�sactivez la possibilit� d'attaquer pendant le temps de recharge
        canAttack = false;

        // Mettez � jour l'image de cooldown pour indiquer le temps de recharge
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 1.0f; // Remplit compl�tement l'image au d�but du cooldown
        }

        // Attendez le temps de recharge
        float cooldownTimer = attackCooldown;
        while (cooldownTimer > 0f)
        {
            yield return null;
            cooldownTimer -= Time.deltaTime;

            // Mettez � jour l'image de cooldown pendant l'attente
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = cooldownTimer / attackCooldown;
            }
        }

        // R�activez la possibilit� d'attaquer
        canAttack = true;

        // Obtenez tous les colliders dans la port�e de l'attaque
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            // V�rifiez si l'objet a le tag "Enemy" et le layer "Enemy"
            if (hitCollider.CompareTag("Enemy") && hitCollider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // Obtenez la r�f�rence � l'ennemi s'il est touch� par le raycast
                EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    // Appel de la fonction TakeDamage pour r�duire les points de vie de l'ennemi
                    enemyHealth.TakeDamage(10);

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

        // R�initialisez l'image de cooldown � la fin de l'attaque
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0.0f;
        }
    }
}
