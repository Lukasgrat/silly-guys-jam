using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    internal PlayerController player;
    [SerializeField]
    internal float attackRadius;
    [SerializeField]
    internal float moveSpeed;
    [SerializeField]
    internal Boolean isSerious;
    [SerializeField]
    internal Boolean isJumping;
    [SerializeField]
    internal Sprite sillySprite;
    internal AudioSource m_MyAudioSource;
    internal Rigidbody2D rb2D;
    internal float timer = 0;
    [SerializeField]
    internal float JUMPINGMAX;
    internal AudioSource sillied;
    [SerializeField]
    internal SpriteRenderer textBubble;
    [SerializeField]
    internal TextMeshPro text;
    [SerializeField]
    internal String serious;
    [SerializeField]
    internal String sillyText;
    [SerializeField]
    internal float FADETIME = 2f;
    internal float fadeTimer;
    [SerializeField]
    internal bool hasSpeechBubble;
    //Play the music
    //Detect when you use the toggle, ensures music isn’t played multiple times
    //Ensure the toggle is set to true for the music to play at start-up
    internal bool m_ToggleChange = true;
    internal bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        this.rb2D = GetComponent<Rigidbody2D>();
        sillied = GameObject.Find("Sillify Sound").GetComponent<AudioSource>();

        if (this.textBubble != null)
        {
            textBubble.gameObject.SetActive(false);
        }
        if (hasSpeechBubble)
        {
            textBubble.gameObject.SetActive(false);
            text.text = serious;
            fadeTimer = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSerious) {
            seriousAI();
        }
        if (hasSpeechBubble)
        {
            textHandler();
        }
    }

    internal void textHandler()
    {
        Vector3 currentPos = transform.position;
        if (Vector3.Distance(player.transform.position, currentPos) < attackRadius
            && !player.GetComponent<PlayerController>().isLocked)
        {
            this.textBubble.gameObject.SetActive(true);
            Color color = this.textBubble.color;
            float startOpacity = color.a;

            if (this.fadeTimer == this.FADETIME)
            {
                return;
            }
            else if (this.fadeTimer + Time.deltaTime >= this.FADETIME)
            {
                this.fadeTimer = this.FADETIME;
            }
            else
            {
                this.fadeTimer += Time.deltaTime;
            }
            float blend = Mathf.Clamp01(this.fadeTimer / this.FADETIME);
            color.a = blend;
            this.textBubble.color = color;
        }
        else 
        {
            this.textBubble.gameObject.SetActive(false);
            fadeTimer = 0f;
        }
    }

    internal void seriousAI()
    {
        Vector3 currentPos = transform.position;
        if (Vector3.Distance(player.transform.position, currentPos) < attackRadius 
            && !player.GetComponent<PlayerController>().isLocked)
        {
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
    internal void choosingJumpHandler()
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
            Vector3 textScale = this.text.gameObject.transform.localScale;
            this.text.gameObject.transform.localScale = new Vector3(Mathf.Abs(textScale.x) * -1, textScale.y, textScale.z);
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
            Vector3 textScale = this.text.gameObject.transform.localScale;
            this.text.gameObject.transform.localScale = new Vector3(Mathf.Abs(textScale.x), textScale.y, textScale.z);
        }
    }
    internal void forcedJumpHandler()
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
    internal void jumpingHandler() 
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
    internal void audioHandler()
    {
        //Check to see if you just set the toggle to positive
        if (!m_MyAudioSource.isPlaying && m_ToggleChange)
        {
            m_MyAudioSource.Play();
            m_ToggleChange = false;
        }
    }
    public virtual void sillyHandler()
    {
        if (isSerious) {
            isSerious = false;
            if (this.hasSpeechBubble)
            {
                this.text.text = this.sillyText;
            }
            sillied.Play();
            this.GetComponent<SpriteRenderer>().sprite = this.sillySprite;
            Debug.Log("here");
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
