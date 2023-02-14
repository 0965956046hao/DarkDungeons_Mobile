using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.TryGetComponent<EnemiesController>(out EnemiesController enemy))
        {
           
        }
    }
}
