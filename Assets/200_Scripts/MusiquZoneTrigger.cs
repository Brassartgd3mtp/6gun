using UnityEngine;
using UnityEngine.UI;

public class MusiqueZoneTrigger : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public float fadeSpeed = 1.0f;

    public Slider volumeSlider;  // R�f�rence au Slider pour ajuster le volume

    private AudioSource playerAudio;
    private AudioSource backgroundAudio;

    private bool isInZone = false;

    void Start()
    {
        // Assurez-vous que l'objet "Player" a un composant AudioSource attach�
        playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        // Assurez-vous que cet objet (GameManager) a un composant AudioSource attach�
        backgroundAudio = GetComponent<AudioSource>();
        backgroundAudio.clip = backgroundMusic;
        backgroundAudio.volume = 0.0f;
        backgroundAudio.Play();
    }

    void Update()
    {
        if (isInZone)
        {
            // Si le volume du slider est modifi�, ajuster le volume en cons�quence
            if (volumeSlider != null)
            {
                backgroundAudio.volume = volumeSlider.value;
            }

            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false;
        }
    }

    void FadeIn()
    {
        // Si le volume n'a pas atteint la valeur maximale du slider, augmenter progressivement
        if (backgroundAudio.volume < volumeSlider.value)
        {
            backgroundAudio.volume += fadeSpeed * Time.deltaTime;
        }
    }

    void FadeOut()
    {
        // Si le volume n'a pas atteint z�ro, diminuer progressivement
        if (backgroundAudio.volume > 0.0f)
        {
            backgroundAudio.volume -= fadeSpeed * Time.deltaTime;
        }
    }
}
