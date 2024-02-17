using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using TMPro;
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
    public GameObject deathScreen;
    bool isLocked;
    public TextMeshProUGUI sillies;
    float attackTimer;
    const float MELEECOOLDOWN = 1f;
    public GameObject meleeWeapon;
    // Start is called before the first frame update
    void Start()
    {
        isInAir = false;
        rb2D = GetComponent<Rigidbody2D>();
        this.isLocked = false;
        this.deathScreen.SetActive(false);
        attackTimer = 0f;
        this.meleeWeapon.SetActive(false);
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
        if (!this.isLocked)
        {
            movementHandler();
            meleeHandler();

        }
    }
    //EFFECT: adds force to the player
    //handles movement inputs and redirects for resistances
    private void movementHandler() 
    {
        Vector2 horizontalMovement = new Vector2(movementSpeed, 0f);
        if ((Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && maxMovement > rb2D.totalForce.x)
        {
            rb2D.AddForce(horizontalMovement);
            this.transform.localScale = new Vector3(1,1,1);
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
            this.transform.localScale = new Vector3( -1, 1,1);
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
    //handles melee hits
    private void meleeHandler() {
        if (this.attackTimer == 0 && Input.GetKeyDown(KeyCode.S))
        {
            this.attackTimer = MELEECOOLDOWN;
            this.meleeWeapon.SetActive(true);
        }
        else if (this.attackTimer - Time.deltaTime < 0)
        {
            this.attackTimer = 0;
        }
        else { 
            this.attackTimer -= Time.deltaTime;
        }
        if (this.attackTimer < MELEECOOLDOWN / 3) {
            this.meleeWeapon.SetActive(false);
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
        if (collision.gameObject.tag.Equals("Enemy") && collision.gameObject.GetComponent<EnemyScript>().isSerious) 
        {
            death();
        }
    }
    public int getSavedAmounts() 
    { 
        return this.silliesSaved;
    }
    public void increaseSillyAmounts(int amount) 
    { 
        this.silliesSaved += amount;
        this.sillies.text = "Sillies Saved: " + this.silliesSaved.ToString();
    }
    public void death() 
    {
        this.gameObject.SetActive(false);
        this.deathScreen.SetActive(true);
    }
}
