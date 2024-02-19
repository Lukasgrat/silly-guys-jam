using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public float attackRadius;
    public float moveSpeed;
    public Boolean isSerious;
    public Boolean isJumping;
    public Sprite sillySprite;
    AudioSource m_MyAudioSource;
    Rigidbody2D rb2D;
    float timer;
    public float JUMPINGMAX;
    //Play the music
    //Detect when you use the toggle, ensures music isn’t played multiple times
    bool m_ToggleChange;
    bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
        this.m_ToggleChange = true;
        this.timer = 0;
        canJump = true;
        this.rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSerious) {
            seriousAI();
        }
    }
    public void seriousAI()
    {
        Vector3 currentPos = transform.position;
        if (Vector3.Distance(player.transform.position, currentPos) < attackRadius)
        {
            Debug.Log(canJump);
            if (canJump)
            {
                choosingJumpHandler();
            }
            else
            {
                forcedJumpHandler();
            }
            if (isJumping)
            {
                jumpingHandler();
            }
            audioHandler();
        }
        else 
        {
            m_ToggleChange = true;
        }
    }
    private void choosingJumpHandler()
    {
        Vector3 currentPos = transform.position;
        if (player.transform.position.x > currentPos.x)
        {
            transform.position = new Vector3(
                currentPos.x + moveSpeed * Time.deltaTime,
                currentPos.y,
                currentPos.z
                );
            transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x),
                this.transform.localScale.y,
                this.transform.localScale.z);
        }
        else if (player.transform.position.x < currentPos.x)
        {
            transform.position = new Vector3(
                    currentPos.x - moveSpeed * Time.deltaTime,
                    currentPos.y,
                    currentPos.z
                    );
            transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x),
                this.transform.localScale.y,
                this.transform.localScale.z);
        }
    }
    private void forcedJumpHandler()
    {
        Vector3 currentPos = transform.position;
        if (this.transform.localScale.x < 0)
        {
            transform.position = new Vector3(
                currentPos.x + moveSpeed * Time.deltaTime,
                currentPos.y,
                currentPos.z
                );
        }
        else
        {
            transform.position = new Vector3(
                    currentPos.x - moveSpeed * Time.deltaTime,
                    currentPos.y,
                    currentPos.z
                    );
        }
    }
    private void jumpingHandler() 
    {
        if (this.timer == 0 && canJump)
        {
            rb2D.AddForce(new Vector2(0f, 900));
            canJump = false;
            this.timer = JUMPINGMAX;
        }
        else if (this.timer - Time.deltaTime < 0)
        {
            this.timer = 0;
        }
        else { this.timer -= Time.deltaTime; }
    }
    private void audioHandler()
    {
        //Check to see if you just set the toggle to positive
        if (!m_MyAudioSource.isPlaying && m_ToggleChange)
        {
            m_MyAudioSource.Play();
            m_ToggleChange = false;
        }
    }
    public void sillyHandler()
    {
        if (isSerious) { 
            isSerious = false;
            this.GetComponent<SpriteRenderer>().sprite = this.sillySprite;
            this.player.GetComponent<PlayerController>().increaseSillyAmounts(1);
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
