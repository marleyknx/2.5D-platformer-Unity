using System.Collections;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public Transform flipTarget;
    
    [SerializeField] float _rotationSpeed;
    float _rotateMultiplier = 10;

   

    // est utilisée par le player et l'ennemie
    public void FlipCharacter(float xDirection)
    {
        if (xDirection == 0) return; 
        Quaternion.RotateTowards(flipTarget.rotation, flipTarget.rotation = Quaternion.Euler(0, xDirection *90, 0),
                _rotationSpeed * _rotateMultiplier * Time.deltaTime)  ;
       
    }


  
}
