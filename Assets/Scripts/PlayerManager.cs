using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float maxJumpForce = 10f;
    public float minJumpForce = 3f;
    public float verForce;
    public float horForce;
    private Rigidbody2D _rb;
    public Slider slider;
    private bool _isHolding;
    private bool _isGround;
    public int score;
    public float force;
    public float jumpForceMultiplier = 0.02f;

    public static PlayerManager Instance;

    private void Awake() => Instance = this;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        slider.maxValue = maxJumpForce;
        slider.minValue = minJumpForce;
        slider.value = minJumpForce;
    }

    void Update()
    {
        if (_isGround) GameManager.Instance.CameraPos();
        HandleTouchInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (_rb.linearVelocity.y == 0)
            {
                if (touch.phase == TouchPhase.Began)
                    _isHolding = true;
                else if (touch.phase == TouchPhase.Ended)
                    Jump();
            }
        }

        if (_isHolding)
            UpdateJumpForce();
    }

    private void Jump()
    {
        _rb.AddForce(new Vector2(horForce, verForce) * force, ForceMode2D.Impulse);
        AudioManager.instance.PlaySFX("Jump");
        _isHolding = false;
        slider.value = minJumpForce;
        GameManager.Instance.RandomPos();
        verForce = minJumpForce;
    }

    void UpdateJumpForce()
    {       
        
        verForce += jumpForceMultiplier * Time.deltaTime;
        verForce = Mathf.Clamp(verForce, minJumpForce, maxJumpForce);
        slider.value = verForce;
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
            _isGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            UIManager.Instance.GameOver();
            AudioManager.instance.StopMusic();
            AudioManager.instance.PlaySFX("GameOver");
        }
    }
}
