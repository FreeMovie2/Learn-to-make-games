using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour
{
    public Text highscoretxt;

    private void Start()
    {
        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoretxt.text = "Highscore = " + highscore.ToString();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Game is exiting");
        }
    }
}
