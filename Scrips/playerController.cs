using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerController : MonoBehaviour
{

    public float speed = 10;
    public float jumpHeight = 10;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameManagerScript gameManager; 
    public bool isGrounded;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private float Jump;
    private bool isDead;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        isGrounded = false;
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
        
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 16)
        {
            winTextObject.SetActive(true);
            gameManager.quit();
        }
    }

    void OnJump()
    {
        if (isGrounded)
        {
            Jump = jumpHeight;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, Jump, movementY);

        rb.AddForce(movement*speed);
        Jump = 0;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

        if (other.gameObject.CompareTag("Die") && !isDead)
        {
            other.gameObject.SetActive(false);
            isDead = true;
            gameObject.SetActive(false);
            gameManager.gameOver();

        }
        
    }
}
