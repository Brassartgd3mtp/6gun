using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public float particleDuration = 1.0f;
    public float attackCooldown = 2.0f;
    public Image cooldownImage;
    public GameObject particlePrefab;
    private GameObject nearestEnemy;

    private bool canAttack = true;

    private void Start()
    {
        cooldownImage.fillAmount = 0f;
    }

    private void Update()
    {
        // V�rifiez si le joueur peut attaquer et si la touche est press�e
        if (canAttack && Input.GetKeyDown(KeyCode.V))
        {
            // D�marrez la coroutine d'attaque
            StartCoroutine(PerformMeleeAttack());
        }
    }

    private IEnumerator PerformMeleeAttack()
    {
        // D�sactivez la possibilit� d'attaquer pendant le temps de recharge
        canAttack = false;

        // Mettez � jour l'image de cooldown pour indiquer le temps de recharge
        if (cooldownImage != null)
            cooldownImage.fillAmount = 1.0f; // Remplit compl�tement l'image au d�but du cooldown

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        float maxDist = float.MaxValue;

        foreach (Collider hitCollider in hitColliders)
        {
            // Je v�rifie si le collider a le component EnemyHealth
            if (hitCollider.TryGetComponent(out EnemyHealth _enemy))
            {
                float dist = Vector3.Distance(transform.position, _enemy.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    nearestEnemy = _enemy.gameObject;
                }
            }
        }

        if (nearestEnemy.TryGetComponent(out EnemyHealth _enemyHealth))
        {
            _enemyHealth.TakeDamage(10);

            if (particlePrefab != null)
            {
                GameObject particleInstance = Instantiate(particlePrefab, nearestEnemy.transform.position, Quaternion.identity);

                // D�sactivez les particules apr�s la dur�e sp�cifi�e
                Destroy(particleInstance, particleDuration);
            }
        }

        // Attendez le temps de recharge
        float cooldownTimer = attackCooldown;

        while (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;

            // Mettez � jour l'image de cooldown pendant l'attente
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = cooldownTimer / attackCooldown;
            }
            yield return null;
        }

        // Obtenez tous les colliders dans la port�e de l'attaque

        // R�initialisez l'image de cooldown � la fin de l'attaque
        if (cooldownImage != null)
            cooldownImage.fillAmount = 0.0f;

        // R�activez la possibilit� d'attaquer
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange); 
    }
}
