using UnityEngine;
using System.Collections;

public class HealthRegenSkill : MonoBehaviour
{
    [SerializeField] private float m_regenTickRate = 1f;
    [SerializeField] private float m_healthPerTick = 2f;
    
    private PlayerHealthManager m_healthManager;
    private bool m_isRegenerating;
    private Coroutine m_regenCoroutine;

    private void Awake()
    {
        if (!TryGetComponent(out m_healthManager))
        {
            Debug.LogError("HealthRegenSkill requires PlayerHealthManager!");
            enabled = false;
            return;
        }
    }

    public void StartRegeneration()
    {
        if (m_isRegenerating) return;
        
        m_isRegenerating = true;
        m_regenCoroutine = StartCoroutine(RegenerateHealth());
    }

    public void StopRegeneration()
    {
        if (!m_isRegenerating) return;
        
        if (m_regenCoroutine != null)
        {
            StopCoroutine(m_regenCoroutine);
        }
        m_isRegenerating = false;
    }

    private IEnumerator RegenerateHealth()
    {
        while (m_isRegenerating)
        {
            m_healthManager.Heal(m_healthPerTick);
            yield return new WaitForSeconds(m_regenTickRate);
        }
    }
} 