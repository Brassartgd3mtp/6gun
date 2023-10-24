using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectCounter : MonoBehaviour
{
    public TextMeshProUGUI countText; // R�f�rence � l'objet TextMeshProUGUI
    public GameObject objectToDisappear; // R�f�rence � l'objet � faire dispara�tre
    public TextMeshProUGUI displayText; // R�f�rence au TextMeshProUGUI pour afficher le texte
    public float fadeDuration = 1.0f; // Dur�e du fondu en secondes
    public float displayDuration = 3.0f; // Dur�e d'affichage du texte
    private int blueObjectCount = 0;
    private int redObjectCount = 0;

    private void Start()
    {
        displayText.gameObject.SetActive(false);
        UpdateCountText();
    }

    private void Update()
    {
        CountObjectsByLayer();
        UpdateCountText();

        // Si le compteur atteint 0, affichez le texte et d�marrez la s�quence de fondu
        if (blueObjectCount == 0 && redObjectCount == 0)
        {
            objectToDisappear.SetActive(false);
            displayText.gameObject.SetActive(true);
            StartCoroutine(FadeAndHideText());
        }
    }

    private void CountObjectsByLayer()
    {
        GameObject[] blueObjects = GameObject.FindGameObjectsWithTag("Destroyable");
        blueObjectCount = blueObjects.Length;

        GameObject[] redObjects = GameObject.FindGameObjectsWithTag("Destroyable");
        redObjectCount = redObjects.Length;
    }

    private void UpdateCountText()
    {
        countText.text = "Objets  : " + blueObjectCount;
    }

    private IEnumerator FadeAndHideText()
    {
        float startTime = Time.time;
        Color startColor = displayText.color;

        yield return new WaitForSeconds(displayDuration);

        while (Time.time - startTime < fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            displayText.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1.0f, 0.0f, t));
            yield return null;
        }

        // Assurez-vous que le texte est compl�tement transparent et d�sactivez-le
        displayText.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
        displayText.gameObject.SetActive(false);

        // D�sactivez l'objet � faire dispara�tre
        objectToDisappear.SetActive(false);
    }
}
