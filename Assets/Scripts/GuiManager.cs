using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public static GuiManager instance; 
    private void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(this.gameObject);
    }

    public Text countdownText, timerText;
    public GameObject playScreen, startScreen, shopScreen, gameOverScreen;
    public Button buyLifeBtn;
    public Text lifeCountWorld, coinCountWorldText, gameOverScoreText, gameOverHighScoreText;
    GameObject currentScreen;
    // Start is called before the first frame update
    void Start()
    {
        currentScreen = startScreen;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGameOverScreen(int score, int highScore){
        gameOverScoreText.text = "Score:" + score.ToString();
        gameOverHighScoreText.text = "High Score: " + highScore.ToString();
        OpenScreen(gameOverScreen);
    }

    public void SetCountdownText(int time){
        countdownText.text = time.ToString();
    }

    public void HideCountdown(){
        countdownText.enabled = false;
    }

    public void SetGameTimerText(int time){
        timerText.text = time.ToString();
    }

    void OpenScreen(GameObject screen){
        currentScreen.SetActive(false);
        currentScreen = screen;
        currentScreen.SetActive(true);
    }

    public void OpenPlayScreen(int lives, int coins){
        OpenScreen(playScreen);
        UpdateWorldLives(lives);
        UpdateWorldCoins(coins);
    }

    public void OpenShop(int lives, int  coins, bool canBuyMore){
        OpenScreen(shopScreen);
    }
    
    public void UpdateWorldCoins(int coins){
        coinCountWorldText.text = coins.ToString();
    }

    

    public void UpdateWorldLives(int lives){
        lifeCountWorld.text = lives.ToString();
    }   
}
