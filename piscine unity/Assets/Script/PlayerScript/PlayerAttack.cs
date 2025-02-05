using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : Attack
{
   
   
    public float attackTime = 0.5f;
    public float _chargeTime = 2f;
    public bool _isCharging = false;

    PlayerAnimator _playerAnimator;
    PlayerInputHandler _playerInputHandler;
    FrameInput _frameInput;

    public float _attackRadius;
    public Transform _attackCheck;
   
   
    public   bool attack1;
    public bool attack2;
    public bool attack3;
    private void Awake()
    {
       
        
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();

    }

    private void OnEnable()
    {
       
    }
    private void OnDisable()
    {
      
    }


    void Update()
    {

        _frameInput = _playerInputHandler.FrameInput;
        if (_frameInput.attack && !_isCharging)
        {
            PerformAttack();
            AttackCollision();
           
        }

        if (_frameInput.attack && !_isCharging)
        {

            StartCoroutine(ChargeAttack());

        }


    }

    public void AttackCollision()
    {
        Collider[] coll = Physics.OverlapSphere(_attackCheck.position, _attackRadius);

        foreach(Collider collider in coll)
        {
            if(collider.gameObject.name == "Enemie")
            {
                print("je tape " + collider.gameObject.name);
                //met les degat et autre 
                break;
            }
        }

            
       
    }

    public void PerformAttack()
    {
        
        if (!attack1 && !attack2 && !attack3)
        {
            
            _playerAnimator.SetAttack();
            StartCoroutine(waitShoot1(attackTime));
        }
        else if (attack1 && !attack2)
        {
           
            StartCoroutine(waitShoot2(attackTime));
        }
        else if (attack2 && !attack3)
        {
           
            StartCoroutine(waitShoot3(attackTime));
        }
    }


   public IEnumerator ChargeAttack()
    {
       
        if (_frameInput.attackUp) yield return null; 
        float startTime = Time.time;
       

            _isCharging = true;
        while (_frameInput.Hold)
        {
            // Arrête la charge si le joueur relâche avant que le temps de charge soit atteint
            if (Time.time - startTime >= _chargeTime)
            {
                print("ischarged");
                TriggerChargedAttack(_playerAnimator);
                _isCharging = false;
                yield break;
            }

            yield return null;

        }
        // Si le joueur relâche avant le temps de charge, la charge est annulée
        _isCharging = false;
    }
    private void TriggerChargedAttack(PlayerAnimator _playerAnimator)
    {
        _playerAnimator.SetIsCharged();
       // ajoute un délai avant que l'attaque suivante soit possible
        StartCoroutine(ResetComboAfterDelay(attackTime));
    }
    IEnumerator ResetComboAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        attack1 = false;
        attack2 = false;
        attack3 = false;
    }
    IEnumerator waitShoot1(float timer)
    {
        attack1 = true;
        yield return new WaitForSeconds(timer);
        attack1 = false;
    }
    IEnumerator waitShoot2(float timer)
    {
        attack2 = true;
        yield return new WaitForSeconds(timer);
        
        attack2 = false;
       
    }
    IEnumerator waitShoot3(float timer)
    {
        attack3 = true;
        yield return new WaitForSeconds(timer);
       
        attack3 = false;
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackCheck.position, _attackRadius);
    }


}

   

