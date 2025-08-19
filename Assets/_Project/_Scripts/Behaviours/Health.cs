using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 300;
    public int currentHealth;
    public bool canTakeDamage = true;
    public float invulnerabilityTime = 0.5f; // Thời gian bất tử sau khi nhận damage
    private bool isDead = false;
   
    void Start()
    {
        // Khởi tạo máu ban đầu
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Giảm máu
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        MMEventManager.TriggerEvent(new LoseAHeartEvent());
    }

   
    public void Heal(int healAmount)
    {
        
        if (isDead || healAmount <= 0)
            return;

        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    }

    public void HealToFull()
    {
        Heal(maxHealth - currentHealth);
    }

    /// <summary>
    /// Đặt lại máu về max
    /// </summary>
    public void ResetHealth()
    {
        isDead = false;
        currentHealth = maxHealth;
        canTakeDamage = true;
    }
    

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }


}
