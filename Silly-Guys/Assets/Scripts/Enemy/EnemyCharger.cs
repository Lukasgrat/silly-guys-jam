using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharger : EnemyScript
{
    private float swapDirectionTimer;
    private readonly float MAXSWAPDIRECTIONTIMEER = .5f; 
    private bool facingFront = true;
    [SerializeField]
    private float angerTimer;
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
        if (isSerious)
        {
            seriousAI();
        }
        if (hasSpeechBubble)
        {
            textHandler();
        }
    }

    private void seriousAI() 
    {
        Vector3 currentPos = transform.position;
        int scaler = 1;
        if (player.transform.position.x > currentPos.x)
        {
            if (this.facingFront)
            {
                if (this.swapDirectionTimer == 0f)
                {
                    this.facingFront = false;
                }
                else if (swapDirectionTimer < Time.deltaTime)
                {
                    swapDirectionTimer = 0f;
                }
                else
                {
                    swapDirectionTimer -= Time.deltaTime;
                }
            }
            else 
            {
                swapDirectionTimer = MAXSWAPDIRECTIONTIMEER;
            }
        }
        else 
        {
            if (!this.facingFront)
            {
                if (this.swapDirectionTimer == 0f)
                {
                    this.facingFront = true;
                }
                else if (swapDirectionTimer < Time.deltaTime)
                {
                    swapDirectionTimer = 0f;
                }
                else
                {
                    swapDirectionTimer -= Time.deltaTime;
                }
            }
            else
            {
                swapDirectionTimer = MAXSWAPDIRECTIONTIMEER;
            }
        }
        if (!facingFront) 
        {
            scaler = -1;
        }
        transform.position = new Vector3(
               currentPos.x - moveSpeed * Time.deltaTime * scaler,
               currentPos.y,
               currentPos.z
               );
        transform.localScale = new Vector3(scaler * Mathf.Abs(this.transform.localScale.x),
            this.transform.localScale.y,
            this.transform.localScale.z);
        Vector3 textScale = this.text.transform.localScale;
        this.text.transform.localScale = new Vector3(Mathf.Abs(textScale.x) * scaler, textScale.y, textScale.z);
        if (this.angerTimer == 0f)
        {
            this.sillify();
        }
        else if (this.angerTimer < Time.deltaTime)
        {
            this.angerTimer = 0f;
        }
        else 
        {
            this.angerTimer -= Time.deltaTime;
        }
    }

    public override void sillyHandler()
    {
    }

    private void sillify() 
    {
        if (isSerious)
        {
            isSerious = false;
            if (this.hasSpeechBubble)
            {
                this.text.text = this.sillyText;
            }
            sillied.Play();
            this.GetComponent<SpriteRenderer>().sprite = this.sillySprite;
            this.player.GetComponent<PlayerController>().increaseSillyAmounts(1);
        }
    }
}
