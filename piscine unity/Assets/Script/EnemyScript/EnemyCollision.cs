using System;
using System.Collections;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollision : MonoBehaviour , IPatrol
{
    Rigidbody rb;

    private Vector3 limiteGauchePosition;
    private Vector3 limiteDroitePosition;
    [SerializeField, Range(0.1f, 50f)] public float limiteDroite = 1f;
    [SerializeField, Range(0.1f, 50f)] public float limiteGauche = 1f;

    [Header("Detector")]
    public LayerMask mask;
    private RaycastHit hitVision;
    [SerializeField] float _maxDistance;
    public Transform Body;
    [Header("Chase")]
    Transform target;
    [SerializeField] float _chaseTime;
    [Header("AttackRange")]
    [SerializeField] float _maxAttackDistance;
   public bool _canAttack = false;
   [SerializeField] Collider monColl;
   public Transform _flipCheck;
    

    bool _canTurn = true;
    float _waitToTurn = 0.5f;

    public bool _isAlert = false;

    public float direction = 1f;

    private void Awake()
    {
        monColl = GetComponent<Collider>();
    }
    void Start()
    {
        limiteDroitePosition = transform.position + new Vector3(limiteDroite, 0, 0);
        limiteGauchePosition = transform.position - new Vector3(limiteGauche, 0, 0);
    }

    private void Update()
    {
    }
    private void FixedUpdate()
    {
        PlayerDetector();
        FlipDetetctor();
        
    }

    public void FlipToTarget( float direction)
    {
        if (target.position.x < transform.position.x && _canTurn)
        {
            direction = -1f;
            _canTurn = false;
            StartCoroutine(TurnRoutine());
        }
        if (target.position.x > transform.position.x && _canTurn)
        {
            direction = 1f;
            _canTurn = false;
            StartCoroutine(TurnRoutine());
        }
    }
    IEnumerator TurnRoutine()
    {
        yield return new WaitForSeconds(_waitToTurn);
        _canTurn = true;


    }


    public void Chase(float speedMultiplier)
    {
       
        _canAttack = Vector3.Distance(target.position, transform.position) < 2.5f;
        if (!_canAttack)
        {
            speedMultiplier = 2;

        }else if (_canAttack)
         {
            speedMultiplier = 0;
           

        }
      
    }



    void FlipDetetctor()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(_flipCheck.position, Vector3.down, monColl.bounds.extents.y * 1.1f);
        if (hits.Length == 0)
        {

            direction = -direction;
        }
       
        // le fait changer de sens si flip
          hits = Physics.RaycastAll(transform.position, Vector3.right * direction, monColl.bounds.extents.x * 1.1f);
        foreach (RaycastHit truc in hits)
        {

            if (truc.collider != monColl && truc.transform.tag != "Player" && !truc.collider.isTrigger)
            {
                direction = -direction;
            }
        }
        
    }
    public void Patrol()
    {
        //Si il dépasse sa limite Droite, il se retourne
        if (transform.position.x > limiteDroitePosition.x)
        {
            direction = -1f;
        }

        //Si il dépasse sa limite gauche, il se retourne
        if (transform.position.x < limiteGauchePosition.x)
        {
            direction = 1f;
        }
    }

    private void PlayerDetector()
    {
       

        if(Physics.Raycast(Body.position, -Vector3.left * direction, out hitVision , _maxDistance,mask )) {
            Debug.DrawRay(Body.position, -Vector3.left * direction * hitVision.distance, Color.red);
            _isAlert = true;

            if (hitVision.collider.tag == "Player")
            {
               
             
            target = hitVision.collider.transform;

            }
           
        }
        else
        {
            Debug.DrawRay(Body.position, -Vector3.left * direction * _maxDistance, Color.blue);
           
               StartCoroutine(StopChaseRoutine());
           
            
        }
      
    }
    IEnumerator StopChaseRoutine()
    {
        yield return new WaitForSeconds(_chaseTime);
             print("je patrol");
        _isAlert = false;
    }

   

    void OnDrawGizmos()
    {
        #region Patrol
        // permet d'ajuster/ bouger les position sans lancer l'application
        if (!Application.IsPlaying(gameObject))
        {
            limiteDroitePosition = transform.position + new Vector3(limiteDroite, 0, 0);
            limiteGauchePosition = transform.position - new Vector3(limiteGauche, 0, 0);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(limiteDroitePosition, new Vector3(0.2f, 1, 0.2f));
        Gizmos.DrawCube(limiteGauchePosition, new Vector3(0.2f, 1, 0.2f));
        Gizmos.DrawLine(limiteDroitePosition, limiteGauchePosition);
        #endregion

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_flipCheck.position, new Vector3(_flipCheck.position.x, _flipCheck.position.y - monColl.bounds.extents.y * 1.1f));
    }

   

}
