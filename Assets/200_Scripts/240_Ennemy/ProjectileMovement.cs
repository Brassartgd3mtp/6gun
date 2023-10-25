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
        target = player; // Initialisez la position cible

        // D�truisez le projectile apr�s la dur�e de vie sp�cifi�e
        Invoke("DestroyProjectile", lifetime);
    }

    // Appel� � chaque frame
    void Update()
    {
        // Mettez � jour la position cible � chaque frame en fonction de la position actuelle du joueur
        target = player;

        // D�placez le projectile vers la position cible (le joueur)
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // Appel� lorsqu'une collision se produit
    void OnTriggerEnter(Collider other)
    {
        // V�rifiez si l'objet avec lequel le projectile a collision est sur la couche "Enemies"
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Ignorez la collision avec cet ennemi (m�me couche)
            return;
        }

        // V�rifiez si l'objet avec lequel le projectile a collision est le joueur
        if (other.CompareTag("Player"))
        {
            // R�duisez la sant� du joueur en utilisant la m�thode DamageButton
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.DamageButton(damageAmount);
            }
        }

        // D�truisez le projectile lorsqu'il entre en collision avec n'importe quel autre objet
        DestroyProjectile();
    }

    // Fonction pour d�truire le projectile
    void DestroyProjectile()
    {
        CancelInvoke(); // Annule l'appel � la destruction si elle est d�clench�e manuellement
        Destroy(gameObject);
    }
}
