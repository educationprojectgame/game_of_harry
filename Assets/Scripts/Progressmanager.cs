using UnityEngine;
using System.Collections.Generic; // Для использования словаря

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }

    // Словарь для хранения статуса боссов (ID босса, побежден ли)
    public Dictionary<string, bool> defeatedBosses = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожать при загрузке новой сцены
        }
        else
        {
            Destroy(gameObject); // Если экземпляр уже существует, уничтожить этот
        }
    }

    // Метод для отметки босса как побежденного
    public void MarkBossAsDefeated(string bossId)
    {
        if (!defeatedBosses.ContainsKey(bossId))
        {
            defeatedBosses.Add(bossId, true);
        }
        else
        {
            defeatedBosses[bossId] = true;
        }
        Debug.Log($"Boss {bossId} marked as defeated.");
    }

    // Метод для проверки, побежден ли босс
    public bool IsBossDefeated(string bossId)
    {
        return defeatedBosses.ContainsKey(bossId) && defeatedBosses[bossId];
    }

    // TODO: Позже здесь можно добавить методы для сохранения и загрузки этих данных в файл
    // public void SaveProgress() { /* ... */ }
    // public void LoadProgress() { /* ... */ }
}