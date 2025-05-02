using UnityEngine;
using System.Collections.Generic;

public class PLayer : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;




    public Vector2 moveVelocity;
    public Animator animator;
    public Rigidbody2D rb;






    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        var spawnPointName = PlayerPrefs.GetString("SpawnPoint");
        if (!string.IsNullOrEmpty(spawnPointName))
        {
            var spawnPoint = GameObject.Find(spawnPointName);
            Debug.Log($"Сохраняем позицию: {spawnPointName}"); // Логируем

            if (spawnPoint != null)
            {

                transform.position = spawnPoint.transform.position;
            }
            PlayerPrefs.DeleteKey("SpawnPoint");
        }
    }


    private void FixedUpdate()
    {
        Move();
        animator.SetFloat("Horizontal", moveVelocity.x);
        animator.SetFloat("Vertical", moveVelocity.y);
        animator.SetFloat("Speed", moveVelocity.sqrMagnitude);


    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();

        }
    }
    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }


    void Shoot()
    {
        Instantiate(bullet, shootPos.position, shootPos.rotation);
    }



}
