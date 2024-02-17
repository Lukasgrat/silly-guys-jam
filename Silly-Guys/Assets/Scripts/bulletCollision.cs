using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 vec2 = new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z),
            Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z));
        rb2d.velocity = vec2 * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().sillyHandler();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
