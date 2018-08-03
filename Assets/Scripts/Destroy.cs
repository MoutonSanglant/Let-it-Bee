using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public Transform respawnPoint;
    public bool DoNotDestroy;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!DoNotDestroy && other.gameObject.CompareTag("Ground"))
          Destroy(this.gameObject);
        else if (other.gameObject.CompareTag("Player"))
        {
            if (!DoNotDestroy)
                Destroy(this.gameObject);
            
            other.transform.position = respawnPoint.transform.position;
        }           
    }
}