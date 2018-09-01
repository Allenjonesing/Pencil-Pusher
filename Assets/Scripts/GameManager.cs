using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject battleGroup;

    public GameObject playButton;
    public GameObject playerHP;
    public GameObject enemyHP;
    public GameObject player;
    public GameObject mockPlayer;
    public GameObject enemy;
    public GameObject defeatlogo;
    public GameObject victoryLogo;
    public GameObject scoreUIText;
    public GameObject highscoreUIText;
    public GameObject timeCounter;
    public GameObject startButton;
    public GameObject charButton;
    public GameObject tutorialText;


    public int charID;

    public int timer;
    public int round;
    public int stage;
    public int timerMax;
    public int roundMax;
    public int stageMax;

    public enum GameManagerState
    {
        Opening,
        Mainmenu,
        Charselect,
        Battle,
        Victory,
        Defeat,
        Timeup


    }
    GameManagerState GMState;


    IEnumerator ScaleTime(float start, float end, float time)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < time)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / time);
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = end;
    }

    // Use this for initialization
    void Start()
    {
        //Screen.autorotateToPortrait = false;
        //Screen.autorotateToPortraitUpsideDown = false;
        charID = 0;
        GMState = GameManagerState.Opening;
        UpdateGameManagerState();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Quit();
        if (timeCounter.GetComponent<TimeCounter>().timeUp)
        {
            GMState = GameManagerState.Timeup;
            UpdateGameManagerState();
        }
    }
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
               // mainMenuGroup.SetActive(true);
                //charSelectGroup.SetActive(false);
                battleGroup.SetActive(false);
                // defeatlogo.SetActive(false);
                // victoryLogo.SetActive(false);
                //player.GetComponent<PlayerControl>().canShoot = false;
                startButton.SetActive(true);
                charButton.SetActive(false);
                timeCounter.SetActive(false);
                playerHP.SetActive(false);
                enemyHP.SetActive(false);
                // tutorialText.SetActive(true);
                //player.GetComponent<PlayerControl>().Init();
                //player.SetActive(false);
                // mockPlayer.SetActive(false);
                // enemy.SetActive(false);
                break;
            case GameManagerState.Battle:
                StartCoroutine(ScaleTime(1.0f, 0.0f, 3.0f));
                // mainMenuGroup.SetActive(false);
                // charSelectGroup.SetActive(false);
                battleGroup.SetActive(true);

                // player.GetComponent<PlayerControl>().canShoot = true;
                //scoreUIText.GetComponent<GameScore>().Score = 0;
                //tutorialText.SetActive(false);
                //title.SetActive(false);
                //playButton.SetActive(false);
                timeCounter.SetActive(true);
                playerHP.SetActive(true);
                enemyHP.SetActive(true);
                charButton.SetActive(false);
               // player.SetActive(true);
               // mockPlayer.SetActive(true);

               // enemy.SetActive(true);

                //enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                timeCounter.GetComponent<TimeCounter>().StartTimeCounter();

                break;

            case GameManagerState.Charselect:
               // mainMenuGroup.SetActive(false);
               // charSelectGroup.SetActive(true);
                battleGroup.SetActive(false);

                startButton.SetActive(false);
                charButton.SetActive(true);

                break;

            case GameManagerState.Defeat:
                /*player.GetComponent<PlayerControl>().canShoot = false;
                //tutorialText.SetActive(true);
                player.GetComponent<TouchControl>().firstTouch = true;
                player.GetComponent<PlayerControl>().firstTouch = true;

                timeCounter.GetComponent<TimeCounter>().StopTimeCounter();
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                gameOver.SetActive(true);*/

                // highscoreUIText.GetComponent<GameScore>().StoreHighscore(scoreUIText.GetComponent<GameScore>().Score);
                //StoreHighscore();
                GMState = GameManagerState.Opening;

                break;
            case GameManagerState.Timeup:
                break;
        }
    }

    public void StartButton()
    {
        GMState = GameManagerState.Charselect;
        UpdateGameManagerState();
    }

    public void CharButton(int charID)
    {
        this.charID = charID;
        GMState = GameManagerState.Battle;
        UpdateGameManagerState();

    }




    public void Quit()
    {
        Application.Quit();
    }
}