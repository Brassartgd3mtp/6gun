using UnityEngine;
using TMPro;

public class ZoneScript : MonoBehaviour
{
    public TextMeshProUGUI playerText; // R�f�rence au composant TextMeshProUGUI du joueur
    public string zoneText = "Votre texte ici";
    public KeyCode pauseKey = KeyCode.P;

    private bool isPaused = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPaused)
        {
            // D�marrer la coroutine pour afficher le texte progressivement
            StartCoroutine(DisplayText(zoneText));
        }
    }

    private System.Collections.IEnumerator DisplayText(string textToDisplay)
    {
        Time.timeScale = 0f;
        isPaused = true;

        if (playerText != null)
        {
            playerText.text = ""; // Assurez-vous que le texte soit vide au d�but

            // Afficher le texte progressivement
            for (int i = 0; i <= textToDisplay.Length; i++)
            {
                playerText.text = textToDisplay.Substring(0, i);
                yield return new WaitForSecondsRealtime(0.05f); // Ajustez la vitesse d'affichage selon vos pr�f�rences
            }

            // Attendre avant de permettre au joueur de reprendre le jeu
            yield return new WaitForSecondsRealtime(1.0f);

            // Effacer le texte progressivement
            for (int i = textToDisplay.Length; i >= 0; i--)
            {
                playerText.text = textToDisplay.Substring(0, i);
                yield return new WaitForSecondsRealtime(0.05f); // Ajustez la vitesse de disparition selon vos pr�f�rences
            }

            // R�activer le jeu imm�diatement apr�s l'affichage et avant la disparition du texte
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            Debug.LogError("La r�f�rence du TextMeshProUGUI du joueur n'est pas d�finie.");
        }
    }

    private void Update()
    {
        // V�rifier si la touche de reprise est press�e
        if (isPaused && Input.GetKeyDown(pauseKey))
        {
            // Arr�ter la coroutine si la touche de reprise est press�e
            StopAllCoroutines();

            // R�activer le jeu imm�diatement
            Time.timeScale = 1f;
            isPaused = false;

            // Effacer le texte imm�diatement
            playerText.text = "";
        }
    }
}
