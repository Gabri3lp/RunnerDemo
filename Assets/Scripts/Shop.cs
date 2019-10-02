using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text livesCount, buyLifeBtnText, coinsCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void OnEnable() {
        UpdateInfo();
    }

    void UpdateInfo(){
        livesCount.text = GameManager.instance.livesCount.ToString();
        coinsCount.text = GameManager.instance.coinsCount.ToString();
        if(GameManager.instance.CanBuyLife())
            buyLifeBtnText.text = $"Buy life ({GameManager.instance.lifeCost})";
        else
            buyLifeBtnText.text = "-";
    }

    public void BuyLife(){
        GameManager.instance.BuyLife();
        UpdateInfo();
    }

    void Continue(){
        GameManager.instance.Continue();
    }
}
