using System;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public GameObject bridge;
    [SerializeField] private Color inactiveColor = Color.darkRed;
    [SerializeField] private Color activeColor = Color.lawnGreen;
    private bool isPressed = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = inactiveColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
          OpenBridge();
          spriteRenderer.color = activeColor;
        }
    }

    public void OpenBridge()
    {
        bridge.SetActive(true);
    }
    public void CloseBridge()
    {
        bridge.SetActive(false);
    }
}
