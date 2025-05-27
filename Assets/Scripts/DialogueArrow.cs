using UnityEngine;
using UnityEngine.UI; // Для работы с UI элементами, такими как Image

public class DirectionalPointer : MonoBehaviour
{
    [Header("Цель, на которую указывает стрелка")]
    public Transform target; // Перетащите сюда объект цели из сцены

    [Header("Настройки игрока и камеры")]
    public Transform playerTransform; // Перетащите сюда объект игрока
    public Camera mainCamera;     // Камера, от которой ведется обзор (обычно Camera.main)

    [Header("Настройки UI стрелки")]
    public float hideDistance = 5f; // Если цель ближе этого расстояния, стрелка скрывается

    private RectTransform pointerRectTransform;
    private Image pointerImage; // Чтобы скрывать/показывать стрелку

    void Start()
    {
        pointerRectTransform = GetComponent<RectTransform>();
        pointerImage = GetComponent<Image>();

        if (playerTransform == null)
        {
            // Попытка найти игрока по тегу, если не назначен вручную
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerTransform = playerObject.transform;
            }
            else
            {
                Debug.LogError("DirectionalPointer: Player Transform не назначен и не найден по тегу 'Player'.");
                enabled = false; // Выключаем скрипт, если нет игрока
                return;
            }
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("DirectionalPointer: Main Camera не назначена и не найдена.");
                enabled = false; // Выключаем скрипт, если нет камеры
                return;
            }
        }

        if (target == null)
        {
            Debug.LogWarning("DirectionalPointer: Target не назначен. Стрелка будет неактивна.");
            if (pointerImage != null) pointerImage.enabled = false;
            enabled = false; // Выключаем скрипт, если нет цели
            return;
        }
    }

    void Update()
    {
        if (target == null || playerTransform == null || mainCamera == null || pointerRectTransform == null)
        {
            if (pointerImage != null) pointerImage.enabled = false;
            return; // Выход, если что-то не настроено
        }

        // 1. Проверяем расстояние до цели
        float distanceToTarget = Vector3.Distance(playerTransform.position, target.position);
        if (distanceToTarget < hideDistance)
        {
            if (pointerImage != null) pointerImage.enabled = false; // Скрываем стрелку, если цель близко
            return;
        }
        else
        {
            if (pointerImage != null) pointerImage.enabled = true; // Показываем, если далеко
        }

        // 2. Определяем позицию цели на экране
        Vector3 targetScreenPosition = mainCamera.WorldToScreenPoint(target.position);

        // 3. Проверяем, находится ли цель перед камерой или за ней
        bool isTargetInFront = targetScreenPosition.z > 0;

        if (isTargetInFront)
        {
            // Цель перед камерой
            // Получаем позицию самой стрелки на экране (её pivot point)
            Vector3 pointerScreenPosition = pointerRectTransform.position;

            // 4. Рассчитываем направление от стрелки к цели на 2D экране
            Vector2 directionToTargetOnScreen = new Vector2(targetScreenPosition.x - pointerScreenPosition.x, targetScreenPosition.y - pointerScreenPosition.y);

            // 5. Рассчитываем угол поворота для стрелки
            // Mathf.Atan2 возвращает угол в радианах. Переводим в градусы.
            float angle = Mathf.Atan2(directionToTargetOnScreen.y, directionToTargetOnScreen.x) * Mathf.Rad2Deg;

            // 6. Применяем поворот к RectTransform стрелки.
            // Смещение -90f нужно, если ваш спрайт стрелки по умолчанию направлен ВВЕРХ.
            // Если он направлен ВПРАВО, смещение не нужно (или будет 0f). Подберите это значение.
            pointerRectTransform.rotation = Quaternion.Euler(0f, 0f, angle - 45f);
        }
        else
        {
            // Цель за камерой
            // В этом случае, мы хотим, чтобы стрелка указывала на край экрана в направлении цели.
            // Инвертируем позицию по X и Y (как будто смотрим сзади)
            Vector3 behindTargetScreenPosition = targetScreenPosition * -1; // Упрощенное инвертирование
                                                                            // Чтобы сделать это более точно, нужно было бы проецировать на плоскость экрана
                                                                            // точку, противоположную направлению на цель от игрока.

            // Для упрощения, если цель сзади, можно просто развернуть стрелку примерно в ту сторону
            // или заставить ее указывать на ближайший край экрана.
            // Самый простой вариант — заставить стрелку указывать в "обратную" сторону от ее текущего положения на экране.
            Vector3 pointerScreenPosition = pointerRectTransform.position;
            Vector2 directionToTargetOnScreen = new Vector2(behindTargetScreenPosition.x - pointerScreenPosition.x, behindTargetScreenPosition.y - pointerScreenPosition.y);
            float angle = Mathf.Atan2(directionToTargetOnScreen.y, directionToTargetOnScreen.x) * Mathf.Rad2Deg;
            pointerRectTransform.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

            // Более продвинутый вариант: закрепить стрелку на краю экрана.
            // Это требует дополнительных вычислений для определения точки на границе экрана.
            // Например, можно скрыть стрелку если цель за спиной:
            // if (pointerImage != null) pointerImage.enabled = false;
        }
    }

    // Вы можете вызывать этот метод из других скриптов, чтобы сменить цель
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != null)
        {
            enabled = true; // Включаем скрипт, если была установлена новая цель
            if (pointerImage != null) pointerImage.enabled = true;
        }
        else
        {
            Debug.LogWarning("DirectionalPointer: New target is null. Стрелка будет неактивна.");
            if (pointerImage != null) pointerImage.enabled = false;
            // enabled = false; // Можно выключить, если нет цели
        }
    }
}