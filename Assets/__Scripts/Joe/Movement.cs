using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
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
    

    // Update is called once per frame
    void FixedUpdate()
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
    private void Update()
    {
        onGround = Physics2D.OverlapCircle(transform.position, polygen.radius + jumpRadiusOffset , groundLayer);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            var velocity = rigid.velocity;
            velocity = new Vector2(
                velocity.x * leapScale,
                jumpPower);
            rigid.velocity = velocity;
        }
    }
}
