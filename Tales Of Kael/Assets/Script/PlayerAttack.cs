using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;
    [Header("Melee Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int attDmg;
    [SerializeField] private float range;
    [SerializeField] private float distanceCollider;
    [SerializeField] private CapsuleCollider2D boxCollider;
    private float cooldownTimerAtt = Mathf.Infinity;

    [Header("Fire Attack")]
    [SerializeField] private float fireCooldown;
    [SerializeField] public int fireDmg;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private float cooldownTimerFire = Mathf.Infinity;
    private bool explode;

    [SerializeField] private LayerMask enemyLayer;
    private Animator anim;
    private PlayerMovement playerMovement;
    private Health enemyHealth;
    private int damage;

    private void Awake() 
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && cooldownTimerAtt > attackCooldown)
        {
            Attack();
        }
        if(Input.GetKey(KeyCode.Mouse1) && cooldownTimerFire > attackCooldown)
        {
            Fire();
        }
        cooldownTimerFire += Time.deltaTime;
        cooldownTimerAtt += Time.deltaTime;
    }

    private void Fire()
    {
        damage = fireDmg;
        anim.SetTrigger("Fire");
        cooldownTimerFire = 0;
        
        fireballs[CheckFireball()].transform.position = firePoint.position;
        fireballs[CheckFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));   
    }

    private void Attack()
    {
        damage = attDmg;
        anim.SetTrigger("Attack");
        cooldownTimerAtt = 0;
    }

    public bool checkEnemy()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distanceCollider, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y,  boxCollider.bounds.size.z), 0, Vector2.left, 0, enemyLayer);

        if(hit.collider != null)
        {
            enemyHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }
    
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distanceCollider, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y,  boxCollider.bounds.size.z));
    }

    public void DamageEnemy()
    {
        if(checkEnemy())
        {
            if(enemyHealth.currentHealth>0)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log(enemyHealth.currentHealth);
            }
        }
    }

    public int CheckFireball()
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
