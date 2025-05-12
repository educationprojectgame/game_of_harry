using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashDistance = 55f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 1f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPos;
    
    private Vector2 moveInput;
    private bool isDashing;
    private float dashCooldownTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var spawnPointName = PlayerPrefs.GetString("SpawnPoint");
        if (!string.IsNullOrEmpty(spawnPointName))
        {
            var spawnPoint = GameObject.Find(spawnPointName);

            if (spawnPoint != null)
            {

                transform.position = spawnPoint.transform.position;
            }
        }

        if (PlayerPrefs.HasKey("CheckPoint"))
        {
            GameObject checkPoint = GameObject.Find(PlayerPrefs.GetString("CheckPoint"));
            if (checkPoint != null)
                transform.position = checkPoint.transform.position;
        }
    }

    private void Update()
    {
        // Получаем ввод
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // Обработка выстрела
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // Активация рывка
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash())
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            // Обычное движение
            rb.linearVelocity = moveInput * speed;

            // Обновляем аниматор
            if (moveInput != Vector2.zero)
            {
                animator.SetFloat("Horizontal", moveInput.x);
                animator.SetFloat("Vertical", moveInput.y);
            }
            animator.SetFloat("Speed", moveInput.sqrMagnitude);
        }
    }

    private bool CanDash()
    {
        return !isDashing &&
            dashCooldown + dashCooldownTimer < Time.time && 
            moveInput != Vector2.zero;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float startTime = Time.time;

        Vector2 dashVelocity = moveInput * (dashDistance / dashDuration);

        // Основной цикл рывка
        while (Time.time < startTime + dashDuration)
        {
            rb.linearVelocity = dashVelocity;
            yield return null;
        }

        // Завершение рывка
        rb.linearVelocity = moveInput * speed; // Возврат к обычной скорости
        isDashing = false;
        dashCooldownTimer = Time.time;
    }

    private void Shoot()
    {
        Instantiate(bullet, shootPos.position, shootPos.rotation);
    }
}