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
    public ObstacleSpawner obstacleSpawner;

    [SerializeField] private AudioSource swim,hit,point;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        FishSwim ();
        
    }
    private void FixedUpdate ()
    {
        FishRotation ();
    }

    private void FishSwim ()
    {
        if(Input.GetMouseButtonDown(0) && GameManager.gameOver == false)                    //oyun sonlanmadýðýnda veya týklandýðýnda
        {
            swim.Play ();
            if(GameManager.gameStarted == false)                                            //oyun baþlamamýþsa
            {
                _rb.gravityScale = 4f;
                _rb.velocity = Vector2.zero;
                _rb.velocity = new Vector2 (_rb.velocity.x, _speed);
                obstacleSpawner.InstantiateObstacle ();
                gameManager.GameHasStarted ();
            }
            else                                                                            //oyun baþladýðýnda
            {
                _rb.velocity = Vector3.zero;
                _rb.velocity = new Vector2 (_rb.velocity.x, _speed);
            }
            
        }
    }

    private void FishRotation ()                                                            //balýk yüzerken aþaðý yukarý hareketi
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
        if(collision.CompareTag("Obstacle"))                                                 //sayý kazandýðýmýz box.
        {
            //Debug.Log ("scored");
            score.Scored();
            point.Play();
        }
        else if(collision.CompareTag ("Column") && GameManager.gameOver == false)           //oyun bir kez bittiðinde alttaki sesi her temasta duymak istmiyoruz.
        {
            //gameover
            gameManager.GameOver();                                                         // oyun bitti.
            FishDieEffect ();                                                               //hit voice.   
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
                FishDieEffect ();
            }
            else
            {
                //gameover (fish)
                GameOver();
                
            } 
            
        }
    }

    void FishDieEffect()
    {
        hit.Play();
    }

    void GameOver ()
    {
        touchGround = true;
        transform.rotation = Quaternion .Euler(0,0,-90);
        sp.sprite = fishDied;
        anim.enabled = false;
    }
}
