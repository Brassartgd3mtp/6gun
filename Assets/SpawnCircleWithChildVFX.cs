using UnityEngine;

public class ActivateCircleWithDelayedVFX : MonoBehaviour
{
    public GameObject circle; // Faites glisser votre cercle rouge d�sactiv� depuis l'Inspector Unity.
    public GameObject vfxPrefab; // Le pr�fab du VFX � attacher en tant qu'enfant.
    public float minActivationTime = 2.0f; // Temps minimum d'attente avant l'activation.
    public float maxActivationTime = 5.0f; // Temps maximum d'attente avant l'activation.
    public float vfxDelay = 1.0f; // D�lai avant l'apparition du VFX apr�s l'activation du cercle rouge.
    public float vfxLifetime = 5.0f; // Dur�e de vie du VFX en secondes.

    private void Start()
    {
        // Assurez-vous que le cercle rouge est d�sactiv� au d�but.
        if (circle != null)
        {
            circle.SetActive(false);
        }

        // Appelez la m�thode ActivateRandomly � intervalles al�atoires.
        InvokeRepeating("ActivateRandomly", Random.Range(minActivationTime, maxActivationTime), Random.Range(minActivationTime, maxActivationTime));
    }

    private void ActivateRandomly()
    {
        if (circle != null)
        {
            // Activez le cercle rouge de mani�re al�atoire.
            circle.SetActive(!circle.activeSelf);

            if (circle.activeSelf)
            {
                // Si le cercle rouge est activ�, appelez la m�thode ActivateVFX avec un d�lai.
                Invoke("ActivateVFX", vfxDelay);
            }
        }
    }

    private void ActivateVFX()
    {
        if (vfxPrefab != null)
        {
            // Instanciez le VFX en tant qu'enfant du cercle.
            GameObject vfxInstance = Instantiate(vfxPrefab, circle.transform);
            vfxInstance.transform.localPosition = Vector3.zero; // R�initialise la position locale du VFX par rapport au cercle.

            // D�truisez le VFX apr�s la dur�e de vie sp�cifi�e.
            Destroy(vfxInstance, vfxLifetime);
        }
    }
}
