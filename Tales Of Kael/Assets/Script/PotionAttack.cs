using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionAttack : MonoBehaviour
{
    [SerializeField] private int attackPercent;
    [SerializeField] private float timelife;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            collision.GetComponent<PlayerAttack>().addAttackValue(attackPercent, timelife);
            gameObject.SetActive(false);

        }
    }
}
