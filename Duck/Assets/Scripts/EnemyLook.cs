using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour
{
    public EnemyFallow enemyFallow;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            enemyFallow.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
