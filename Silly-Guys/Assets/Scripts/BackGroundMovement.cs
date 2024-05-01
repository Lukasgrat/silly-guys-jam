using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vec3 = transform.position;
        Vector3 playerForce = controller.GetComponent<Rigidbody2D>().velocity /150;
        this.transform.position = new Vector3(playerForce.x + vec3.x, vec3.y, vec3.z);
    }
}
