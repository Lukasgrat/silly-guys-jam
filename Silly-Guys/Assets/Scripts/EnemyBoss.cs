using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBoss : EnemyScript
{
    [SerializeField]
    GameObject nextLevelScreen;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
        this.rb2D = GetComponent<Rigidbody2D>();
        sillied = GameObject.Find("Sillify Sound").GetComponent<AudioSource>();
        if (this.hasSpeechBubble)
        {
            text.text = serious;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSerious) {
            seriousAI();
        }
    }


    public override void sillyHandler()
    {
        if (isSerious) {
            isSerious = false;
            sillied.Play();
            this.GetComponent<SpriteRenderer>().sprite = this.sillySprite;
            this.player.GetComponent<PlayerController>().increaseSillyAmounts(1);
            this.player.lockMovement(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canJump && collision.contacts[0].point.y < this.transform.position.y - .01)
        {
            canJump = true;
        }
    }
}
