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
    public bool isLocked;
    [Header("Movement Values")]
    public float jumpHeight;
    public float movementSpeed;
    public float maxMovement;
    public GameObject playerCamera;
    [Header("Enemy Interactions")]
    public int silliesSaved;
    public GameObject deathScreen;
    public TextMeshProUGUI sillies;
    public GameObject meleeWeapon;
    public Transform firePoint;
    public GameObject bulletPref;
    public GameObject respawn;
    // Start is called before the first frame update

    void Start()
    {
        isInAir = false;
        rb2D = GetComponent<Rigidbody2D>();
        this.isLocked = false;
        this.deathScreen.SetActive(false);
        this.meleeWeapon.SetActive(true);
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!this.isLocked)
        {
            movementHandler();
            //meleeHandler();
        }
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            respawnHandler();
        }
        Boolean jumpKey = (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow));
        if (jumpKey && !isInAir && !this.isLocked)
        {
            rb2D.AddForce(new Vector2(0f, jumpHeight * 100));
            this.gameObject.GetComponent<AudioSource>().Play();
            isInAir = true;
        }
    }

    //EFFECT:Sets the player as fully active again and sends them to their prior spawn point
    private void respawnHandler() {
        this.transform.position = new Vector3(respawn.transform.position.x,
            respawn.transform.position.y,
            0);
        this.isLocked = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        this.deathScreen.SetActive(false);
    }


    //EFFECT: adds force to the player
    //handles movement inputs and redirects for resistances
    private void movementHandler() 
    {
        Vector2 horizontalMovement = new Vector2(movementSpeed * Time.fixedDeltaTime * 50, 0f);
        if ((Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && maxMovement > rb2D.totalForce.x)
        {
            rb2D.AddForce(horizontalMovement);
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, 1);
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && maxMovement * -1 < rb2D.totalForce.x)
        {
            rb2D.AddForce(horizontalMovement * -1);
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x) * -1, this.transform.localScale.y, 1);
        }
        else
        {
            rb2D.AddForce(resistanceHandler());
        }
    }
    //handles the resistance calculations based on the force of this object
    private Vector2 resistanceHandler() {
        float RESISTANCE = 300 * Time.fixedDeltaTime * 50;
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
        this.deathScreen.SetActive(true);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        this.isLocked = true;
        
    }
}
