using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Vitesse de d�placement du projectile
    public float lifetime = 5.0f; // Dur�e de vie du projectile (en secondes)
    public int damageAmount = 10; // Montant de d�g�ts inflig�s par le projectile au joueur

    private Transform player; // R�f�rence au joueur
    private Transform target; // Position cible actuelle

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouvez le joueur

        if (player == null)
        {
            Debug.LogError("Le joueur n'a pas �t� trouv�. Assurez-vous qu'un objet avec le tag 'Player' est pr�sent.");
            return;
        }
        else
        {
            Debug.Log("Joueur trouv� avec succ�s.");
        }

        target = player; // Initialisez la position cible

        // D�truisez le projectile apr�s la dur�e de vie sp�cifi�e
        Invoke("DestroyProjectile", lifetime);
    }

    // Appel� � chaque frame
    void Update()
    {
        // D�placez le projectile vers la position cible (le joueur)
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // Appel� lorsqu'une collision se produit
    void OnTriggerEnter(Collider other)
    {
        // V�rifiez si l'objet avec lequel le projectile a collision est le joueur
        if (other.CompareTag("Player"))
        {
            Debug.Log("Projectile en collision avec le joueur. Infliger des d�g�ts.");
            // R�duisez la sant� du joueur en utilisant la m�thode ApplyDamage
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.ApplyDamage(damageAmount);
            }

            // Annulez l'Invoke avant de d�truire le projectile
            Debug.Log("Destruction du projectile apr�s la collision avec le joueur.");
            CancelInvoke();
            Destroy(gameObject);
        }
        else
        {
            // Si la collision n'est pas avec le joueur, ne faites rien (ou effectuez d'autres actions si n�cessaire)
        }
    }

}