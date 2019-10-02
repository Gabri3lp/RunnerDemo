using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;
    public float minSeparation = 2f;
    public float probability = 0.1f;
    float deltaDistance = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isGamePaused){
            if(BackgroundController.instance.currentSpeed != 0){ 
                deltaDistance += BackgroundController.instance.currentSpeed * Time.deltaTime;
                if(deltaDistance >= minSeparation){
                    if(Random.value <= probability){
                        Instantiate(obstacle, this.transform.position, Quaternion.identity);
                        deltaDistance = 0;
                    }
                }
            }
        }
        
    }
}
