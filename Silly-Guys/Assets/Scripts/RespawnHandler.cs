using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) 
        {
            collision.gameObject.GetComponent<PlayerController>().respawn = this;
        }
    }
}
