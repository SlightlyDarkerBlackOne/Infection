using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SkillTree skillTree;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start(){
        skillTree.SetPlayerSkills(player.GetPlayerSkills());
    }
}
