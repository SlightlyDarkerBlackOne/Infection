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
    public AudioSource dash;
    public AudioSource speedBuff;
    public AudioSource footsteps;

    #region Singleton
    public static SFXManager Instance {get; private set;}

	// Use this for initialization
	void Awake () {
		if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
	}
	#endregion

    public void PlaySound(AudioSource source){
        source.Play();
    }
}
