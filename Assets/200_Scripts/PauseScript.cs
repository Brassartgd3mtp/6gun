using TMPro;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    // R�f�rence au texte que vous souhaitez afficher
    public TextMeshProUGUI text;

    // La vitesse � laquelle le texte appara�t/dispara�t (modifiable dans l'inspecteur Unity)
    public float spawnSpeed = 1.0f;
    private float targetOpacity = 0.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            targetOpacity = 1.0f;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            targetOpacity = 0.0f;
    }

    void Update()
    {
        float newOpacity = Mathf.MoveTowards(text.color.a, targetOpacity, spawnSpeed * Time.deltaTime);
        text.color = new Color(text.color.r, text.color.g, text.color.b, newOpacity);
    }
}
