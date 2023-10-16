using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 10.0f; // Force du dash
    public float dashDuration = 0.2f; // Dur�e du dash
    public float dashCooldown = 1.0f; // Temps de recharge entre les dashes
    public float timer;
    private bool canDash = true; // Indicateur permettant de savoir si le joueur peut effectuer un dash
    private Rigidbody rb; // R�f�rence au Rigidbody du joueur
    [SerializeField] private Camera fovEffect; // R�f�rence � la cam�ra du joueur
    private PlayerController playerController;
    public Image DashBarImage;

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
            if (canDash)

            {
                if (Input.GetKeyDown(KeyCode.LeftShift)) // Changez la touche selon vos pr�f�rences
                {
                    StartCoroutine(Dash());
                    timer = 1; 
                }
            }
             
        }
        else timer -= Time.deltaTime;
    }

    private IEnumerator Dash()
    {
        canDash = false; //Je d�sactive le dash pendant la recharge

        playerController.moveDirection.y = 0;

        rb.AddForce(playerController.moveDirection * dashForce, ForceMode.Impulse);

        yield return new WaitForSeconds(dashDuration);

        //J'attends la p�riode de recharge
        yield return new WaitForSeconds(dashCooldown);

        canDash = true; //Je r�active le dash
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
