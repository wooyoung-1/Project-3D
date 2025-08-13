using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    [Header("Player Stat")]
    public float maxHP = 100f;
    public float currentHP;

    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaRegen = 10f;

    [Header("UI")]
    public Image hpBar;
    public Image staminaBar;

    private bool isRunningCooldown = false;
    private float cooldownTime = 0f;
    private bool _Running = false;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();

        currentHP = maxHP;
        currentStamina = maxStamina;

        UpdateHPUI();
        UpdateStaminaUI();
    }
    
    private void Update()
    {
        if (_Running && !GameManager.isRunning)
        {
            isRunningCooldown = true;
            cooldownTime = 0f;
        }

        if (!isRunningCooldown && !GameManager.isRunning)
        {
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegen * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
                UpdateStaminaUI();
            }
        }
        else
        {
            if (isRunningCooldown)
            {
                cooldownTime += Time.deltaTime;

                if (cooldownTime >= 2f)
                {
                    isRunningCooldown = false;
                    cooldownTime = 0f;
                    Debug.Log("달리기 쿨초");
                }
            }
        }

        _Running = GameManager.isRunning;
    }

    public void TakeDamage(float f)
    {
        currentHP -= f;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void UseStamina(float f)
    {
        if (currentStamina >= f)
        {
            currentStamina -= f;
            UpdateStaminaUI();
        }
        else
        {
            Debug.Log("스태미너 다씀");
            GameManager.isRunning = false;
        }
    }

    private void UpdateHPUI()
    {
        hpBar.fillAmount = currentHP / maxHP;
    }

    private void UpdateStaminaUI()
    {
        staminaBar.fillAmount = currentStamina / maxStamina;
    }

    private void Die()
    {
        Debug.Log("YOU DIE");
    }
}
