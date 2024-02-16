using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Boolean isInAir;
    public float jumpHeight;
    public float movementSpeed;
    public float maxMovement;
    public GameObject playerCamera;
    public int silliesSaved;
    // Start is called before the first frame update
    void Start()
    {
        isInAir = false;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Boolean jumpKey = (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow));
        if (jumpKey && !isInAir)
        {
            rb2D.AddForce(new Vector2(0f, jumpHeight * 100));
            isInAir = true;
        }

        Vector2 horizontalMovement = new Vector2(movementSpeed, 0f);
        if ((Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && maxMovement > rb2D.totalForce.x)
        {
            rb2D.AddForce(horizontalMovement);
            if (false)//this.playerCamera.transform.position.x > -1)
            {
                this.playerCamera.transform.position = new Vector3(
                    this.playerCamera.transform.position.x - .05f, 
                    this.playerCamera.transform.position.y, 
                    this.playerCamera.transform.position.z);
            }
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && maxMovement * -1 < rb2D.totalForce.x)
        {
            rb2D.AddForce(horizontalMovement * -1);
            if (false)//this.playerCamera.transform.position.x < 1)
            {
                this.playerCamera.transform.position = new Vector3(
                    this.playerCamera.transform.position.x + .05f,
                    this.playerCamera.transform.position.y,
                    this.playerCamera.transform.position.z);
            }
        }
        else 
        {
            rb2D.AddForce(resistanceHandler());
        }
    }
    //handles the resistance calculations based on the force of this object
    private Vector2 resistanceHandler() {
        int RESISTANCE = 40;
        Vector2 dir;
        if (rb2D.totalForce.x > 0)
        {
            if (rb2D.totalForce.x < movementSpeed * RESISTANCE)
            {
                dir = new Vector2(rb2D.totalForce.x * -1, 0f);
            }
            else
            {
                dir = new Vector2(movementSpeed * -1 * RESISTANCE, 0f);
            }
        }
        else if (rb2D.totalForce.x < 0)
        {
            if (rb2D.totalForce.x > movementSpeed * -1 * RESISTANCE)
            {
                dir = new Vector2(rb2D.totalForce.x * -1, 0f);
            }
            else
            {
                dir = new Vector2(movementSpeed * RESISTANCE, 0f);
            }
        }
        else 
        {
            dir = new Vector2(0f, 0f);
        }
        return dir;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].point.y < this.transform.position.y - .01 && isInAir) {
            isInAir = false;
        }
    }
    public int getSavedAmounts() 
    { 
        return this.silliesSaved;
    }
    public void increaseSillyAmounts(int ) 
    { 
      return   
    }
}
