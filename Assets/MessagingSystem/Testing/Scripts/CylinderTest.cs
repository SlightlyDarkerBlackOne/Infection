using UnityEngine;
using Action = System.Action;

public class CylinderTest : MessagingBehaviour
{
    [SerializeField] private MeshRenderer m_meshRenderer = null;

    private void Awake()
    {
        Subscribe(MessageType.ChangeCylinderColor, ChangeColor);
        Subscribe(MessageType.ChangeColor, ChangeColor);
    }

    private void Start()
    {
        m_meshRenderer.material = new Material(m_meshRenderer.material);
    }

    private void ChangeColor(object obj)
    {
        m_meshRenderer.material.color = Random.ColorHSV();
    }

    public void Unsubscribe()
    {
        Unsubscribe(MessageType.ChangeColor, ChangeColor);
        Unsubscribe(MessageType.ChangeCylinderColor, ChangeColor);
    }

    public void Subscribe()
    {
        Subscribe(MessageType.ChangeColor, ChangeColor);
        Subscribe(MessageType.ChangeCylinderColor, ChangeColor);
    }
}
