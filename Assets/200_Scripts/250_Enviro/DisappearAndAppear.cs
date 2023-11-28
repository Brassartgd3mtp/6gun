using System.Collections;
using UnityEngine;

public class DisappearAndAppear : MonoBehaviour
{
    public GameObject objectToToggle; // L'objet que vous voulez faire dispara�tre et appara�tre.
    public float toggleInterval = 2.0f; // L'intervalle en secondes entre les changements d'�tat.

    private bool isObjectActive = true;

    private void Start()
    {
        // D�marrer la coroutine pour effectuer les changements d'�tat � intervalles r�guliers.
        StartCoroutine(ToggleObject());
    }

    private IEnumerator ToggleObject()
    {
        while (true)
        {
            // Inverser l'�tat de l'objet actif.
            isObjectActive = !isObjectActive;

            // Activer ou d�sactiver l'objet en fonction de l'�tat.
            if (objectToToggle != null) objectToToggle.SetActive(isObjectActive);
            else Destroy(this);

            // Attendre pendant l'intervalle sp�cifi� avant de continuer.
            yield return new WaitForSeconds(toggleInterval);
        }
    }
}
