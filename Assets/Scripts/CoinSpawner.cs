using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    public GameObject coinPrefab;
    public float minHeight, maxHeight;
    float spawnRate = 0, deltaTime = 0;
    public int coinCount = 0;
    
    void Start()
    {
        //Spawn rate is calculate to distribuite all the coins throught all the game time.
        //Reduce by 20% the gameTime in the rate calculation to give the player more chance to get all the coins
        //At slow speeds the player may be unable to reach all the coins though.
        spawnRate =  GameManager.instance.gameTime * 0.8f / GameManager.instance.maxCoins;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isGamePaused){
            if(BackgroundController.instance.currentSpeed != 0){ //Only calculate when the background is moving to avoid unnecessary calculations.
                deltaTime += Time.deltaTime;                    //If we reach the rate time we spawn one coin.
                if(deltaTime >= spawnRate){
                    deltaTime = 0;
                    Vector3 position = this.transform.position;
                    position.y = Random.Range(minHeight, maxHeight);    //The coin is spawned on a random height between the min and max height
                    Instantiate(coinPrefab, position, Quaternion.identity);
                    coinCount++;
                    if(coinCount == GameManager.instance.maxCoins)      //If the spawner spawned all the coins then it stops the spawn.
                        Destroy(this.gameObject);
                }
            }
        }
    }
}
