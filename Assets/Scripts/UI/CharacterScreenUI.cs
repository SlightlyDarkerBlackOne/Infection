using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScreenUI : MonoBehaviour
{

    [SerializeField] Text healthText;
    [SerializeField] Text levelText;
    [SerializeField] Text attackText;
    [SerializeField] Text defenceText;
    [SerializeField] Text monstersSlainText;

    public GameObject characterScreenPanel;

    public static CharacterScreenUI instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        healthText.text = PlayerStats.Instance.currentHP.ToString();
        levelText.text = PlayerStats.Instance.currentLevel.ToString();
        attackText.text = PlayerStats.Instance.currentAttack.ToString();
        defenceText.text = PlayerStats.Instance.currentDefense.ToString();
        monstersSlainText.text = UIManager.Instance.numberOfSlainBugs.text;
    }
}
