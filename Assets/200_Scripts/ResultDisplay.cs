using UnityEngine;
using TMPro;

public class ResultDisplay : MonoBehaviour
{
    public TimerController timerController; // Assurez-vous de faire r�f�rence au script TimerController
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.enabled = false; // Masquez le texte du r�sultat au d�marrage
    }

    private void Update()
    {
        if (timerController != null)
        {
            if (!timerController.IsRunning())
            {
                // Affichez le r�sultat si le timer n'est pas en cours d'ex�cution
                textMeshPro.enabled = true;
                textMeshPro.text = "R�sultat : " + timerController.GetTimer().ToString("F2");
            }
            else
            {
                // Masquez le r�sultat si le timer est en cours d'ex�cution
                textMeshPro.enabled = false;
            }
        }
    }
}
