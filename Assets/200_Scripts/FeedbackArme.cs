using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // Variable publique pour l'Animator
    private PlayerController playerController; // Assurez-vous que le script PlayerController est correctement attach�

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (animator == null)
        {
            Debug.LogError("L'Animator n'a pas �t� assign�.");
            return;
        }

        // V�rifie si le joueur est en mouvement
        bool isMoving = playerController.horizontalInput != 0 || playerController.verticalInput != 0;

        // Active ou d�sactive l'animation en fonction de l'�tat de mouvement
        animator.SetBool("IsMoving", isMoving);
    }
}
