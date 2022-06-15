using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

	public int health;
    private Animator anim;   
	public bool isInvulnerable = false;

	[SerializeField]private Behaviour[] components; 

    void Start()
    {
		anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
			TakeDamage(100);
        }

	}

    public void TakeDamage(int damage)
	{
		/*if (isInvulnerable)
			return;*/

		health -= damage;
		

		if (health > 0)
		{
			anim.SetTrigger("Hit");
		}

		if (health <= 0)
		{
			anim.SetTrigger("Dead");
			foreach(Behaviour component in components)
            {
				component.enabled = false;
            }
			/*
			if(GetComponent<Boss>() != null)
            {
				GetComponent<Boss>().enabled = false;
			}*/
			
		}

	}

   
}
