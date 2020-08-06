using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    private Player player;
    public bool isAllowedToTrigger = true;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAllowedToTrigger && collision.gameObject.CompareTag("Ball"))
        {
            player.RemoveBall(collision.gameObject.GetComponent<Ball>());
        }
    }
}
