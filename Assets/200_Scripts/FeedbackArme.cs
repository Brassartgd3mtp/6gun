using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // Variable publique pour l'Animator

    private void Update()
    {
        if (animator == null)
        {
            Debug.LogError("L'Animator n'a pas �t� assign�.");
            return;
        }

        // V�rifie si le joueur est en mouvement
        bool isMoving = PlayerController.horizontalInput != 0 || PlayerController.verticalInput != 0;

        // Active ou d�sactive l'animation en fonction de l'�tat de mouvement
        animator.SetBool("IsMoving", isMoving);
    }
}
