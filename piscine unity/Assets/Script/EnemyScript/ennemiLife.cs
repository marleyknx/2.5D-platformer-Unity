using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemiLife : MonoBehaviour
{
    public float vie;
    private float vieMax;
    private Animator anim;
    private ennemi scriptSalaud;

    public float knockBackForce, kbCounter, kbTotalTime;
  public  bool isKnockRight;

    void Start() {
        vieMax = vie;
        anim = transform.GetChild(0).GetComponent<Animator>();
        scriptSalaud = GetComponent<ennemi>();
    }
    
    
    public void takeDamage(float damage) {
        vie = vie - damage;
        if (vie > 0) {
            anim.SetTrigger("Hit");
            KnockBack();
            scriptSalaud.enabled = false;
            StartCoroutine(ouille());
        } else {
            anim.Play("Die");
            scriptSalaud.enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            
            Destroy(gameObject, 2f);
        }
    }

    void KnockBack()
    {
        if(isKnockRight)
            GetComponent<Rigidbody>().linearVelocity = new Vector3(-knockBackForce,0,0);
        if(!isKnockRight)
            GetComponent<Rigidbody>().linearVelocity = new Vector3(knockBackForce, 0, 0);
    }

    IEnumerator ouille() {
        yield return new WaitForSeconds(1.2f);
        scriptSalaud.enabled = true;
    }
    
}
