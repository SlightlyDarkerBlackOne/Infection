using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFXManager : MessagingBehaviour
{
	public AudioSource playerHurt;
	public AudioSource playerDead;
	public AudioSource youDied;
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
	public AudioSource outOfMana;
	public AudioSource bowHitSolid;
	public AudioSource[] bowFire;

	public AudioSource deathScreenFail;

	private static readonly Dictionary<AudioSource, float> s_lastPlayTime = new Dictionary<AudioSource, float>();

	private void Awake()
	{
		Subscribe(MessageType.PlayerDamaged, OnPlayerDamaged);
		Subscribe(MessageType.PlayerHealed, OnPlayerHealed);
		Subscribe(MessageType.PlayerDied, PlayDeathScreenSound);
		Subscribe(MessageType.LevelUp, PlayLevelUpSound);
		Subscribe(MessageType.PlayerDash, PlayDashSound);
		Subscribe(MessageType.BreakCrate, PlayBreakCrateSound);
		Subscribe(MessageType.ManaPotionPop, PlayManaPotionPopSound);
		Subscribe(MessageType.BossSpawned, BossSpawned);
		Subscribe(MessageType.BowFired, PlayBowFireSound);
		Subscribe(MessageType.ItemPickedUp, ItemPickedUp); 
		Subscribe(MessageType.BowHitSolid, BowHitSolid); 
		Subscribe(MessageType.EnemyHit, EnemyHit);
		Subscribe(MessageType.EnemyKilled, EnemyDead);
		Subscribe(MessageType.BossDied, BossDied);
		Subscribe(MessageType.NotEnoughMana, PlayNotEnoughManaSound);
	}

	private void PlayNotEnoughManaSound(object obj)
	{
		PlaySound(outOfMana, cooldown: 1f);
	}

	private void BossDied(object obj)
	{
		PlaySoundTrack(soundTrack);
	}

	private void EnemyDead(object obj)
	{
		PlaySound(enemyDead);

	}

	private void EnemyHit(object obj)
	{
		PlaySound(enemyHit);
	}

	private void BowHitSolid(object obj)
	{
		PlaySound(bowHitSolid);
	}

	private void OnPlayerDamaged(object obj)
	{
		PlaySound(playerHurt);
	}

	private void OnPlayerHealed(object obj)
	{
		PlaySound(playerHealed);
	}

	private void PlayLevelUpSound(object obj)
	{
		PlaySound(levelUP);
	}

	private void PlayDashSound(object obj)
	{
		PlaySound(dash);
	}

	private void PlayBreakCrateSound(object obj)
	{
		PlaySound(breakCrate);
	}

	private void PlayManaPotionPopSound(object obj)
	{		
		PlaySound(manaPotion);
	}

	private void BossSpawned(object obj)
	{
		PlaySound(bossSpawnGrunt); 
		PlayBossMusic(bossTrackDrums);
	}

	private void ItemPickedUp(object obj)
	{
		PlaySound(itemPickedUp);
	}

	private void PlaySound(AudioSource source, float lowPitch = 0.8f, float highPitch = 1.2f, float cooldown = 0f)
	{
		if (source.isPlaying || (cooldown > 0f && Time.time < s_lastPlayTime.GetValueOrDefault(source, 0f) + cooldown))
			return;

		source.pitch = Random.Range(lowPitch, highPitch);
		source.Play();

		if (cooldown > 0f)
		{
			s_lastPlayTime[source] = Time.time;
		}
	}

	private void PlayOnLoop(AudioSource source)
	{
		source.Play();
		source.loop = true;
	}

	private void PlayBowFireSound(object obj)
	{
		int random = Random.Range(0, 3);
		bowFire[random].Play();
	}

	public void PlayBossMusic(AudioSource source)
	{
		soundTrack.Stop();
		PlayOnLoop(source);
	}

	private void PlaySoundTrack(AudioSource source)
	{
		bossTrackDrums.Stop();
		PlayOnLoop(source);
	}

	private void PlayDeathScreenSound(object _obj)
	{
		PlaySound(youDied, 0.6f, 1.2f);
		StartCoroutine(PlaySoundWithDelay(deathScreenFail, 1.1f));
	}

	private IEnumerator PlaySoundWithDelay(AudioSource source, float delay)
	{
		yield return new WaitForSecondsRealtime(delay);
		PlaySound(source);
	}
}
