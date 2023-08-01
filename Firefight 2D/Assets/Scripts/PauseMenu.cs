using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public PlayerHealth playerHealth;  // Reference to the PlayerHealth component

    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);

        // Get a reference to the PlayerHealth component
        GameObject playerObject = GameObject.Find("Player"); // replace "Player" with the name of your player object in the scene
        if (playerObject != null) {
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth != null && playerHealth.health <= 0) // Assuming health is a public field in PlayerHealth
        {
            PauseGame();
            if(isPaused && Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                ResumeGame();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
        
    }


    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }


    public void GoToMainMenu()
    {
        
        SceneManager.LoadScene("StartingMap");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
