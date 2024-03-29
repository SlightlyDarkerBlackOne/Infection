﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public AudioSource playerHurt;
    public AudioSource playerDead;
    public AudioSource playerAttack;
    public AudioSource levelUP;
    public AudioSource playerHealed;
    public AudioSource enemyHit;
    public AudioSource enemyDead;
    public AudioSource itemPickedUp;

    public AudioSource soundTrack;
    public AudioSource bossTrackDrums;
    public AudioSource bossSpawnGrunt;

    public AudioSource dash;
    public AudioSource speedBuff;
    public AudioSource footsteps;
    public AudioSource breakCrate;
    public AudioSource manaPotion;
    public AudioSource bowHitSolid;
    public AudioSource[] bowFire;

    public AudioSource deathScreenFail;

    #region Singleton
    public static SFXManager Instance {get; private set;}

	// Use this for initialization
	void Awake () {
		if (Instance == null) {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
	}
    #endregion

    private void Start() {
        PlayerHealthManager.PlayerDead += PlayDeathScreen;
    }
    //private void OnDisable() {
    //    PlayerController2D.Instance.GetComponent<Player>().playerHealthManager.PlayerDead -= PlayDeathScreen;
    //}
    public void PlaySound(AudioSource source){
        source.Play();
    }
    private void PlayOnLoop(AudioSource source) {
        source.Play();
        source.loop = true;
    }

    public void PlayBowFireSound()
    {
        int random = Random.Range(0, 3);
        bowFire[random].Play();
    }

    public void PlayBossMusic(AudioSource source) {
        soundTrack.Stop();
        PlayOnLoop(source);
    }
    public void PlaySoundTrack(AudioSource source) {
        bossTrackDrums.Stop();
        PlayOnLoop(source);
    }

    private void PlayDeathScreen() {
        deathScreenFail.Play();
    }
}
