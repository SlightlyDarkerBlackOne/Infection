using UnityEngine;
using Action = System.Action;

public class CubeTest : MessagingBehaviour
{
    [SerializeField] private MeshRenderer m_meshRenderer = null;

    private void Awake()
    {
        Subscribe(MessageType.ChangeCubeColor, ChangeColor);
        Subscribe(MessageType.ChangeColor, ChangeColor);
        Subscribe(MessageType.ChangeCubeToColor, ChangeToColor);
    }

    private void Start()
    {
        m_meshRenderer.material = new Material(m_meshRenderer.material);
    }

    private void ChangeColor(object _obj)
    {
        m_meshRenderer.material.color = Random.ColorHSV();
    }

    public void ChangeToColor(object color)
    {
        if (color is not Color) return;

        m_meshRenderer.material.color = (Color)color;
    }
}
