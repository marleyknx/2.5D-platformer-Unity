using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class ennemi : MonoBehaviour
{



    EnemyCollision _enemyCollision;
    EnemyAnimator _enemyAnimator;
     Movement _movement;
      Flip _flip;
     public float cooldownAttack = 2f;
     private bool attacking;
   
    



     private float _direction;
     private Rigidbody rb;
     private bool venere;
     public Transform target;
     private CapsuleCollider monColl;
     private Vector3 startRaySol;




     private void Awake()
     {
         _enemyCollision = GetComponent<EnemyCollision>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
         _movement = GetComponent<Movement>();
         _flip = GetComponent<Flip>();
     }
     void Start() {
         rb = GetComponent<Rigidbody>();
        
       
     }


     void Update() {
         _direction = _enemyCollision.direction;
         _movement.SetDirection(_direction);
         _flip.FlipCharacter(_direction);

        if (!_enemyCollision._isAlert) {

            _enemyCollision.Patrol();
            _movement._canMove = true;

        }
        else if (_enemyCollision._isAlert)
        {
                if (target.position.x < transform.position.x)
                {
                    _direction = -1f;
                }
                if (target.position.x > transform.position.x)
                {
                    _direction = 1f;
                }
            _enemyCollision.Chase(_movement._speedMultiplier);
            //_enemyCollision.FlipToTarget(_direction);
            if (_enemyCollision._canAttack)
            {
                rb.linearVelocity = Vector3.zero;
                _movement._canMove = false;
                attacking = true;
                print("j'attack");
                _enemyAnimator.SetAttack();
                _enemyAnimator.SetChoiceAttack();
                pasCalme();
            }
        }

        _enemyAnimator.UpdateSpeedAnimation(_movement.speed);

       
       
    }

     void pasCalme() {

         if (Vector3.Distance(target.position, transform.position) < 2f && !attacking) {
            
           
             StartCoroutine(jmeFaisChier());
         }

       
     }





     

     IEnumerator jmeFaisChier() {
         yield return new WaitForSeconds(cooldownAttack);
         attacking = false;
     }
 
}
