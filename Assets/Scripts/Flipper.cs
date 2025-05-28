using UnityEngine;

public class Flipper : MonoBehaviour
{
    public GameObject player;
    public bool startFlipped = false;
    private bool facingRight = true;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player object is not assigned. Disabling script.");
            enabled = false;
            return;
        }
        if(startFlipped)
        {
            Flip();
        }

    }

    void Update()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        if ((directionToPlayer.x < 0 && facingRight) || (directionToPlayer.x > 0 && !facingRight))
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}