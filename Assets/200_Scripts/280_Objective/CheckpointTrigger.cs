using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Met � jour le dernier checkpoint du joueur en utilisant la position de ce checkpoint
            CheckpointManager.SetCheckpoint(transform.position, HealthManager.health);
        }
    }
}
