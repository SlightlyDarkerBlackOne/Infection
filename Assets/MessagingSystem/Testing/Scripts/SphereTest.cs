using System.Collections;
using UnityEngine;

using Action = System.Action;

public class SphereTest : MessagingBehaviour
{
    private const int s_slowColorChangeCount = 3;
    private const int s_fastColorChangeCount = 6;
    private const float s_slowChangeTime = 2f;
    private const float s_fastChangeTime = 0.5f;

    [SerializeField] private MeshRenderer m_meshRenderer = null;

    private void Awake()
    {
        Subscribe(MessageType.ChangeSphereColor, ChangeColor);
        Subscribe(MessageType.ChangeColor, ChangeColor, new MessagingSubscriptionConfig { m_DoStopAll = true });
        Subscribe(MessageType.SemaphoreTest, SemaphoreStart);
        Subscribe(MessageType.SemaphoreTest, SemaphoreEnd);
    }

    private void Start()
    {
        m_meshRenderer.material = new Material(m_meshRenderer.material);
    }


    private void ChangeColor(object _obj = null)
    {
        m_meshRenderer.material.color = Random.ColorHSV();
    }

    private void SemaphoreStart(object _obj, Action _ack)
    {
        StartCoroutine(SemaphoreStartDo(_ack));
    }

    private void SemaphoreEnd(object _obj, Action _ack)
    {
        StartCoroutine(SemaphoreEndDo(_ack));
    }

    private IEnumerator SemaphoreStartDo(Action _ack)
    {
        ChangeColor();
        for (int i = 0; i < s_slowColorChangeCount; ++i)
        {
            ChangeColor();
            yield return new WaitForSeconds(s_slowChangeTime);
        }

        _ack?.Invoke();
        yield return null;
    }

    private IEnumerator SemaphoreEndDo(Action _ack)
    {
        Color color1 = Random.ColorHSV();
        Color color2 = Random.ColorHSV();

        bool colorOneApplied = false;

        for (int i = 0; i < s_fastColorChangeCount; ++i)
        {
            m_meshRenderer.material.color = colorOneApplied ? color2 : color1;
            colorOneApplied = !colorOneApplied;
            yield return new WaitForSeconds(s_fastChangeTime);
        }

        _ack?.Invoke();
        yield return null;
    }
}
