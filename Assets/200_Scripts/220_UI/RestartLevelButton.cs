using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartLevelButton : MonoBehaviour
{
    private Scene sceneToRestart;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void Start()
    {
       sceneToRestart = SceneManager.GetActiveScene();
        Debug.Log(sceneToRestart.ToString());
    }

    public void RestartLevel()
    {
        // Chargez � nouveau la sc�ne actuelle
        SceneManager.LoadScene(sceneToRestart.name);
    }
}
