using UnityEngine;
using System.Collections;

public class DashingPlayer : MonoBehaviour
{
    public float dashSpeed = 10.0f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing) // Utilisation de la touche "Shift" pour le dash
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        // R�cup�rer la direction de d�placement actuelle du joueur
        Vector3 dashDirection = transform.forward;

        // Utiliser un Raycast pour d�tecter les collisions avant de commencer le dash
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dashDirection, out hit, 1.0f))
        {
            // Si une collision est d�tect�e, ne pas effectuer le dash
            isDashing = false;
            yield break;
        }

        float startTime = Time.time;

        while (Time.time - startTime < dashDuration)
        {
            // D�placer le joueur dans la direction du dash
            transform.position += dashDirection * dashSpeed * Time.deltaTime;

            // Utiliser un Raycast continu pour v�rifier les collisions pendant le dash
            if (Physics.Raycast(transform.position, dashDirection, out hit, 1.0f))
            {
                // Si une collision est d�tect�e, arr�ter le dash
                isDashing = false;
                yield break;
            }

            yield return null;
        }

        isDashing = false;
    }
}
