using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public GameObject deathUI;
    public void toggleDeathUI()
    {
        deathUI.SetActive(true);

    }

    public void restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
