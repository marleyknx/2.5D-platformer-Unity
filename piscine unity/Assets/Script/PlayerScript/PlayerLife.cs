using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{

    public float maVie = 10f;
    private float maxLife ;
    public Slider healthBar;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
     
        maxLife = maVie;
        //  healthBar.value = maVie / maxLife;
    }

    public void TakeDamage(float Damage)
    {
        maVie -= Damage;
        healthBar.value = maVie / maxLife;
        if (maVie <= 0)
        {
            anim.SetTrigger("Die");
            
            Destroy(gameObject, 2f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void TakeHeal(float heal)
    {
        maVie = maVie + heal;
        healthBar.value = maVie / maxLife;
    }

    public void maxlife(float bonus)
    {
        maxLife = maxLife + bonus;
        maVie = maVie + bonus;
        healthBar.value = maVie / maxLife;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
