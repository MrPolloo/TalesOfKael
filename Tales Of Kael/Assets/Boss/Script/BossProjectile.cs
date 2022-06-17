using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    public float damage;
    private float direction;
    private bool isHit;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float projectile_lifetime;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isHit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        projectile_lifetime += Time.deltaTime;
        if(projectile_lifetime > 2)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("Explode");
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

    public void SetDirection(float projectileDirection)
    {
        projectile_lifetime = 0;
        direction = projectileDirection;

        gameObject.SetActive(true);
        isHit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != projectileDirection) 
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
