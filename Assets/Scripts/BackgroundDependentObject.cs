using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to move the game object at the same speed of the background but without looping with it.
public class BackgroundDependentObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isGamePaused)
            this.transform.Translate(Vector3.left * BackgroundController.instance.currentSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible() {
        Destroy(this.gameObject);
    }
}
