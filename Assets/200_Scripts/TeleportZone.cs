using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    public Transform destination; // R�f�rence � la zone de destination
    private TimerController timerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que le joueur entre dans la zone
        {
            // T�l�portez le joueur � la position de la zone de destination
            other.transform.position = destination.position;
            timerController.timer = 0.0f;
        }
    }
}
