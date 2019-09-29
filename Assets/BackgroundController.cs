using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    float speed;
    float currentSpeed;
    int accelerationFrames;
    public Transform bg1, bg2;
    Vector3 startPos;
    float maxDeltaPos;
    float deltaPos;
    // Start is called before the first frame update
    void Start()
    {
        accelerationFrames = PlayerController.instance.accelerationFrames;//Though the background is actually the one moving this is a player attribute.
        if(accelerationFrames == 0)                             //Set to one to avoid math errors.
            accelerationFrames = 1;
        startPos = this.transform.position;
        maxDeltaPos = bg2.position.x - bg1.position.x;
        speed = PlayerController.instance.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isGamePaused){
            if(Input.GetKey(KeyCode.R) && currentSpeed < speed)     //Increase current speed while the input is pressed
                currentSpeed += speed / accelerationFrames;     
            else if(currentSpeed > 0)                               //Decrease current speed while the input is not pressed
                currentSpeed -= speed / accelerationFrames;         
            currentSpeed = Mathf.Clamp(currentSpeed, 0, speed);     //Limit current speed between 0 and max speed 
            Move(currentSpeed * Time.deltaTime);                    //Move the background
        }
    }

    void Move(float speed){    
        PlayerController.instance.SetHorizontalSpeed(speed);
        deltaPos = Mathf.Repeat(deltaPos + speed, maxDeltaPos); //Repeat the offset of the background between 0 and maxDeltaPos
        this.transform.position = startPos + deltaPos * Vector3.left;          
    }

}
