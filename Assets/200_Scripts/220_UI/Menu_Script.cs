using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    // M�thode appel�e lorsqu'on appuie sur le bouton
    public void RelancerLeNiveau()
    {
        // Charger � nouveau le m�me niveau
        SceneManager.LoadScene(1);
    } 
    
    public void RetourMenu()
    {
        // Charger � nouveau le m�me niveau
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}