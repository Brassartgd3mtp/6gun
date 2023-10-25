using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckpointText : MonoBehaviour
{
    public float displayDuration = 3f; // Dur�e d'affichage du texte en secondes

    private TextMeshProUGUI textComponent;
    private bool isDisplaying;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.enabled = false; // D�sactivez le texte au d�marrage
    }

    public void ShowCheckpointText(string message)
    {
        if (!isDisplaying)
        {
            textComponent.text = message;
            textComponent.enabled = true;
            isDisplaying = true;
            StartCoroutine(HideTextAfterDelay());
        }
    }

    IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        textComponent.enabled = false;
        isDisplaying = false;
    }
}
