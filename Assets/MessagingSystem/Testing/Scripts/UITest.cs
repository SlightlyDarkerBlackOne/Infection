using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    [SerializeField] private Button m_cylinderBtn;
    [SerializeField] private Button m_cubeBtn;
    [SerializeField] private Button m_sphereBtn;
    [SerializeField] private Button m_allBtn;
    [SerializeField] private Button m_unsubscribeCylinder;
    [SerializeField] private Button m_subscribeCylinder;
    [SerializeField] private Button m_changeCubeToColor;
    [SerializeField] private Button m_semaphoreTest;
    [SerializeField] private Button m_restart;
    [SerializeField] private Color m_targetColor;
    [Space]
    [SerializeField] private CylinderTest m_cylinder;

    private void Awake()
    {
        m_cylinderBtn.onClick.AddListener(() => MessagingSystem.Publish(MessageType.ChangeCylinderColor));
        m_cubeBtn.onClick.AddListener(() => MessagingSystem.Publish(MessageType.ChangeCubeColor));
        m_sphereBtn.onClick.AddListener(() => MessagingSystem.Publish(MessageType.ChangeSphereColor));
        m_allBtn.onClick.AddListener(() => MessagingSystem.Publish(MessageType.ChangeColor));
        m_changeCubeToColor.onClick.AddListener(() => MessagingSystem.Publish(MessageType.ChangeCubeToColor, m_targetColor));
        m_unsubscribeCylinder.onClick.AddListener(() => m_cylinder.Unsubscribe());
        m_subscribeCylinder.onClick.AddListener(() => m_cylinder.Subscribe());
        m_restart.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
        m_semaphoreTest.onClick.AddListener(() => { MessagingSystem.Publish(MessageType.SemaphoreTest); });
    }
}

