using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump")]
    public float jumpforce = 7f;
    Vector3 _jump;
    Rigidbody rb;
  public  bool isJumping;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }
   
     
    public void ExecuteJump(bool isGrounded)
    {
        if(rb == null) return;
        if (isGrounded)
        {

            
            _jump = new Vector3(rb.linearVelocity.x, jumpforce, 0f);
            rb.linearVelocity = _jump;
        }
       
        
    }

  

  
}
