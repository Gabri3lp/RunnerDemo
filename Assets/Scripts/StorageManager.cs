using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public struct Score{
    public string name;
    public int value;
}

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
    List<Score> scores = new List<Score>();
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

    public void AddScore(Score score){
        scores.Add(score);
    }

    public Score[] GetSortedScores(){
        scores.Sort((s1, s2) => s2.value.CompareTo(s1.value));
        return scores.ToArray();
    }
    
    private void OnApplicationQuit() {
        SaveGame();
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
            scores = (List<Score>) bf.Deserialize(file);
            file.Close();
        }
    }


}
