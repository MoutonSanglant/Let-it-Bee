using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

 //   [SerializeField] private Transform player;
    public Transform respawnPoint;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
     // if (other.gameObject.name == "Ground")
          Destroy(this.gameObject);
      else if (other.gameObject.CompareTag("Player"))
      {
          Destroy(this.gameObject);
          other.transform.position = respawnPoint.transform.position;
          
      }           
    }
}