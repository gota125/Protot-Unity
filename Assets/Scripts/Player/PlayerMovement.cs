using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    
    private Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;
    private Vector3 moveDirection;
    private float _timer;
    private bool _canDash;



    public float dashDistance = 1.2f;
    public float runSpeed = 5f;
    public float dashCoolDown = 1f;
    public float speedUpgradeAmount = 0.5f;



    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        Instance = this;


    }

    void Update()
    {
        
        //  ---- Mouvement et Direction ----
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontal, vertical).normalized;

        //  ---- Timer Cooldown ----
        _timer += Time.deltaTime;
        if (_timer <= dashCoolDown)
        {
            _canDash = false;
        }
        else
        {
            _canDash = true;
        }

        //  ---- Dash ----

        Dash();
        
        // test de issa pour voir un trucs 22h06
        if (Input.GetKeyDown(KeyCode.P))
        {
            Reload();
        }
        Enablecollision();

    }

    void FixedUpdate()
    {

        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.linearVelocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

    }

    private void Dash()
    {
        if (_canDash)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Invincible();

                body.MovePosition(transform.position + moveDirection * dashDistance);
                _timer = 0f;
            }
        }
    }
    
    private void Reload()
    {
        GameManager.Instance.PlayerDead();
    }

    public void Invincible()
    {
        StartCoroutine (GameManager.Invincibility());
    }

    public void SpeedUpgrade()
    {
        runSpeed += speedUpgradeAmount;
    }

    public void Enablecollision()
    {
        if (GameManager.Instance.godMode == true)
        {
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            rb2d.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    
}