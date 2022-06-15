using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private Animator anim;
    //private PlayerMovement playerMovement;
    private float cooldownTimer = 0;

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private LayerMask layer;
    private float range = 10f;
    [SerializeField] private float distanceCollider = 0.55f;
    [SerializeField] private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckPlayer())
        {
            if (cooldownTimer > attackCooldown)
            {
                anim.SetTrigger("Attack");
                Attack();
            }
            
        }
        cooldownTimer += Time.deltaTime;
    }

    private bool CheckPlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distanceCollider, new Vector3
        (boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, layer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distanceCollider, new Vector3
        (boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void Attack()
    {
        cooldownTimer = 0;
        attackCooldown = 5;
        
        fireballs[CheckFireball()].transform.position = firepoint.position;
        fireballs[CheckFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        
    }

    private int CheckFireball()
    {
        for(int i = 0; i < fireballs.Length; i++)
        {
            if(!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }


}
