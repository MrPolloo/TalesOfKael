using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float hpValue;
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
            if(collision.tag == "Player")
            {
                collision.GetComponent<Health>().hpRestore(hpValue);
                gameObject.SetActive(false);
                
            }
        }
}
