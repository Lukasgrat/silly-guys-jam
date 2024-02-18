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

    // Start is called before the first frame update
    void Start()
    {       
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
