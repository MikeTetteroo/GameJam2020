using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float defaultHealth = 100;
    public float defaultStamina = 100;
    public float defaultImmunityAfterDamageFrames = 120;
    public float staminaRecoveryRate = 1;

    private float currentImmunityAfterDamageFrames;
    public float currentHealth;
    public float currentStamina;

    void Start()
    {
        currentHealth = defaultHealth;
        currentStamina = defaultStamina;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentImmunityAfterDamageFrames > 0)
        {
            --currentImmunityAfterDamageFrames;
        }

        if(Input.GetKeyDown("k"))
        {
            Debug.Log("Damage detected.");
            TakeDamage(5);
        }

        if(Input.GetKeyDown("l"))
        {
            Debug.Log("Stamina Reduction.");
            RemoveStamina(5);
        }

        if (currentStamina < defaultStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            if(currentStamina > defaultStamina)
            {
                currentStamina = defaultStamina;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentImmunityAfterDamageFrames = defaultImmunityAfterDamageFrames;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        // Play whatever death animation needs playing

        // Wait for a couple of seconds
        yield return new WaitForSeconds(3);

        // Do whatever after a couple of seconds after the death
        Destroy(gameObject);
    }

    public void GainHealth(float healing)
    {
        currentStamina += healing;
    }

    public void RemoveStamina(float staminaCost)
    {
        currentStamina -= staminaCost;
    }

    public bool CheckStamina(float staminaCost)
    {
        return currentStamina >= staminaCost;
    }
}
