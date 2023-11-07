using System.Collections;
using TMPro;
using UnityEngine;

public class ZoneTriggerText : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float typingSpeed = 0.02f;
    public float displayDuration = 2.0f; // Dur�e d'affichage de chaque ligne de dialogue
    public float interSentenceDelay = 1.0f; // D�lai entre les phrases

    private int index;
    private bool isTriggered = false;

    void Start()
    {
        textDisplay.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(AnimateDialogue());
        }
    }

    IEnumerator AnimateDialogue()
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            yield return StartCoroutine(TypeSentence(sentences[i]));

            if (i == 1) // Deuxi�me phrase
            {
                CameraShake cameraShake = CameraShake.instance;
                if (cameraShake != null)
                {
                    cameraShake.Shake(); // D�clenche la secousse de la cam�ra
                }
                else
                {
                    Debug.LogError("CameraShake component not found on the camera.");
                }
            }

            yield return new WaitForSeconds(displayDuration);
            textDisplay.gameObject.SetActive(false);
            yield return new WaitForSeconds(interSentenceDelay);
            textDisplay.gameObject.SetActive(true);
        }

        textDisplay.text = "";
        textDisplay.gameObject.SetActive(false);
    }

    IEnumerator TypeSentence(string sentence)
    {
        textDisplay.gameObject.SetActive(true); // Active l'objet TextMeshProUGUI avant de commencer � �crire
        textDisplay.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}