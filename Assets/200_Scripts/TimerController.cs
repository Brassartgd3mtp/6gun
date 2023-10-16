using System.Collections;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private bool isTimerRunning = false;
    private float timer = 0.0f;

    private void Start()
    {
        // D�sactivez le timer au d�marrage
        isTimerRunning = false;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            // Incr�mente le timer
            timer += Time.deltaTime;
            //Debug.Log("Temps �coul� : " + timer.ToString("F2")); // Affiche le temps �coul�

            // Ici, vous pouvez ajouter d'autres actions � effectuer pendant le timer
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartZone"))
        {
            // Le joueur entre dans la zone de d�part, d�marre le timer
            isTimerRunning = true;
        }
        else if (other.CompareTag("StopZone"))
        {
            // Le joueur entre dans la zone d'arr�t, arr�te le timer
            isTimerRunning = false;
            //Debug.Log("Temps total : " + timer.ToString("F2")); // Affiche le temps total
        }
    }
    public bool IsRunning()
    {
        return isTimerRunning;
    }
    public float GetTimer()
    {
        return timer;
    }
}
