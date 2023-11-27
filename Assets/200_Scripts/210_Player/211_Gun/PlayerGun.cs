using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera fpCam;
    [SerializeField] private Renderer nextBulletColor;
    [SerializeField] private TextMeshProUGUI reloadText;
    [SerializeField] private CanvasToggle canvasToggle;
    [SerializeField] private BarrelRotate barrelRotate;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform canonPos;
    [SerializeField] private LayerMask hitableColliders;

    [Header("Particles")]
    public ParticleManager particleManager;
    public GameObject bossRedParticlePrefab;
    public GameObject bossBlueParticlePrefab;

    private Barrel barrel;
    private float shootDelay = 0.01f;
    private bool canShoot = true;

    private int bossRedTotal = 0;
    private int bossBlueTotal = 0;
    private int bossRedCount = 0;
    private int bossBlueCount = 0;

    private void Start()
    {
        barrel = GetComponent<Barrel>();

        // Comptez le nombre total d'objets "BossRed" et "BossBlue" dans la scène
        GameObject[] bossRedObjects = GameObject.FindGameObjectsWithTag("BossRed");
        GameObject[] bossBlueObjects = GameObject.FindGameObjectsWithTag("BossBlue");
        bossRedTotal = bossRedObjects.Length;
        bossBlueTotal = bossBlueObjects.Length;
    }


    private void Update()
    {
        if (!canvasToggle.isGamePaused)
        {
            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                canShoot = false;
                Shoot();
                Invoke(nameof(ShootDelay), shootDelay);
            }

            if (Input.GetButtonDown("Dump")) Dump();

            if (barrel.barrelStock[Barrel.SelectedBullet] != Color.black) reloadText.enabled = false;
            if (barrel.barrelStock[Barrel.SelectedBullet] == Color.black)
            {
                reloadText.enabled = true;
                nextBulletColor.material.color = Color.black;
            }
            else
                nextBulletColor.material.color = barrel.barrelStock[Barrel.SelectedBullet];
        }

        CheckMagicObjects();
    }

    private void ShootDelay()
    {
        canShoot = true;
    }

    private void Shoot()
    {
        if (barrel.barrelStock[Barrel.SelectedBullet] != Color.black && !Cursor.visible)
        {
            ShotFire();
            Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            barrelRotate.Rotate();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitableColliders))
            {
                if (hit.collider.gameObject.layer == 10)
                {
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth is not null)
                    {
                        enemyHealth.TakeDamage(10);
                    }
                }

                if (hit.transform.CompareTag("Destroyable") && hit.collider.gameObject.layer == 0)
                {
                    Destroy(hit.transform.gameObject);
                }

                if (barrel.barrelStock[Barrel.SelectedBullet] == Color.red)
                {
                    if (hit.transform.CompareTag("BossRed") && hit.collider.gameObject.layer == 9)
                    {
                        Destroy(hit.transform.gameObject);
                        bossRedCount++;

                        if (bossRedParticlePrefab != null)
                        {
                            Instantiate(bossRedParticlePrefab, hit.point, Quaternion.identity);
                            Debug.Log("Particule");
                        }
                    }
                }

                else if (barrel.barrelStock[Barrel.SelectedBullet] == Color.blue)
                {
                    if (hit.transform.CompareTag("BossBlue") && hit.collider.gameObject.layer == 8)
                    {
                        Destroy(hit.transform.gameObject);
                        bossBlueCount++;

                        if (bossBlueParticlePrefab != null)
                        {
                            Instantiate(bossBlueParticlePrefab, hit.point, Quaternion.identity);
                        }
                    }
                }

                particleManager.Impact(barrel.barrelStock[Barrel.SelectedBullet], hit.point, hit.normal);
            }

            Vector3 direction = hit.point - canonPos.position;

            GameObject _bullet = Instantiate(bullet, canonPos.position, Quaternion.identity);
            PlayerController playerController = FindObjectOfType<PlayerController>();

            _bullet.transform.forward = direction.normalized + playerController.Orientation.forward;
            _bullet.transform.up = direction.normalized + playerController.Orientation.forward;

            _bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * 10, ForceMode.Impulse);
            _bullet.GetComponent<Renderer>().material.color = barrel.barrelStock[Barrel.SelectedBullet];


            barrel.NextBullet();
            //Destroy(_bullet.gameObject);
        }
    }

    private void Dump()
    {
        bossRedCount = 0;
        bossBlueCount = 0;

        for (int i = 0; i < barrel.barrelStock.Count; i++)
        {
            barrel.barrelStock[i] = Color.black;
        }
    }

    private void CheckMagicObjects()
    {
        GameObject[] bossRedObjects = GameObject.FindGameObjectsWithTag("BossRed");
        GameObject[] bossBlueObjects = GameObject.FindGameObjectsWithTag("BossBlue");

        if (bossRedObjects.Length == 0 && bossBlueObjects.Length == 0)
        {
            GameObject[] magicObjects = GameObject.FindGameObjectsWithTag("Magic");
            foreach (var magicObject in magicObjects)
            {
                Destroy(magicObject);
            }
        }
    }

    public void ShotFire()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(14, audioSource);
    }










}
