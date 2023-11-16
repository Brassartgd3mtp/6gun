using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    public Canvas canvas; // R�f�rence au Canvas que vous souhaitez activer/d�sactiver
    public bool isGamePaused = false;
    public HealthManager healthManager;
    public InteractableItem interactableItem;
    private void Start()
    {
        if (canvas != null)
        {
            // Au d�marrage, d�sactivez le Canvas
            canvas.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !healthManager.Escape)
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (canvas != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !healthManager.Escape)
            {
                // Inversez l'�tat du Canvas (activ� ou d�sactiv�) lorsque la touche "Echap" est enfonc�e
                canvas.enabled = !canvas.enabled;
            }
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0f; // Mettez le temps � z�ro pour mettre le jeu en pause
        isGamePaused = true;

        // Affichez le curseur de la souris pour permettre l'interaction avec l'interface utilisateur
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResumeGame()
    {
        if(!interactableItem.buttonAlreadyPressed) Time.timeScale = 1f; // R�tablissez le temps normal pour reprendre le jeu
        isGamePaused = false;

        // Cachez � nouveau le curseur de la souris
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
