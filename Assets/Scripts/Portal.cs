using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal toPortal;
    [SerializeField] private GameObject tpEffect;

    public static bool tpActive;
    void Start()
    {
        tpActive = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb && tpActive != null)
        {
            tpActive = false;
            float magnitude = rb.angularVelocity;
        }
        else tpActive = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
