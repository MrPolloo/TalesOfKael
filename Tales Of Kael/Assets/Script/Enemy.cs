using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Player;
    public bool flip;
    public float speed;
    private Vector3 position;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int checkRange;
    [SerializeField] private int checkHeight;
    [SerializeField] private int distanceColliderRange;
    private Animator anim;
    [SerializeField] private bool Move;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update () 
    {
        if(gameObject.GetComponent<Health>())
        {
            movement();
        }
    }

    private bool checkArea()
    {
        RaycastHit2D check = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * checkRange * transform.localScale.x * distanceColliderRange, new Vector3(boxCollider.bounds.size.x * checkRange, boxCollider.bounds.size.y * checkHeight,  boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return check.collider != null;
    }
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * checkRange * transform.localScale.x * distanceColliderRange, new Vector3(boxCollider.bounds.size.x * checkRange, boxCollider.bounds.size.y * checkHeight,  boxCollider.bounds.size.z));
    }

    private void movement()
    {
        if(checkArea())
        {
            if(Player != null && gameObject != null)
            {
                Vector3 scale = transform.localScale;
                float dist = Vector3.Distance(Player.transform.position, gameObject.transform.position);
                if(Player.transform.position.x > gameObject.transform.position.x)
                {
                    scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                    if(Move)
                    {
                        if(dist > 0.6f)
                        {
                            transform.Translate(speed * Time.deltaTime, 0, 0);
                            anim.SetBool("moving", true);
                            // Debug.Log(dist);
                        }
                    }
                }
                else
                {
                    scale.x = Mathf.Abs(scale.x) * 1 * (flip ? -1 : 1);
                    if(Move)
                    {
                        if(dist > 0.6f)
                        {
                            transform.Translate(speed * Time.deltaTime * -1, 0, 0);
                            anim.SetBool("moving", true);
                            // Debug.Log(dist);
                        }
                    }
                }
                transform.localScale = scale;
            }
        }
        else 
        {
            if(Move)
            {
                if(Player != null && gameObject != null)
                {
                    Vector3 scale = transform.localScale;
                    float distSpawn = Vector3.Distance(position, gameObject.transform.position);
                    if(position.x < gameObject.transform.position.x)
                    {
                        scale.x = Mathf.Abs(scale.x) * 1 * (flip ? -1 : 1);
                        if(distSpawn>0.4f)
                        {
                            transform.Translate(speed * Time.deltaTime * -1, 0, 0);
                            anim.SetBool("moving", true);
                        }
                    }
                    else
                    {
                        scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                        if(distSpawn>0.4f)
                        {
                            transform.Translate(speed * Time.deltaTime, 0, 0);
                            anim.SetBool("moving", true);
                        }
                    }
                    transform.localScale = scale;
                    anim.SetBool("moving", false);
                }
            }
        }
    }
}