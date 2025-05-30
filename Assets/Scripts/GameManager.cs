using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int score = 0;
    public Text scoretxt;
    // Update is called once per frame
    void Update()
    {
        scoretxt.text = "Score = " + score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Game is exiting");
        }
    }

    public void killEnemy()
    {
        score += 10;
        if (score >= PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
