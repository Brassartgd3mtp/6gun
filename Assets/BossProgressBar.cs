using UnityEngine;
using UnityEngine.UI;

public class BossProgressBar : MonoBehaviour
{
    public GameObject bossRedPrefab; // Attachez le prefab de l'objet "BossRed" dans l'Inspector Unity
    public GameObject bossBluePrefab; // Attachez le prefab de l'objet "BossBlue" dans l'Inspector Unity
    public Slider progressBar; // Attachez la barre d'UI (Slider) dans l'Inspector Unity

    private int bossRedTotal = 0;
    private int bossBlueTotal = 0;
    private int bossRedCount = 0;
    private int bossBlueCount = 0;

    private void Start()
    {
        // Comptez le nombre total d'objets "BossRed" et "BossBlue" dans la sc�ne
        GameObject[] bossRedObjects = GameObject.FindGameObjectsWithTag("BossRed");
        GameObject[] bossBlueObjects = GameObject.FindGameObjectsWithTag("BossBlue");
        bossRedTotal = bossRedObjects.Length;
        bossBlueTotal = bossBlueObjects.Length;

        // Mettez � jour la barre de progression
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        // Calculez la proportion des boss rouges et bleus par rapport au nombre total
        float bossRedProgress = (float)bossRedCount / bossRedTotal;
        float bossBlueProgress = (float)bossBlueCount / bossBlueTotal;

        // Choisissez le pourcentage le plus bas entre les deux
        float overallProgress = Mathf.Min(bossRedProgress, bossBlueProgress);

        // Mettez � jour la barre de progression en fonction de la proportion calcul�e
        progressBar.value = overallProgress;
    }

    // Appelez cette fonction chaque fois qu'un objet "BossRed" est d�truit
    public void BossRedDestroyed()
    {
        bossRedCount++;
        UpdateProgressBar();
    }

    // Appelez cette fonction chaque fois qu'un objet "BossBlue" est d�truit
    public void BossBlueDestroyed()
    {
        bossBlueCount++;
        UpdateProgressBar();
    }
}
