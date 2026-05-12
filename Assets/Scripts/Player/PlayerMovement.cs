using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    private Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private Vector2 moveDirection;

    private float _timer;
    private bool _canDash;

    private PlatformScript currentPlatform;

    public float dashDistance = 1.2f;
    public float runSpeed = 5f;
    public float dashCoolDown = 1f;
    public float speedUpgradeAmount = 0.5f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        body = GetComponent<Rigidbody2D>();
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        // ---- INPUT ----
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontal, vertical).normalized;

        // ---- COOLDOWN DASH ----
        _timer += Time.deltaTime;
        _canDash = _timer >= dashCoolDown;

        // ---- DASH ----
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash());
        }

        // ---- TEST ----
        if (Input.GetKeyDown(KeyCode.P))
        {
            Reload();
        }

        EnableCollision();
    }

    void FixedUpdate()
    {
        Vector2 movement = moveDirection * runSpeed;

        if (currentPlatform != null)
        {
            movement += (Vector2)currentPlatform.PlatformVelocity;
        }

        body.linearVelocity = movement;
    }

    private IEnumerator Dash()
    {
        if (!_canDash) yield break;

        _canDash = false;
        _timer = 0f;

        Invincible();

        body.linearVelocity = Vector2.zero;

        body.MovePosition(body.position + moveDirection * dashDistance);

        yield return null;
    }

    public void SetCurrentPlatform(PlatformScript platform)
    {
        currentPlatform = platform;
    }

    private void Reload()
    {
        GameManager.Instance.PlayerDead();
    }

    public void Invincible()
    {
        StartCoroutine(GameManager.Invincibility());
    }

    public void SpeedUpgrade()
    {
        runSpeed += speedUpgradeAmount;
    }

    public void EnableCollision()
    {
        if (GameManager.Instance.godMode)
        {
            body.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            body.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}