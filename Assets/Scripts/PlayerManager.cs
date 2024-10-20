using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float maxJumpForce = 10f; 
    public float minJumpForce = 3f;
    public float jumpForce;  
    private Rigidbody2D _rb;
    public Slider slider;
    private bool _isHolding;
    private bool _isGround;
    private bool _isFilling = true;
    public int score;
    
    
    public float jumpForceMultiplier = 0.02f;

    public static PlayerManager Instance;

    private void Awake() => Instance = this;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        slider.maxValue = maxJumpForce; 
    }

    void Update()
    {
        if (_isGround)
        {
            GameManager.Instance.CameraPos(); 
        }

        TouchScreen();
    }

    void TouchScreen()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (_rb.velocity.y == 0) 
            {
                if (touch.phase == TouchPhase.Began)
                    _isHolding = true;
                
                else if (touch.phase == TouchPhase.Ended)
                {
                    _isHolding = false; 
                    Jump();
                }
                    
                
            }
        }

        if (_isHolding)
            Slider();
       
        
    }
    
    private void Jump()
    {
        
        _rb.velocity = new Vector2(jumpForce, jumpForce);
        slider.value = 0;
        GameManager.Instance.RandomPos();
        jumpForce = 0; 
        
    }


    void Slider()
    {
        slider.value = jumpForce;
        if (_isFilling)
        {
            
            jumpForce += jumpForceMultiplier;  
            jumpForce = Mathf.Clamp(jumpForce, minJumpForce, maxJumpForce);
            if (slider.value >= maxJumpForce)
            {
                _isFilling = false;
            }
        }
        else
        {
            jumpForce -= jumpForceMultiplier;
            jumpForce = Mathf.Clamp(jumpForce, minJumpForce, maxJumpForce);
            if (slider.value <= minJumpForce)
            {
                slider.value = slider.minValue;
                _isFilling = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isGround = true;
            score++;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isGround = false;  
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            SceneManager.LoadScene(1);  
        }
    }
}
