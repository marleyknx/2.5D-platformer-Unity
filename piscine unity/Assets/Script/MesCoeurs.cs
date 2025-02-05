using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesCoeurs : MonoBehaviour
{
    public GameObject[] Coeur;
   
    // Start is called before the first frame update
    void Awake()
    {
        Coeur = new GameObject[transform.childCount];
        for(int i = 0;  i< transform.childCount; i++)
        {
            Coeur[i] = transform.GetChild(i).gameObject;
        }


        
    }

    // Update is called once per frame
public void VieUpdate(int vie)
    {
        for (int i = 0; i < transform.childCount; i++)
        {   // en cas de heal
            Coeur[i].SetActive(true);
            // met a jour les coeur

            if (i >= vie)
            {
                Coeur[i].SetActive(false);
            }
        }
    }
}
