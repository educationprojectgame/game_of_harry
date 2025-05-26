using UnityEngine;

public class CrystalTake : MonoBehaviour
{
    private bool isUpgraded = false;
    private GameObject player;

    public GameObject crystall;

    void Start()
    {
        if (PlayerPrefs.GetInt("CrystallIsTaken") == 1)
            crystall.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerGame"))
        {
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerGame"))
        {
            player = null;
        }
    }

    void Update()
    {
        if (player != null && Input.GetKeyDown(KeyCode.E))
        {
            crystall.SetActive(false);
            PlayerPrefs.SetInt("CrystallIsTaken", 1);
        }
    }


}