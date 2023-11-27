using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public float attackRange = 2f;
    [SerializeField] public float attackCooldown = 2f;
    public float timeBeforeAttack = 1.5f; // Temps d'attente avant d'attaquer une fois que le joueur est d�tect�
    public int meleeDamage = 10;
    public float playerDetectionRange = 5f; // Port�e de d�tection du joueur
    public LayerMask playerLayer; // Couche du joueur

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private float lastAttackTime;
    private float detectionTime; // Temps de d�tection du joueur

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lastAttackTime = -attackCooldown; // Pour permettre la premi�re attaque
    }

    void Update()
    {
        DetectPlayer();

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown && Time.time - detectionTime >= timeBeforeAttack)
            {
                AttackMelee();
            }

            navMeshAgent.SetDestination(player.position);
        }
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerDetectionRange, playerLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                player = hitCollider.transform;
                detectionTime = Time.time; // Enregistrez le temps de d�tection
               
                return; // Arr�tez de chercher d�s que le joueur est trouv�
            }
        }

        // Si le joueur n'est pas d�tect�, effacez la r�f�rence au joueur
        player = null;
    }

    void AttackMelee()
    {
        // G�rer les d�g�ts au corps � corps ici
        Debug.Log("Melee enemy attack!");
        lastAttackTime = Time.time;
    }

    // Fonction pour dessiner un gizmo dans l'�diteur Unity
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    //}
}
