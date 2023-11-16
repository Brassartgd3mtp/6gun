using UnityEngine;

public class VFXWithDebug : MonoBehaviour
{
    public GameObject vfxPrefab; // Le pr�fab des VFX que vous souhaitez faire appara�tre.
    public GameObject warningCirclePrefab; // Le pr�fab du cercle rouge.
    public int maxNumberOfVFX = 50; // Le nombre maximum de VFX � faire appara�tre.
    public Collider spawnArea; // Le Collider de la zone de spawn.
    public float spawnDelay = 1.0f; // Le d�lai entre chaque r�p�tition en secondes.
    public float vfxLifetime = 5.0f; // La dur�e de vie des VFX en secondes.

    private float timeSinceLastSpawn;
    private Transform spawnTransform;
    private int numberOfVFXSpawned;

    private void Start()
    {
        timeSinceLastSpawn = 0;
        spawnTransform = transform; // R�cup�re la transformation de l'objet vide.
        numberOfVFXSpawned = 0;
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnDelay && numberOfVFXSpawned < maxNumberOfVFX)
        {
            int numberOfVFXToSpawn = Mathf.Min(maxNumberOfVFX - numberOfVFXSpawned, 10); // Augmente progressivement de 10 VFX maximum.

            ShowDelayedCircles(numberOfVFXToSpawn);
            SpawnVFX(numberOfVFXToSpawn);
            timeSinceLastSpawn = 0;

            numberOfVFXSpawned += numberOfVFXToSpawn; // Incr�mente le nombre de VFX instanti�s.
        }
    }

    private void ShowDelayedCircles(int numberOfCircles)
    {
        if (spawnArea != null && warningCirclePrefab != null)
        {
            for (int i = 0; i < numberOfCircles; i++)
            {
                Vector3 randomPosition = GetRandomPositionInsideArea();
                //Debug.Log("Position du cercle : " + randomPosition); // Ajoutez un log pour afficher la position du cercle rouge.

                // Utilisez directement la position du VFX pour l'instanciation du cercle rouge.
                GameObject warningCircle = Instantiate(warningCirclePrefab, randomPosition, Quaternion.identity);
            }
        }
    }

    private void SpawnVFX(int numberOfVFXToSpawn)
    {
        if (spawnArea != null)
        {
            for (int i = 0; i < numberOfVFXToSpawn; i++)
            {
                Vector3 randomPosition = GetRandomPositionInsideArea();
                //Debug.Log("Position du VFX : " + randomPosition); // Ajoutez un log pour afficher la position du VFX.

                Quaternion rotation = Quaternion.Euler(0, 0, -90); // Rotation pr�cise de -90 degr�s sur l'axe Z.
                GameObject vfxInstance = Instantiate(vfxPrefab, randomPosition, rotation);

                // D�truit les VFX apr�s la dur�e de vie sp�cifi�e.
                Destroy(vfxInstance, vfxLifetime);
            }
        }
    }

    private Vector3 GetRandomPositionInsideArea()
    {
        Vector3 randomPosition = Vector3.zero;
        if (spawnArea != null)
        {
            Bounds bounds = spawnArea.bounds;
            randomPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
        return randomPosition;
    }
}
