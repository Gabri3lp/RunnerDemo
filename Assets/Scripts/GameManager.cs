using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    private void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(this.gameObject);
    }
    public int maxCoins, gameTime, maxLives, lifeCost;
    [HideInInspector]
    public bool isGamePaused = true;
    [HideInInspector]
    public int livesCount = 0;
    [HideInInspector]
    public int coinsCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        coinsCount = 0;
        if(PlayerPrefs.HasKey("Lives")){
            livesCount = PlayerPrefs.GetInt("Lives");
            coinsCount = PlayerPrefs.GetInt("Coins");
            StartGame();
        }else
            livesCount = maxLives;
    }

    public void StartGame(){
        GuiManager.instance.OpenPlayScreen(livesCount , coinsCount);
        StartCoroutine(StartCountdown());
        StorageManager.instance.SetMatchActive(true);
    }

    IEnumerator StartCountdown(){
        for (int timeLeft = 3; timeLeft > 0; timeLeft--) {
            GuiManager.instance.SetCountdownText(timeLeft);
            yield return new WaitForSecondsRealtime(1f);
        }
        GuiManager.instance.HideCountdown();
        SetPause(false);
        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer(){
        for (int timeLeft = gameTime; timeLeft > 0; timeLeft--) {
            GuiManager.instance.SetGameTimerText(timeLeft);
            yield return new WaitForSeconds(1f);
        }
        SetPause(true);
        GuiManager.instance.OpenShop(livesCount, coinsCount, CanBuyLife());
    }

    void SetPause(bool set){
        isGamePaused = set;
        Time.timeScale = set? 0 : 1;
    }

    void GameOver(){
        PlayerPrefs.DeleteAll();
        StorageManager.instance.SetMatchActive(false);
        SetPause(true);
        StorageManager.instance.AddScore(coinsCount);
        int maxScore = Mathf.Max(StorageManager.instance.GetSortedScores());
        GuiManager.instance.OpenGameOverScreen(coinsCount, maxScore);
    }


    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Continue(){
        PlayerPrefs.SetInt("Lives", livesCount);
        PlayerPrefs.SetInt("Coins", coinsCount);
        Restart();
    }

    public void AddCoin(){
        coinsCount++;
        GuiManager.instance.UpdateWorldCoins(coinsCount);
    }

    public void BuyLife(){
        if(CanBuyLife()){
            livesCount++;
            coinsCount -= lifeCost;
        }
    }

    public void AddLife(){
        if(livesCount == maxLives)
            return;
        livesCount++;
    }

    public bool CanBuyLife(){
        return coinsCount >= lifeCost && livesCount < maxLives;
    }

    public void TakeDamage(){
        livesCount--;
        if(livesCount == 0)
            GameOver();
        GuiManager.instance.UpdateWorldLives(livesCount);
    }

}
