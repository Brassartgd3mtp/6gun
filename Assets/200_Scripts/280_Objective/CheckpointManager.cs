using UnityEngine;

public class CheckpointManager : MonoBehaviour
{


    private static Vector3 checkpointPosition;
    public static float SavedHealth;
    private void Start()
    {
        checkpointPosition = transform.position; // Le point de d�part initial devient le premier checkpoint
    }

    // Fonction pour d�finir un nouveau checkpoint
    public static void SetCheckpoint(Vector3 position, float savedHealth)
    {
        SavedHealth = savedHealth;
        checkpointPosition = position;


    }

    // Fonction pour retourner au dernier checkpoint
    public static void ReturnToCheckpoint(Transform _player)
    {
        _player.position = checkpointPosition;

        HealthManager.health = SavedHealth;

        Debug.Log("TP");
    }
}
