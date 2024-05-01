using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase : MonoBehaviour
{
	[SerializeField] private string m_name;
	[SerializeField] private string m_description;

	[SerializeField] private Dialogue m_startdialogue;
	[SerializeField] private Dialogue m_inProgressdialogue;

}
