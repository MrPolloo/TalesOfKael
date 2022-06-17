using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDeffense : MonoBehaviour
{
    [SerializeField] private float deffensePercent;
    [SerializeField] private float timelife;
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
            collision.GetComponent<Health>().addDeffense(deffensePercent, timelife);
            gameObject.SetActive(false);

        }
    }
}
