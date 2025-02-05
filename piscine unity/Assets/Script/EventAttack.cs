using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAttack : MonoBehaviour
{
     private  EnemyAttack attack;

    private void Start()
    {
        attack = transform.parent.GetComponent<EnemyAttack>();
    }
    // Start is called before the first frame update
    public void aparition()
    {
        attack.activation = true;
    }
    public void Disparition()
    {
       attack.activation = false;
    }
}
