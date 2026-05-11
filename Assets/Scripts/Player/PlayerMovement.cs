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

    public float dashDistance = 1.2f;
    public float runSpeed = 5f;
    public float dashCoolDown = 1f;
    public float speedUpgradeAmount = 0.5f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        Instance = this;

        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        
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
        body.linearVelocity = moveDirection * runSpeed;
    }

    private IEnumerator Dash()
    {
        if (!_canDash) yield break;

        _canDash = false;
        _timer = 0f;

        Invincible();

        // ✔️ stop mouvement actuel
        body.linearVelocity = Vector2.zero;

        // ✔️ dash instantané propre
        body.MovePosition(body.position + moveDirection * dashDistance);

        yield return null;
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