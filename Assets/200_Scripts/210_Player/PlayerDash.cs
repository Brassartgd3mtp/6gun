using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerDash : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float dashForce = 10.0f; // Force du dash
    [SerializeField] private float dashDuration = 0.2f; // Dur�e du dash
    [SerializeField] private float dashCooldown = 1.0f; // Temps de recharge entre les dashes
    private float cooldownTimer;
    private Vector3 direction;

    [Header("References")]
    [SerializeField] private Image DashBarImage;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera fovEffect; // R�f�rence � la cam�ra du joueur

    private bool canDash = true; // Indicateur permettant de savoir si le joueur peut effectuer un dash
    private bool isDashing = false; // Ajoutez une variable pour savoir si le joueur est en train de dasher

    private PlayerController playerController;
    private Rigidbody rb;

    private void Start()
    {
        cooldownTimer = dashCooldown; 
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        DashBarImage.fillAmount = cooldownTimer;
        if (cooldownTimer <= 0)
        {
            if (canDash && Input.GetButtonDown("Dash")) // Changez la touche selon vos pr�f�rences
            {
                direction = playerController.moveDirection;
                StartCoroutine(Dash());
                cooldownTimer = 1;
            }
        }
        else cooldownTimer -= Time.deltaTime;
    }


    private IEnumerator Dash()
    {
        if (!canDash || isDashing) yield break; // V�rifiez si le dash est possible et si le joueur n'est pas d�j� en train de dasher

        isDashing = true; // Le joueur commence � dasher
        float dashTimer = 0f;

        while (dashTimer < dashDuration)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(direction * dashForce, ForceMode.Force);
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
