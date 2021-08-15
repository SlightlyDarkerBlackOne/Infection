using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossItems : MonoBehaviour
{
    public GameObject itemToDrop;

    private EnemyHealthManager boss;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        boss = gameObject.GetComponent<EnemyHealthManager>();
        boss.OnEnemyDeath += EnemyHealthManager_OnDeath;
    }
   
    private void EnemyHealthManager_OnDeath(object sender, System.EventArgs e) {
        DropItems();
    }

    private void DropItems(){
        Vector3 position = gameObject.transform.position;
        if(itemToDrop != null)
            Instantiate(itemToDrop, position, Quaternion.identity);
        SFXManager.Instance.PlaySoundTrack(SFXManager.Instance.soundTrack);
    }
}
