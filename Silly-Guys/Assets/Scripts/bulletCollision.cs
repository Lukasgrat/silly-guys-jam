using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb2d;
    GameObject destroySound;
    float timer = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 vec2 = new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z),
            Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z));
        rb2d.velocity = vec2 * speed;
        destroySound = GameObject.Find("Destroy Bubble Sound");
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0)
        {
            destroySound.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        else if (timer < Time.deltaTime)
        {
            timer = 0;
        }
        else 
        {
            timer -= Time.deltaTime;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyScript>().isSerious)
            {
                Destroy(gameObject);
            }

            collision.gameObject.GetComponent<EnemyScript>().sillyHandler();
        }
        else if (collision.gameObject.tag.Equals("Boss"))
        {
            collision.gameObject.GetComponent<EnemyBoss>().sillyHandler();
        }
        else if (!collision.gameObject.tag.Equals("Player") && !collision.gameObject.tag.Equals("Respawn"))
        {
            destroySound.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
