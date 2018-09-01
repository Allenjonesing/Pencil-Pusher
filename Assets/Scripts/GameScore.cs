using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScore : MonoBehaviour
{
    Text scoreTextUI;
    //Text highscoreTextUI;

    public GameObject Enemy;

    // public int oldHighscore;
    int Enemyscore;



	// Use this for initialization
	void Start ()
    {

        scoreTextUI = GetComponent<Text>();
        //highscoreTextUI = GetComponent<Text>();
    }

    /* public void StoreHighscore(int newHighscore)
     {
         //highscoreTextUI = GetComponent<Text>();

         oldHighscore = PlayerPrefs.GetInt("highscore", 0);
         if (newHighscore > oldHighscore)
         {
             PlayerPrefs.SetInt("highscore", newHighscore);
             PlayerPrefs.Save();
             string scoreStr = string.Format("{0:000000}", PlayerPrefs.GetInt("highscore"));
             highscoreTextUI.text = scoreStr;
         }
         else
         {
             string scoreStr = string.Format("{0:000000}", oldHighscore);
             highscoreTextUI.text = scoreStr;
         }
     }*/
    void Update()
    {
        UpdateScoreTextUI();
    }


    void UpdateScoreTextUI()
    {
        //scoreTextUI = GetComponent<Text>();
        Enemyscore = Enemy.GetComponent<EnemyController>().hp;
        string scoreStr = string.Format("{0:000}", Enemyscore);
        scoreTextUI.text = scoreStr;

    }

}
