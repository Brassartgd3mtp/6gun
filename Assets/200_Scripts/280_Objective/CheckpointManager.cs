using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    private Vector3 checkpointPosition;
    public float SavedHealth; 
    private void Start()
    {
        checkpointPosition = transform.position; // Le point de d�part initial devient le premier checkpoint
    }

    // Fonction pour d�finir un nouveau checkpoint
    public void SetCheckpoint(Vector3 position, float savedHealth)
    {
        SavedHealth = savedHealth;
        checkpointPosition = position;
    }

    // Fonction pour retourner au dernier checkpoint
    public void ReturnToCheckpoint()
    {
        transform.position = checkpointPosition;

        Debug.Log("TP");
    }
}
