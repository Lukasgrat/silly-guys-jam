using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public float attackRadius;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        Debug.Log(Vector3.Distance(player.transform.position, currentPos));
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
}
