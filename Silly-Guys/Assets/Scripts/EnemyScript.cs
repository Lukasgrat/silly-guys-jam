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
    public Sprite sillySprite;
    AudioSource m_MyAudioSource;
    //Play the music
    //Detect when you use the toggle, ensures music isn’t played multiple times
    bool m_ToggleChange;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
        this.m_ToggleChange = true;
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
            audioHandler();
        }
        else 
        {
            m_ToggleChange = true;
        }
    }
    private void audioHandler()
    {
        Debug.Log("here");
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
}
