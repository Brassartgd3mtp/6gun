using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public string playerTag = "Player"; // Tag du joueur
    public float rotationSpeed = 5.0f; // Vitesse de rotation

    private Transform playerTransform;

    private void Start()
    {
        // Recherche le joueur au d�marrage
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Le joueur n'a pas �t� trouv�. Assurez-vous d'avoir correctement tagu� le joueur.");
        }
    }

    private void Update()
    {
        // V�rifiez si le joueur a �t� trouv�
        if (playerTransform != null)
        {
            // Faites en sorte que l'objet regarde toujours vers le joueur
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
