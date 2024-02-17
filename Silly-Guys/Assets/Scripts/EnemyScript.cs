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
            }
            else if (player.transform.position.x < currentPos.x)
            {
                transform.position = new Vector3(
                        currentPos.x - moveSpeed * Time.deltaTime,
                        currentPos.y,
                        currentPos.z
                        );
            }
        }
    }
    public void sillyHandler()
    {
        if (isSerious) { 
            isSerious = false;
            this.GetComponent<SpriteRenderer>().color = Color.green;
            this.player.GetComponent<PlayerController>().increaseSillyAmounts(1);
        }
    }
}
