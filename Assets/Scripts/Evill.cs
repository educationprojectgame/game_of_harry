using UnityEngine;

public class Evil : MonoBehaviour
{
    public Transform Player;
    public Transform Target1;
    public float Speed = 3f;
    private bool isFollowingPlayer = true;

    void Update()
    {
        if (isFollowingPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime); 
            if (Vector2.Distance(transform.position, Player.position) < 0.5f)
            {
                isFollowingPlayer = false;
                Invoke("SwitchTarget", 10f); 
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Target1.position, Speed * Time.deltaTime); 

            if (Vector2.Distance(transform.position, Target1.position) < 0.5f)
            {
                isFollowingPlayer = true;
            }
        }
    }

    void SwitchTarget()
    {
        // меняем цель на Target1
        isFollowingPlayer = false;
    }
}
