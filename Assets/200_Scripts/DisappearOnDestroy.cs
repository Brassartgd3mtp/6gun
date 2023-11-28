using UnityEngine;

public class DisappearOnDestroy : MonoBehaviour
{
    public GameObject objectToDisappear; // L'objet que vous souhaitez faire dispara�tre
    public string destroyableTag = "Destroyable"; // Tag pour les objets d�truisibles

    private void Update()
    {
        // V�rifie si tous les objets avec le tag "Destroyable" sont d�truits
        if (AllObjectsDestroyedWithTag(destroyableTag))
        {
            // Fait dispara�tre l'objet sp�cifi�
            if (objectToDisappear != null)
            {
                objectToDisappear.SetActive(false);
                

            }

            // Vous pouvez �galement d�truire l'objet au lieu de le d�sactiver si vous le souhaitez
            // Destroy(objectToDisappear);
        }
    }

    private void PlaySound()
    {
        if (objectToDisappear == null)
        {
            DoorSound();

        }

    }

    // Fonction pour v�rifier si tous les objets avec un tag sp�cifi� sont d�truits
    private bool AllObjectsDestroyedWithTag(string tag)
    {

       
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        return objectsWithTag.Length == 0;
    }

    public void DoorSound(float volume = 1.0f)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        
        AudioManager.Instance.PlaySound(20, audioSource);
    }

}
