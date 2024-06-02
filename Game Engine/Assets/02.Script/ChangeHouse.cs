using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHouse : MonoBehaviour
{
   
    IngameUI ingame;
 
    public GameObject house;
    private void Start()
    {
        ingame = GameObject.Find("UIManager").GetComponent<IngameUI>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            BuildHouse();
        }
    }

    public void BuildHouse()
    {
        if (ingame.c_leather >= 5 && ingame.c_rock >= 5 && ingame.c_wood >= 5)
        {
            house.gameObject.SetActive(true);

            Destroy(this.gameObject);
            ingame.c_wood = 0;
            ingame.c_rock = 0;
            ingame.c_leather = 0;
        }

      
    }

}
