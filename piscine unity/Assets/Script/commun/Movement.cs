using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator anim;
    [Header("component")]
    private Rigidbody rb;

    [Header("movement")]
    public float speed = 5f;
    [HideInInspector] public float _speedMultiplier = 0;

    float _xDirection;
   public bool _canMove = true;
   
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        Move();
    }

    //update
    public void SetDirection(float xDirection)
    {
        _xDirection = xDirection;
        //anim.SetFloat("Speed", Mathf.Abs(xDirection))
    }
    //fixedUpdate
    public void Move()
    {
        if (!_canMove) return;
       
            rb.linearVelocity = new Vector3(_xDirection * speed + _speedMultiplier, rb.linearVelocity.y,
                rb.linearVelocity.z);
    }





   
}
