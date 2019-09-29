using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    private void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(this.gameObject);
    }
    public bool isGamePaused = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGame(){
        GuiManager.instance.OpenPlayScreen();
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown(){
        for (int timeLeft = 3; timeLeft > 0; timeLeft--) {
            GuiManager.instance.SetCountdownText(timeLeft);
            yield return new WaitForSecondsRealtime(1f);
        }
        GameManager.instance.enabled = true;
        GuiManager.instance.HideCountdown();
        Time.timeScale = 1;
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
