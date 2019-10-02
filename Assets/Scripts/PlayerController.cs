using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Singleton code
    public static PlayerController instance;
    private void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public float speed;
    public int accelerationFrames;
    public int jumpHeight;
    SpriteRenderer sr;
    Animator ator;
    Rigidbody2D rb;
    bool isGrounded = true;
    bool isInvulnerable = false;
    Vector2 jumpVelocity;
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        ator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        float height = this.GetComponent<SpriteRenderer>().size.y; //Heigh of the sprite
        float yMax = jumpHeight * height;   //Max jump heigh
        float gravity = Physics2D.gravity.magnitude;
        jumpVelocity =  Mathf.Sqrt(2 * gravity * yMax) * Vector2.up; //Initial velocity formula for a 90° proyectile
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isGamePaused)
        { //Jump only if it's grounded and the input is pressed
            if(isGrounded){
                if(Input.GetKeyDown(KeyCode.Space))
                    rb.velocity = jumpVelocity;
                ator.SetFloat("ySpeed", 0);
            }else{
                if(rb.velocity.y > 0)
                    ator.SetFloat("ySpeed", 1);
                else
                    ator.SetFloat("ySpeed", -1);
            }
        }
    }


    private void OnCollisionStay2D(Collision2D other) {
        //Set isGrounded to true while touching the ground
        if(other.gameObject.CompareTag("Ground")){
            isGrounded = true;                          
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        //Set isGrounded to false while touching the ground
        if(other.gameObject.CompareTag("Ground")){      
            isGrounded = false;                         
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //If the player touches a coin he destroys it and add one to the inventory.
        if(other.CompareTag("Coin")){
            Destroy(other.gameObject);
            GameManager.instance.AddCoin();
        }else if(!isInvulnerable && other.CompareTag("Spike")){
            isInvulnerable = true;
            StartCoroutine(TakeDamageAnimation());
            GameManager.instance.TakeDamage();
        }
    }

    IEnumerator TakeDamageAnimation(){
        float deltaTime = 0;
        while(deltaTime < 3){
            sr.enabled = false;
            yield return new WaitForSeconds(0.15f);
            deltaTime += 0.15f;
            sr.enabled = true;
            yield return new WaitForSeconds(0.15f);
            deltaTime += 0.15f;
        }
        isInvulnerable = false;
    }
    
    //Handle horizontal velocity
    public void SetHorizontalSpeed(float xSpeed){
        xSpeed = xSpeed == 0? 0 : 1;
        ator.SetFloat("xSpeed", xSpeed);
    }
}
