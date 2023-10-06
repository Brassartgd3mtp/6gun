using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField] private int shootForce, upwardForce;

    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int magazineSize;

    [SerializeField] private Camera fpCam;
    [SerializeField] private Transform attackPoint; //Je r�cup�re un point dans ma sc�ne qui correspond au bout de mon canon

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shoot();
    }

    private void Shoot()
    {
        Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Cr�e un point de r�f�rence au centre de l'�cran (� ne pas confondre avec le pointeur)
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit)) //Je lance un Raycast avec comme point de d�part ma variable Ray, et je check s'il touche quelque chose
        {
            targetPoint = hit.point; //Je r�cup�re le point de collision de mon Raycast
            if (hit.transform.tag == "Destroyable")
            {
                Destroy(hit.transform.gameObject); //Je d�truis l'objet touch� s'il a le tag "Destroyable"
            }
        }
        else
            targetPoint = ray.GetPoint(75); //S'il ne touche rien, je r�cup�re un point vide � une distance assez loin

        Vector3 direction = targetPoint - attackPoint.position; //Je d�duis la position de ma target � celle de mon canon (pour que la balle parte en direction du centre de la cam�ra)

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //Je cr�e une instance de la balle instanci�e)

        currentBullet.transform.forward = direction.normalized; //Je d�finit dans quelle direction elle doit aller

        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse); //Je lui applique une v�locit� en direction de ma cible
        currentBullet.GetComponent<Rigidbody>().AddForce(fpCam.transform.up * upwardForce, ForceMode.Impulse);
    }
}
