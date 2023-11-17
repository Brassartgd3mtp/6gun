using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    // R�f�rence � la cam�ra � suivre (peut �tre d�finie dans l'inspecteur Unity)
    public Camera targetCamera;

    void Update()
    {
        // Assurez-vous qu'une cam�ra est d�finie
        if (targetCamera == null)
        {
            Debug.LogError("La cam�ra cible n'est pas d�finie. Veuillez assigner une cam�ra dans l'inspecteur Unity.");
            return;
        }

        // Faites en sorte que l'objet regarde toujours vers la cam�ra
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
            targetCamera.transform.rotation * Vector3.up);
    }
}
