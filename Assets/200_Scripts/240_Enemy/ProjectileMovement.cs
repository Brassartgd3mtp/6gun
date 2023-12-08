using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5.0f;
    public int damageAmount = 10;

    private Transform player; // R�f�rence au joueur

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouvez le joueur

        //if (player == null)
        //{
        //    Debug.LogError("Le joueur n'a pas �t� trouv�. Assurez-vous qu'un objet avec le tag 'Player' est pr�sent.");
        //    return;
        //}
        //else
        //{
        //    Debug.Log("Joueur trouv� avec succ�s.");
        //}

        // D�truisez le projectile apr�s la dur�e de vie sp�cifi�e
        Destroy(gameObject, lifetime);
    }

    // Appel� � chaque frame
    void Update()
    {
        // D�placez le projectile vers la position cible (le joueur)
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    // Appel� lorsqu'une collision se produit
    void OnTriggerEnter(Collider other)
    {
        // V�rifiez si l'objet avec lequel le projectile a collision est le joueur
        if (other.CompareTag("Player"))
        {
            // R�duisez la sant� du joueur en utilisant la m�thode ApplyDamage
            HealthManager healthManager = other.GetComponent<HealthManager>();

            if (healthManager != null)
            {
                healthManager.ApplyDamage(damageAmount);
            }

            Destroy(gameObject);
        }
    }
}