using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float liftSpeed = 1.0f; // Vitesse de mont�e de l'ascenseur
    public float maxHeight = 10.0f; // Hauteur maximale � laquelle l'ascenseur doit monter
    private Transform player; // R�f�rence au transform du joueur
    private bool isMoving = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouvez le joueur par son tag
    }

    private void Update()
    {
        if (isMoving)
        {
            // V�rifiez si le joueur n'a pas atteint la hauteur maximale
            if (player.position.y < maxHeight)
            {
                // Augmentez la position Y du joueur pour le faire monter
                float newY = player.position.y + liftSpeed * Time.deltaTime;
                player.position = new Vector3(player.position.x, newY, player.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = false;
        }
    }
}
