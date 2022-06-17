using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private Animator anim;
    //private PlayerMovement playerMovement;
    private float cooldownTimer = 0;
    private BossHealth Health;

    [SerializeField] private GameObject Boss;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    // Start is called before the first frame update
    void Awake()
    {
        Health = Boss.GetComponent<BossHealth>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Health.health <= 20)
        {
            StartFinalAttack();
        }
    }

    // Update is called once per frame
    private void StartFinalAttack()
    {
        if (cooldownTimer > attackCooldown)
        {
            anim.SetTrigger("Attack");
            finalAttack();
        }
        cooldownTimer += Time.deltaTime;

    }

    private void finalAttack()
    {
        cooldownTimer = 0;
        attackCooldown = 5;

        fireballs[CheckFireball()].transform.position = firepoint.position;
        fireballs[CheckFireball()].GetComponent<BossProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int CheckFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}
