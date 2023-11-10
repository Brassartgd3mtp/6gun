using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public SceneFader sceneFader; // R�f�rence au script de fondu
    public string sceneToLoad;
    public Canvas[] canvasesToDisable; // Liste des Canvas � d�sactiver

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // D�sactiver tous les Canvas de la liste
            foreach (Canvas canvas in canvasesToDisable)
            {
                canvas.enabled = false;
            }

            // Appel de la transition avec fondu
            sceneFader.FadeToScene(sceneToLoad);
        }
    }
}
