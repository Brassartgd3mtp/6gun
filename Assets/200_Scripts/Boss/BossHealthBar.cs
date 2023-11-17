using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; 

public class BossHealthBarUpdater : MonoBehaviour
{
    public Slider bossRedSlider;
    public Slider bossBlueSlider;
    public TextMeshProUGUI endText;
    public float fadeDuration = 1.0f; // Dur�e du fondu en secondes
    public float displayDuration = 3.0f; // Dur�e d'affichage du texte

    private int bossRedTotal;
    private int bossBlueTotal;

    private void Start()
    {
        // Initialiser les valeurs totales des boss
        bossRedTotal = CountObjectsWithTag("BossRed");
        bossBlueTotal = CountObjectsWithTag("BossBlue");
    }

    private void Update()
    {
        // Mettre � jour les barres de progression
        UpdateHealthBars();

        // V�rifier si les barres de vie sont vides et d�sactiver l'objet si c'est le cas
        CheckAndDisableHealthBars();
    }

    private void UpdateHealthBars()
    {
        // Calculer la progression des boss rouges et bleus
        float bossRedProgress = CalculateProgress("BossRed");
        float bossBlueProgress = CalculateProgress("BossBlue");

        // Mettre � jour les barres de progression
        bossRedSlider.value = bossRedProgress;
        bossBlueSlider.value = bossBlueProgress;
    }

    private int CountObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        return objects.Length;
    }

    private float CalculateProgress(string tag)
    {
        int bossCount = CountObjectsWithTag(tag);
        int bossTotal = (tag == "BossRed") ? bossRedTotal : bossBlueTotal;

        // �viter une division par z�ro
        if (bossTotal == 0)
        {
            return 0f;
        }

        return (float)bossCount / bossTotal;
    }

    private void CheckAndDisableHealthBars()
    {
        // D�sactiver les barres de vie si elles sont vides
        if (bossRedSlider.value == 0f && bossBlueSlider.value == 0f)
        {
            bossRedSlider.gameObject.SetActive(false);
            bossBlueSlider.gameObject.SetActive(false);

            // D�marrer les coroutines pour le fondu du texte et le d�lai
            StartCoroutine(FadeInText());
        }
    }

    private IEnumerator FadeInText()
    {
        float startTime = Time.time;
        Color startColor = endText.color;

        // Progression du fondu
        while (Time.time - startTime < fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            endText.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(0.0f, 1.0f, t));
            yield return null;
        }

        // Assurez-vous que le texte est compl�tement opaque
        endText.color = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

        // Attendre la dur�e d'affichage du texte
        yield return new WaitForSeconds(displayDuration);

        // D�marrer la coroutine pour le fondu sortant
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        float startTime = Time.time;
        Color startColor = endText.color;

        // Progression du fondu sortant
        while (Time.time - startTime < fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            endText.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1.0f, 0.0f, t));
            yield return null;
        }

        // Assurez-vous que le texte est compl�tement transparent
        endText.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
    }
}
