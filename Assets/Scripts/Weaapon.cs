using UnityEngine;

public class Weaapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float rotationSpeed;

    void Update()
    {
        PlayerRotation();
    }
    private void PlayerRotation()
    {
       Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
