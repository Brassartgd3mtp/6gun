using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource; // R�f�rence � l'AudioSource.

    private void Start()
    {
        // Assurez-vous que l'AudioSource est r�f�renc� dans l'inspecteur.
        if (audioSource == null)
        {
            Debug.LogError("Veuillez assigner l'AudioSource dans l'inspecteur.");
        }
    }

    // Fonction pour jouer le son.
    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
