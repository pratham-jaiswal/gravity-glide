using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Transform cameraPivot;
    [SerializeField] bool startScreen;
    [SerializeField] GameObject statusScreen;
    [SerializeField] Text statusText;
    Rigidbody2D rb;
    Animator animator;
    float gravityScale = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if(!startScreen)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            Invoke("AutoSwitch", 2f);
        }
    }

    void Update()
    {
        if (!startScreen)
        {
            #if UNITY_IPHONE || UNITY_ANDROID
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    animator.SetTrigger("switch");
                    SwitchGravity();
                }
            #elif UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetTrigger("switch");
                    SwitchGravity();
                }
            #endif
        }
    }

    void FixedUpdate() {
        if(!startScreen)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);

            if (cameraPivot != null)
            {
                Vector3 newPos = cameraPivot.position;
                newPos.x = transform.position.x + 7;
                cameraPivot.position = newPos;
            }
        }
    }

    void AutoSwitch()
    {
        animator.SetTrigger("switch");
        SwitchGravity();
        Invoke("AutoSwitch", 3f);
    }

    void SwitchGravity()
    {
        gravityScale *= -1;
        animator.SetInteger("gravity", (int)gravityScale);
        rb.gravityScale = gravityScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!startScreen)
        {
            if (collision.gameObject.CompareTag("Traps"))
            {
                speed = 0f;
                Destroy(gameObject);
                statusText.text = "Game Over!";
                statusScreen.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!startScreen)
        {
            if (collision.CompareTag("Finish"))
            {
                speed = 0f;
                statusText.text = "You Win!";
                statusScreen.SetActive(true);
            }
        }
    }
}