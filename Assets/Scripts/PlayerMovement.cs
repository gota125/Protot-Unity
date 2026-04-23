using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;
    private Vector3 moveDirection;
    private float _timer;
    private bool _canDash;



    public float dashDistance = 1.2f;
    public float runSpeed = 20.0f;
    public float dashCoolDown = 1f;



    void Awake()
    {
        body = GetComponent<Rigidbody2D>();

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
                body.MovePosition(transform.position + moveDirection * dashDistance);
                _timer = 0f;
            }
        }
    }
    
    private void Reload()
    {
        GameManager.Instance.PlayerDead();
    }
}