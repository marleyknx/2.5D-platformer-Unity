using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerComboSystem : MonoBehaviour {

     PlayerInputHandler playerInputHandler;
    FrameInput _frameInput;
    InputAction AttackAction,ChargeAttackAction;
    public float attackRadius;


    int _comboCounter; // compte les coups donnée
    float lastTimeAttacked; // last time attacked
    float ComboWindow = 2; // numb of attack 



    private Collider2D[] target;
    private Transform skin;
    private Animator anim;

    public float degats = 1f;
    public Vector2 centreAttack;
    public float attackRayon;
    public float attackTime;

     PlayerMovement CharacterMovement;
    private Collider[] trucBaffe;
    private Vector3 attackPoint;
    [Header("flashEffect")]
    public MeshRenderer renderer;
   public  Material emissiveMaterial;
   
    public float FlashTime = 0f;
    [Range(0, 1)]
    public float ColorIntensity = 1;
    bool recharger;
    private void Awake()
    {
       // AttackAction = playerInput.actions["Attack"];
      //  ChargeAttackAction = playerInput.actions["ChargeAttack"];
        emissiveMaterial = renderer.GetComponent<Renderer>().material;
        
    }

    void Start() {

        anim = GetComponent<Animator>();
        CharacterMovement = GetComponent<PlayerMovement>();
        playerInputHandler = GetComponent<PlayerInputHandler>();


    }

    void Update()
    {

        _frameInput = playerInputHandler.FrameInput;



        TimeCombo();

        if (_frameInput.attack)
        {
            if (_comboCounter <= 2) // Limiter le combo à 3 attaques
            {
                anim.SetBool("Attack", true);
                anim.SetInteger("ComboCounter", _comboCounter);
            }

        }
        if (_frameInput.attackUp)
        {
            // Passer à l'attaque suivante
            _comboCounter++;
            lastTimeAttacked = Time.time;

            // Si on atteint la fin du combo, revenir au début
            if (_comboCounter > 2)
            {
                ResetCombo();
            }
        }



        if (_frameInput.attackUp)
        {
            // Passer à l'attaque suivante
            _comboCounter++;
            lastTimeAttacked = Time.time;

            // Si on atteint la fin du combo, revenir au début
            if (_comboCounter > 2)
            {
                ResetCombo();
            }
        }
    }

    private void TimeCombo()
    {
        // Réinitialiser le combo si la fenêtre est dépassée
        if (_comboCounter > 2 || Time.time >= lastTimeAttacked + ComboWindow)
        {
            ResetCombo();
        }
    }
    void ResetCombo()
        {
            _comboCounter = 0;
            anim.SetBool("Attack", false);
            anim.SetInteger("ComboCounter", 0);
        }
            public void PAF()
    {
       
        if (transform.eulerAngles.y < 180)
        {
            attackPoint = transform.position + (Vector3)centreAttack;
           
        }
        else if (transform.eulerAngles.y > 180)
        {
             attackPoint = transform.position + new Vector3(-centreAttack.x, centreAttack.y, 0);
           
        }

        trucBaffe = Physics.OverlapSphere(attackPoint, attackRayon);
        foreach (Collider truc in trucBaffe)
        {
            if (truc.tag == "enemy")
            {
                truc.SendMessage("takeDamage", degats);
               // CameraShake.Instance.ShakeCamera(0.3f, 0.2f);

            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        attackPoint = transform.position + (Vector3)centreAttack;
        Gizmos.DrawWireSphere(attackPoint, attackRayon);
    }

    public void TurnEmissiveOff()
    {
        // ColorIntensity = 0;
        emissiveMaterial.DisableKeyword("_EMISSION");
        emissiveMaterial.SetColor("_EmissionColor", Color.white * ColorIntensity);

    }

    public void TurnEmissiveOn()
    {
        // ColorIntensity = 1;
        emissiveMaterial.EnableKeyword("_EMISSION");
        emissiveMaterial.SetColor("_EmissionColor", Color.white * ColorIntensity);


    }



  
}
