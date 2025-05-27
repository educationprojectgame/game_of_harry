// В скрипте ProgressManager.cs
using UnityEngine;
using System.Collections.Generic;

public class PManager : MonoBehaviour
{
    public static PManager Instance { get; private set; }

    public Dictionary<string, bool> defeatedBosses = new Dictionary<string, bool>();
    public HashSet<string> collectedItems = new HashSet<string>(); // Используем HashSet для эффективности, если нам просто нужно знать, собран ли предмет

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- Боссы ---
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

    public bool IsBossDefeated(string bossId)
    {
        return defeatedBosses.ContainsKey(bossId) && defeatedBosses[bossId];
    }

    // --- Предметы ---
    public void MarkItemAsCollected(string itemId)
    {
        if (collectedItems.Add(itemId)) // .Add вернет true, если элемент был успешно добавлен (т.е. его еще не было)
        {
            Debug.Log($"Item {itemId} marked as collected.");
        }
    }

    public bool IsItemCollected(string itemId)
    {
        return collectedItems.Contains(itemId);
    }

    // TODO: Методы сохранения/загрузки
}