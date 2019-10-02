using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class StorageManager : MonoBehaviour
{
    public static StorageManager instance; 
    private void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    List<int> scores = new List<int>();
    int lives = 0, coins = 0;
    public bool isMatchActive = false;
    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
    }

    public void SetLives(int value){
        lives = value;
    }  

    public void SetCoins(int value){
        coins = value;
    }

    public int GetCoins(){
        return coins;
    }
    public int GetLives(){
        return lives;
    }

    public void SetMatchActive(bool set){
        isMatchActive = set;
    }

    public void AddScore(int score){
        scores.Add(score);
        SaveGame();
    }

    public int[] GetSortedScores(){
        scores.Sort();
        return scores.ToArray();
    }

    
    private void OnApplicationQuit() {
    }


    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/scores.save");
        bf.Serialize(file, scores);
        file.Close();
    }


    public void LoadGame()
    { 
        if (File.Exists(Application.persistentDataPath + "/scores.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/scores.save", FileMode.Open);
            scores = (List<int>) bf.Deserialize(file);
            file.Close();
        } else
            Debug.Log("No game saved!");
    }


}
