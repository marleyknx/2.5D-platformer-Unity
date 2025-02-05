using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator _anim;
    public static readonly int SpeedASH = Animator.StringToHash("VelocityX");
    public static readonly int AttackAsh = Animator.StringToHash("Attack");
    public static readonly int AttackChoiceAsh = Animator.StringToHash("ChoixAttack");

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    // Met à jour l'animation en fonction de la vitesse horizontale
    public void UpdateSpeedAnimation(float speed)
    {
        _anim.SetFloat(SpeedASH, Mathf.Abs(speed));
    }

    public void SetAttack()
    {
        _anim.SetTrigger(AttackAsh);
    }

    public void SetChoiceAttack()
    {
        _anim.SetInteger(AttackChoiceAsh, Random.Range(1, 3));
    }
}
