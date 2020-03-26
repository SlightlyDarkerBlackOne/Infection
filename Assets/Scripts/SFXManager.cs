using System.Collections;
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


    private static bool sfxmanExists;

	// Use this for initialization
	void Start () {
		if (!sfxmanExists) {
            sfxmanExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
