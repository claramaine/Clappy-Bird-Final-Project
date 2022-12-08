using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOverMenu;
    public Text scoreText;
    public Text gamePlayQuitText;
    public bool gameOver = false;
    private int score = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameOverMenu.SetActive(false);
            //staticCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
            //Destroy(staticCamera.GetComponent<AudioListener>());
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void BirdScored()
    {
        if (gameOver)
        {
            return;
        }
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void BirdDied()
    {
        gameOverMenu.SetActive(true);
        gamePlayQuitText.text = "";
        gameOver = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            } 
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }
}
