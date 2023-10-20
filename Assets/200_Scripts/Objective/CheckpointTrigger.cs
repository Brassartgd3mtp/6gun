using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager checkpointManager = other.GetComponent<CheckpointManager>();
            if (checkpointManager != null)
            {
                // Met � jour le dernier checkpoint du joueur en utilisant la position de ce checkpoint
                checkpointManager.SetCheckpoint(transform.position, healthManager.health);


            }
        }
    }
}
