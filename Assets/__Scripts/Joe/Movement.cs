using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    //Modifier Access will private for this class
    [Header("Inspector")]
    public float speed;
    public float jumpPower;
    public float leapScale;
    public float jumpRadiusOffset;
    public float maxAngVelo;
    public LayerMask groundLayer;

    public PolygonGen polygen;

    [Header("Dynamic")]
    public Rigidbody2D rigid;
    public float velo;
    public float angVelo;
    public Vector2 dir;
    [SerializeField]private bool onGround;

    
    private void Start()
    {
        Rigidbody2D tmp = GetComponent<Rigidbody2D>();
        rigid = tmp == null ? new Rigidbody2D() : tmp;
    }
    

   private void FixedUpdate()
    {
        velo = -Input.GetAxisRaw("Horizontal") * speed;
        dir = new Vector2(velo * Time.deltaTime, transform.position.y);


        rigid.AddTorque(velo);
        angVelo = rigid.angularVelocity;
      
        if (angVelo > maxAngVelo)
        {
            rigid.angularVelocity = maxAngVelo;
        }
        if (angVelo < -maxAngVelo)
        {
            rigid.angularVelocity = -maxAngVelo;
        }
    }
    
    //TWO UPDATES IN ONE CLASS - BAD
    //Movement class has Jump. Just create class Jumper and do jump mechanic. It will be easy to refactoring
    private void Update()
    {
        onGround = Physics2D.OverlapCircle(transform.position, polygen.radius + jumpRadiusOffset , groundLayer); // +rep

        if (Input.GetButtonDown(KeyCode.Space) && onGround) //USE KeyCode, destroy fucking strings to check
        {
            var velocity = rigid.velocity;
            velocity = new Vector2(
                velocity.x * leapScale,
                jumpPower);
            rigid.velocity = velocity;
        }
    }
}
