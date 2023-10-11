using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public float interactRange = 10f;
    [SerializeField] private Camera fpCam;
    [SerializeField] private Barrel barrel;
    public static List<GameObject> RedDoors = new List<GameObject>();
    public static List<GameObject> BlueDoors = new List<GameObject>();

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Le joueur tire
        {
            Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Debug.Log("Shoot");
            if(barrel.barrelStock.Count > 0)
            {
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    if (barrel.barrelStock[0] == Color.blue && hit.collider.CompareTag("BlueButton"))
                    {
                        Debug.Log("BlueButton");
                        // Le joueur a tir� sur un objet bleu, d�sactivez les portes bleues et les portes rouges
                        foreach (GameObject door in RedDoors)
                        {
                            door.SetActive(true);
                        }
                        foreach (GameObject door in BlueDoors)
                        {
                            door.SetActive(false);
                        }
                    }
                    else if (barrel.barrelStock[0] == Color.red && hit.collider.CompareTag("RedButton"))
                    {
                        Debug.Log("Redbutton");
                        // Le joueur a tir� sur un objet rouge, d�sactivez les portes rouges et les portes bleues
                        foreach (GameObject door in RedDoors)
                        {
                            door.SetActive(false);
                        }
                        foreach (GameObject door in BlueDoors)
                        {
                            door.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        RedDoors.Clear();
        BlueDoors.Clear();
    }
}
