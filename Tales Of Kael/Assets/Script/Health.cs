using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.R))
        // {
        //     TakeDamage(1);
        // }

    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else 
        {
            anim.SetTrigger("die");
            if(gameObject.tag == "Player")
            {
                anim.SetBool("grounded", true);
                // Time.timeScale = 0;
                GetComponent<PlayerMovement>().enabled = false;
            }
            if(gameObject.tag== "Enemy")
            {
                GetComponent<Enemy>().enabled = false;
                GetComponent<EnemyAttack>().enabled = false;
                GetComponent<Health>().enabled = false;
            }
        }
    }

    public void hpRestore(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void despawn()
    {
        gameObject.SetActive(false);
    }
    public void over()
    {
        GameManager.instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Void")
        {
            over();
        }
    }   
}
