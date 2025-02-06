using UnityEngine;
using System.Collections;

public class ManaRegenSkill : MonoBehaviour
{
    [Header("Mana Regen Settings")]
    [SerializeField] private float m_baseRegenRate = 3f;
    [SerializeField] private int m_regenAmount = 1;
    [SerializeField] private PlayerManaManager m_manaManager;

    private bool m_isRegenerating;
    private Coroutine m_regenCoroutine;

    public void StartRegeneration()
    {
        if (m_isRegenerating) return;
        
        m_isRegenerating = true;
        m_regenCoroutine = StartCoroutine(RegenManaRoutine());
    }

    public void StopRegeneration()
    {
        if (!m_isRegenerating) return;
        
        m_isRegenerating = false;
        if (m_regenCoroutine != null)
        {
            StopCoroutine(m_regenCoroutine);
            m_regenCoroutine = null;
        }
    }

    private IEnumerator RegenManaRoutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(m_baseRegenRate);

        while (m_isRegenerating)
        {
            m_manaManager.HealMana(m_regenAmount);
            yield return waitTime;
        }
    }

    public void SetRegenRate(float _newRate)
    {
        m_baseRegenRate = _newRate;
        // Restart regeneration to apply new rate
        if (m_isRegenerating)
        {
            StopRegeneration();
            StartRegeneration();
        }
    }

    public void SetRegenAmount(int _newAmount)
    {
        m_regenAmount = _newAmount;
    }

    private void OnDisable()
    {
        StopRegeneration();
    }
} 