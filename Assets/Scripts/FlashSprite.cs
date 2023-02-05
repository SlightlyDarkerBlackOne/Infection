using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    private bool flashActive;
    public float flashLength;
    [HideInInspector]
    public float flashCounter;

    #region Singleton
    public static FlashSprite Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Update() {
        Flash(PlayerController2D.Instance.transform.Find("Animation").GetComponent<SpriteRenderer>());
    }
    //Flashing the unit sprite with white when it takes damage
    public void Flash(SpriteRenderer spriteRenderer) {
        if (flashActive) {
            if (flashCounter > flashLength * 0.66f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            } else if (flashCounter > flashLength * 0.33f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            } else if (flashCounter > 0) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            } else {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
    }

   public void SetCounter() {
        flashActive = true;
        flashCounter = flashLength;
    }
}
