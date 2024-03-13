using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] int points;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("collect");
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(points);
            }
            Destroy(gameObject);
        }
    }
}