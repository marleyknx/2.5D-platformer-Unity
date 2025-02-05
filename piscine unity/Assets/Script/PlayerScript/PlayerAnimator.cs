using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator _anim;
    public static readonly int SpeedASH = Animator.StringToHash("Speed");

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    // Met à jour l'animation en fonction de la vitesse horizontale
    public void UpdateSpeedAnimation(float speed)
    {
        _anim.SetFloat(SpeedASH, Mathf.Abs(speed));
    }

    // Active ou désactive l'état de saut
    public void SetJumping()
    {
        _anim.SetTrigger("Jump");
    }

   
    public void SetIsHold(bool isHold)
    {
        _anim.SetBool("IsHold", isHold);
    }

    public void SetAttack()
    {
        _anim.SetTrigger("AttackCAC");
    }
    public void SetAttack2(bool attack)
    {
        _anim.SetBool("AttackCAC2", attack);
    }

    public void SetAttack3(bool attack)
    {
        _anim.SetBool("AttackCAC3", attack);
    }

    public void SetIsCharged()
    {
        _anim.SetTrigger("IsCharged");
    }
}
