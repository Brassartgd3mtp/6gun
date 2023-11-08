using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float liftSpeed = 1.0f; // Vitesse de mont�e de l'ascenseur
    public float maxHeight = 10.0f; // Hauteur maximale � laquelle l'ascenseur doit monter

    private List<Transform> objectsToMove = new List<Transform>(); // Liste des objets � d�placer
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            foreach (Transform objToMove in objectsToMove)
            {
                // V�rifiez si l'objet n'a pas atteint la hauteur maximale
                if (objToMove.position.y < maxHeight)
                {
                    // Augmentez la position Y de l'objet pour le faire monter
                    float newY = objToMove.position.y + liftSpeed * Time.deltaTime;
                    objToMove.position = new Vector3(objToMove.position.x, newY, objToMove.position.z);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // V�rifiez si l'objet a le tag "Player" ou tout autre tag d'objet que vous souhaitez d�placer
        if (other.CompareTag("Player") || other.CompareTag("ObjectToMove"))
        {
            isMoving = true;

            // Ajoutez l'objet � la liste des objets � d�placer
            if (!objectsToMove.Contains(other.transform))
            {
                objectsToMove.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // V�rifiez si l'objet a le tag "Player" ou tout autre tag d'objet que vous souhaitez d�placer
        if (other.CompareTag("Player") || other.CompareTag("ObjectToMove"))
        {
            // Retirez l'objet de la liste des objets � d�placer
            objectsToMove.Remove(other.transform);

            // Si la liste est vide, arr�tez de d�placer les objets
            if (objectsToMove.Count == 0)
            {
                isMoving = false;
            }
        }
    }
}
