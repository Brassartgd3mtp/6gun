using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TimerController timerController; // Assurez-vous de faire r�f�rence au script TimerController
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        // Obtenez la r�f�rence au composant TextMeshProUGUI
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Masquez le texte du timer au d�marrage
        textMeshPro.enabled = false;
    }

    private void Update()
    {
        if (timerController != null)
        {
            // V�rifiez si le timer est en cours d'ex�cution
            if (timerController.IsRunning())
            {
                // Affichez le texte avec le temps �coul� du TimerController
                textMeshPro.enabled = true;
                textMeshPro.text = "Temps : " + timerController.GetTimer().ToString("F2");
            }
            else
            {
                // Masquez le texte si le timer n'est pas en cours d'ex�cution
                textMeshPro.enabled = false;
            }
        }
    }
}
