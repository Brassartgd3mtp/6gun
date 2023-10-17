using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 10.0f; // Force du dash
    //public float dashUpward = 0.0f; // Force du dash
    public float dashDuration = 0.2f; // Dur�e du dash
    public float dashCooldown = 1.0f; // Temps de recharge entre les dashes
    public float timer;
    public Image DashBarImage;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera fovEffect; // R�f�rence � la cam�ra du joueur

    private bool canDash = true; // Indicateur permettant de savoir si le joueur peut effectuer un dash
    private Rigidbody rb; // R�f�rence au Rigidbody du joueur
    private PlayerController playerController;

    private void Start()
    {
        timer = dashCooldown; 
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        DashBarImage.fillAmount = timer;
        if (timer <= 0)
        {
            if (canDash && Input.GetKeyDown(KeyCode.LeftShift)) // Changez la touche selon vos pr�f�rences
            {
                StartCoroutine(Dash());
                timer = 1;
            }
        }
        else timer -= Time.deltaTime;
    }

    private bool isDashing = false; // Ajoutez une variable pour savoir si le joueur est en train de dasher

    private IEnumerator Dash()
    {
        if (!canDash || isDashing) yield break; // V�rifiez si le dash est possible et si le joueur n'est pas d�j� en train de dasher

        isDashing = true; // Le joueur commence � dasher
        float dashTimer = 0f;

        Vector3 initialVelocity = rb.velocity; // Vitesse actuelle du joueur

        while (dashTimer < dashDuration)
        {
            float t = dashTimer / dashDuration;
            // Interpolation lin�aire entre la vitesse actuelle et la vitesse de dash
            Vector3 dashVelocity = Vector3.Lerp(initialVelocity * dashForce, playerController.moveDirection, t);

            rb.velocity = new Vector3(dashVelocity.x, rb.velocity.y, dashVelocity.z);
            dashTimer += Time.deltaTime;
            // Attendre la prochaine frame
            yield return null;
        }

        isDashing = false; // Le dash est termin�
        Invoke("ResetDash", dashCooldown); // R�active le dash apr�s le temps de recharge
    }

    // Ajoutez cette m�thode pour r�initialiser le dash
    private void ResetDash()
    {
        canDash = true; // R�active le dash
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si le joueur entre en collision avec un objet pendant le dash, j'arr�te le dash
        if (collision.gameObject.tag == "Wall")
        {
            canDash = true;
            rb.velocity = Vector3.zero;
        }
    }
}
