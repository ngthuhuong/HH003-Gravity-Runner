using System;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour,MMEventListener<GetAHeart>
{
    [Header("Health Settings")]
    public int maxHealth = 300;
    private int currentHealth;
    public bool canTakeDamage = true;
    private float invulnerabilityTime = 5f; // Thời gian bất tử sau khi nhận damage
    private bool isDead = false;
   
    void Start()
    {
        // Khởi tạo máu ban đầu
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        this.MMEventStartListening<GetAHeart>();
    }
    private void OnDisable()
    {
        this.MMEventStopListening<GetAHeart>();
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            MMEventManager.TriggerEvent(new LoseAHeartEvent());
        }
        Debug.Log("invulnerable: " + !canTakeDamage);
        return;
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

    public void invulnerable()
    {
        canTakeDamage = false;
        StartCoroutine(WaitUntilEndInvulnerability());
    }

    public IEnumerator WaitUntilEndInvulnerability()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        canTakeDamage = true;
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.UnableShield();
        }
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }


    public void OnMMEvent(GetAHeart eventType)
    {
        Heal(100);
    }
}
