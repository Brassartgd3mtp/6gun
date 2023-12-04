using UnityEngine;

public class EnemyAttraction : MonoBehaviour
{
    public float attractionSpeed = 5.0f; // Vitesse d'attraction de l'ennemi.
    public float detectionRadius = 5.0f; // Rayon de d�tection.
    public LayerMask playerLayer; // Couche du joueur.
    private Transform player; // R�f�rence au transform du joueur.
    private bool playerDetected = false; // Indique si le joueur a �t� d�tect�.

    private void Start()
    {
        // Trouvez le joueur par son tag "Player".
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // V�rifiez si le joueur est dans la zone de d�tection.
        playerDetected = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);

        // Si le joueur est d�tect�, attirez l'ennemi vers le joueur.
        if (playerDetected)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.Translate(directionToPlayer * attractionSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dessinez une gizmo sph�rique pour la zone de d�tection.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
