using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // ����������� ������'�
    public int currentHealth;  // ������� ������'�
    public TextMeshProUGUI healthText; // UI ����� ��� ����������� HP
    public string deathSceneName = "DeathScene"; // ����� ����� �����
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth; // ������������ ��������� ������'�
        UpdateHealthUI(); // ��������� UI
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // �������� ������'�

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void HealToFull()
    {
        if (isDead) return;

        currentHealth = maxHealth; // ³��������� ������'�
        UpdateHealthUI();
        Debug.Log("Health fully restored!");
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died!");
        SceneManager.LoadScene(deathSceneName); // ����������� ����� �����
    }
}
