using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer charSprite;
    private Animator anim;
    public bool grounded;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        charSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one*0.7f;
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1)*0.7f;
        }

        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        anim.SetBool("Dash", horizontalInput != 0) ;
        anim.SetBool("grounded", grounded) ;
        
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Floor")
        {
            grounded = true;
        }
    }
}
