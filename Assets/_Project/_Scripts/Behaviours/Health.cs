using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 300;
    public int currentHealth;
    public bool canTakeDamage = true;
    public float invulnerabilityTime = 0.5f; // Thời gian bất tử sau khi nhận damage

    [Header("Death Settings")]
    public bool destroyOnDeath = true;
    public float destroyDelay = 0f;

    [Header("Events")]
    public UnityEvent<int> OnHealthChanged; // Event khi máu thay đổi
    public UnityEvent OnDeath; // Event khi chết
    public UnityEvent<int> OnDamageTaken; // Event khi nhận damage
    public UnityEvent<int> OnHealthRestored; // Event khi hồi máu

    private bool isDead = false;
    private bool isInvulnerable = false;

    void Start()
    {
        // Khởi tạo máu ban đầu
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    /// <summary>
    /// Nhận sát thương
    /// </summary>
    /// <param name="damage">Lượng sát thương</param>
    public void TakeDamage(int damage)
    {
        // Kiểm tra xem có thể nhận damage không
        if (!canTakeDamage || isDead || isInvulnerable || damage <= 0)
            return;

        // Giảm máu
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Trigger events
        OnDamageTaken?.Invoke(damage);
        OnHealthChanged?.Invoke(currentHealth);

        // Kiểm tra xem đã chết chưa
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Bắt đầu thời gian bất tử
            StartInvulnerability();
        }
    }

    /// <summary>
    /// Hồi máu
    /// </summary>
    /// <param name="healAmount">Lượng máu hồi</param>
    public void Heal(int healAmount)
    {
        if (isDead || healAmount <= 0)
            return;

        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthRestored?.Invoke(healAmount);
        OnHealthChanged?.Invoke(currentHealth);
    }

    /// <summary>
    /// Hồi máu đầy
    /// </summary>
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
        OnHealthChanged?.Invoke(currentHealth);
    }

    /// <summary>
    /// Xử lý khi chết
    /// </summary>
    private void Die()
    {
        if (isDead) return;

        isDead = true;
        canTakeDamage = false;

        // Trigger death event
        OnDeath?.Invoke();

        // Destroy object nếu cần
        if (destroyOnDeath)
        {
            if (destroyDelay > 0)
            {
                Destroy(gameObject, destroyDelay);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Bắt đầu thời gian bất tử
    /// </summary>
    private void StartInvulnerability()
    {
        if (invulnerabilityTime > 0)
        {
            isInvulnerable = true;
            Invoke(nameof(EndInvulnerability), invulnerabilityTime);
        }
    }

    /// <summary>
    /// Kết thúc thời gian bất tử
    /// </summary>
    private void EndInvulnerability()
    {
        isInvulnerable = false;
    }

    /// <summary>
    /// Kiểm tra xem có đang bất tử không
    /// </summary>
    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }

    /// <summary>
    /// Kiểm tra xem đã chết chưa
    /// </summary>
    public bool IsDead()
    {
        return isDead;
    }

    /// <summary>
    /// Lấy phần trăm máu hiện tại
    /// </summary>
    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }

    /// <summary>
    /// Đặt máu tối đa mới
    /// </summary>
    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
    }
}
