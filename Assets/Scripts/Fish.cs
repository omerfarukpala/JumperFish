using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] 
    private float _speed;
    int angle;
    int maxAngle = 20;
    int minAngle = -60;
    public Score score;
    bool touchGround;
    public GameManager gameManager;
    public Sprite fishDied;
    SpriteRenderer sp;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FishSSwim ();
        
    }
    private void FixedUpdate ()
    {
        FishRotation ();
    }

    private void FishSSwim ()
    {
        if(Input.GetMouseButtonDown(0) && GameManager.gameOver == false)
        {
            _rb.velocity = Vector3.zero;
            _rb.velocity = new Vector2 (_rb.velocity.x, _speed);
        }
    }

    private void FishRotation ()
    {
        if(_rb.velocity.y > 0)
        {
            if(angle <= maxAngle)
            {
                angle = angle + 4;
            }

        }
        else if(_rb.velocity.y < -1.2)
        {
            if(angle > minAngle)
            {
                angle = angle - 2;
            }

        }
        if(touchGround == false)
        {
            transform.rotation = Quaternion.Euler (0, 0, angle);
        }

    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            //Debug.Log ("scored");
            score.Scored();
        }
        else if(collision.CompareTag ("Column"))
        {
            //gameover
            gameManager.GameOver();
        }
        
    }
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(GameManager.gameOver == false)
            {
                //gameover
                gameManager.GameOver ();
            }
            else
            {
                //gameover (fish)
                GameOver();
            } 
            
        }
    }

    void GameOver ()
    {
        touchGround = true;
        transform.rotation = Quaternion .Euler(0,0,-90);
        sp.sprite = fishDied;
        anim.enabled = false;
    }
}
