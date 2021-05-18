using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public GameObject destroyEffect;
    public bool isEnemyObject;

    private Transform player;
    private Vector2 target;

    private void Start() {
        player = PlayerController2D.Instance.transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y) {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile() {

        //PlaySound
        if (destroyEffect != null) {
            GameObject effect = Instantiate(destroyEffect, player);
            Destroy(effect, 2f);
        }
        if(isEnemyObject != true)
            Destroy(gameObject);
    }
}
