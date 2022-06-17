using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    [SerializeField] public float deffense_timelife;
    [SerializeField] public float deffense;
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
        if (deffense_timelife > 0)
        {
            deffense_timelife -= Time.deltaTime;
        }
        else
        {
            deffense = 0;
        }
        // if(Input.GetKeyDown(KeyCode.R))
        // {
        //     TakeDamage(1);
        // }

    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - (_damage - (_damage * (deffense / 100))), 0, startingHealth);
        Debug.Log(currentHealth);

        if (currentHealth > 0)
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


    public void addHealth(float healthPercent)
    {
        currentHealth = currentHealth + ((healthPercent / 100) * startingHealth);

       
    }

    public void addDeffense(float deffensePercent, float timelife)
    {
        deffense = deffensePercent;
        deffense_timelife = timelife;
    }
}
