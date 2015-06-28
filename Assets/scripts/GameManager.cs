using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int score = 0;
    private bool isScoreChanged = false;

    private int lives = 3;
    private bool isLivesChanged = false;

    private bool isGameWon = false;

    private LevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        isScoreChanged = true;
    }

    public void KillPlayer(GameObject player)
    {
        lives--;
        isLivesChanged = true;

        levelManager.KillPlayer(player, lives);
    }

    public void Victory()
    {
        isGameWon = true;
    }

    void Update()
    {
        if (isScoreChanged)
        {
            foreach (GUIText guiElem in GetComponentsInChildren<GUIText>())
            {
                if (guiElem.name == "score")
                {
                    guiElem.text = "Score: " + score;
                    break;
                }
            }

            isScoreChanged = false;
        }

        if (isLivesChanged)
        {
            foreach (GUIText guiElem in GetComponentsInChildren<GUIText>())
            {
                if (guiElem.name == "lives")
                {
                    guiElem.text = "Lives: " + lives;
                    break;
                }
            }

            isLivesChanged = false;
        }

        if (isGameWon)
        {
            foreach (GUIText guiElem in GetComponentsInChildren<GUIText>())
            {
                if (guiElem.name == "gameOver")
                {
                    guiElem.text = "Victory achieved!";
                    Time.timeScale = 0;
                    break;
                }
            }
        }

    }

}
