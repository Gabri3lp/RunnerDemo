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
    public GameObject playScreen, startScreen;
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

    public void OpenPlayScreen(){
        OpenScreen(playScreen);
    }
}
