using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HealthPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float HealthPlay = 100f;
    public Image Bar;

    public void TakeDamage(int damage)
    {
        HealthPlay -= damage;
        Bar.fillAmount = HealthPlay / 100;
        if (HealthPlay <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        string checkPointScene = PlayerPrefs.GetString("CheckPointScene");

        Destroy(gameObject);

        if (!string.IsNullOrEmpty(checkPointScene))
        {
            SceneManager.LoadScene(checkPointScene);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
