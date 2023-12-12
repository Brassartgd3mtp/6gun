using UnityEngine;

public class Reload : MonoBehaviour
{
    [Header("Reload")]
    [SerializeField] private float reloadTimeSet = 0.15f;
    [SerializeField] private float reloadTimeCountdown;

    [Header("References")]
    [SerializeField] private RectTransform selector;
    [SerializeField] private CanvasToggle canvasToggle;

    private Barrel barrel;
    private BarrelFader barrelFader;

    private void Start()
    {
        barrel = gameObject.GetComponent<Barrel>();
        barrelFader = FindObjectOfType<BarrelFader>();

        reloadTimeCountdown = reloadTimeSet;
    }

    void Update()
    {
        if (!canvasToggle.isGamePaused)
        {
            //Je cr�e un d�lai de recharge pour �quilibrer le gameplay entre les diff�rents profiles de joueurs
            if (reloadTimeCountdown > 0) reloadTimeCountdown -= Time.deltaTime;

            //Quand je recharge (et si le chargeur n'est pas d�j� plein), je cr�e un balle, je lui change sa couleur et je l'ajoute au Stock
            if (Input.GetButtonDown("Reload") && barrel.barrelStock[Barrel.SelectedForReload] == Color.black && reloadTimeCountdown <= 0)
            {
                barrelFader.Delay = barrelFader.MaxDelay;
                barrelFader.canvaGroup.alpha = 1;

                /// <summary>
                /// En fonction de la position de mon aiguille dans la roue au moment o� on appuye sur la recharge,
                /// je modifie la couleur de l'emplacement de recharge par rapport � la couleur de zone dans laquelle est l'aiguille.
                /// 
                /// //Cette fa�on dont je g�re la position de l'aiguille n'est pas tr�s pratique (surtout si on veut plus de deux �l�ments),
                /// � refacto si ce projet est choisit pour le PFE.
                /// </summary>
                if (selector.rotation.eulerAngles.z <= 180 && selector.rotation.eulerAngles.z > 0) 
                {
                    ReloadWaterSound();
                    barrel.AddStock(Color.blue);
                }
                else
                {
                    ReloadFireSound();
                    barrel.AddStock(Color.red);
                }

                //Je r�initialise le delay de recharge
                reloadTimeCountdown = reloadTimeSet;
            }
        }
    }
    public void ReloadFireSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(12, audioSource);
    }
    public void ReloadWaterSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(13, audioSource);
    }
}