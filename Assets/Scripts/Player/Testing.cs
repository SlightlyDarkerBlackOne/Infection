﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SkillTree skillTree;

    void Start(){
        skillTree.SetPlayerSkills(player.GetPlayerSkills());
    }
}
