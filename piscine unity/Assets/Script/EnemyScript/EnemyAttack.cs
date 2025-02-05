using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Attack
{
    public Vector3 triggerCenter;
    public Vector3 triggerSize;
    public bool activation;
    private Collider[] collider;
    public Transform enemyHand;
    public Mesh meshCube;
   // PlayerLife life;
    public float damage = 3f;
   
  

    private void Start()
    {
     

    }

    // Update is called once per frame
    void Update()
    {
        if (activation)
        {
            collider = Physics.OverlapBox(enemyHand.position + enemyHand.up * 0.5f,  triggerSize, enemyHand.rotation);
            Damage();
        }
    }

 
    public void Damage()
    {
        foreach (Collider col in collider)
        {
            if (col.tag == "Player")
            {

               // life.TakeDamage(damage);
                col.gameObject.GetComponentInChildren<Animator>().SetTrigger("Hit");
                activation = false;


                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireMesh(meshCube,enemyHand.position + enemyHand.up * 0.5f, enemyHand.rotation, triggerSize * 2);
    }
}
