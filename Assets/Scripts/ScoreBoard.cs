using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public GameObject scorePrefab;
    public Transform content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable() {
        foreach (Transform child in content.transform)
            Destroy(child.gameObject);
    }

    private void OnEnable() {
        Score[] scores = StorageManager.instance.GetSortedScores();
        for (int i = 0; i < scores.Length; i++)
            AddScore(i + 1, scores[i]);
    }

    public void AddScore(int index, Score score){ 
        GameObject item = Instantiate(scorePrefab, content);
        item.transform.GetChild(0).GetComponent<Text>().text = index.ToString();
        item.transform.GetChild(1).GetComponent<Text>().text = score.name;
        item.transform.GetChild(2).GetComponent<Text>().text = score.value.ToString();
    }
}
