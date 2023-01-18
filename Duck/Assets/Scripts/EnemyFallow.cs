using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyFallow : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject enemyLook;
    Animator animator;
    NavMeshAgent navMesh;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        animator.SetBool("Run",true);
        enemyLook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.SetDestination(playerTransform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerTransform.position = new Vector3(3.55f,0,-39);
            transform.position = new Vector3(4.12f,0.1f,9.34f);
            animator.SetBool("Run", false);
            enemyLook.SetActive(true);
            navMesh.SetDestination(new Vector3(4.12f, 0.1f, 9.34f));
            this.enabled = false;
        }
    }
 
}
