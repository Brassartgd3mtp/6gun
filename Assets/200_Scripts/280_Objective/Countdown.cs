using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float countdownDuration = 60.0f; // Dur�e du compte � rebours en secondes
    public TextMeshProUGUI countdownText; // R�f�rence au composant TextMesh Pro de l'UI
    private float timeRemaining;
    private bool isPlayerInsideZone = false;

    private void Start()
    {
        // D�sactivez le composant TextMesh Pro au d�but
        countdownText.enabled = false;
        timeRemaining = countdownDuration;
    }

    private void Update()
    {
        // V�rifiez si le joueur est dans la zone
        if (isPlayerInsideZone)
        {
            if (timeRemaining > 0)
            {
                // D�cr�mentez le temps restant
                timeRemaining -= Time.deltaTime;

                // Activez le composant TextMesh Pro s'il ne l'est pas d�j�
                countdownText.enabled = true;

                // Mettez � jour le composant TextMesh Pro avec le temps restant format�
                UpdateCountdownText();
            }
            else
            {
                // Le compte � rebours est termin�, effectuez l'action souhait�e ici.
                Debug.Log("Compte � rebours termin�, faites quelque chose !");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur est entr� dans la zone
            isPlayerInsideZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur est sorti de la zone
            isPlayerInsideZone = false;

            // D�sactivez le composant TextMesh Pro
            countdownText.enabled = false;
        }
    }

    void UpdateCountdownText()
    {
        // Formattez le temps restant en minutes et secondes
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Mettez � jour le texte de l'UI
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
