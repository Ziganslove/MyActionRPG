using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public static int playerLvl = 1;

    private int playerScore = 0;
    private Text scoreUI;
    private Text levelUI;
    private Player player;
    private int pointsToLvlUp = 200;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        scoreUI = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        levelUI = GameObject.FindGameObjectWithTag("Lvl").GetComponent<Text>();
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.text = playerScore.ToString();
        levelUI.text = playerLvl.ToString();
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            scoreUI = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            playerLvl = 0;
            Destroy(gameObject);
        }
        if (playerScore >= pointsToLvlUp)
        {
            pointsToLvlUp *= 2;
            player.LevelUp();
            playerLvl++;
        }
    }

    public void IncreaseScore(int score)
    {
        playerScore += score;
    }
}
