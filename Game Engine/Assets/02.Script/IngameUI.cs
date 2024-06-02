using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameUI : MonoBehaviour
{
 
    public int c_rock, c_leather, c_wood;
    public TMP_Text wood;
    public TMP_Text leather;
    public TMP_Text rock;
    public TMP_Text fire;
    FireController fireController;
    ChangeHouse changeHouse;
    public GameObject guide_panel;


    private void Start()
    {   
        changeHouse = GameObject.Find("Tent").GetComponent<ChangeHouse>();
        fireController = GameObject.Find("Fire").GetComponent<FireController>();
        c_rock = 0;
        c_leather = 0;
        c_wood = 0;
        SetGuidePanel();
        
    }

    private void Update()
    {
        wood.text = "X"+c_wood.ToString();
        leather.text ="X"+ c_leather.ToString();
        rock.text = "X" + c_rock.ToString();
        fire.text = fireController.particlesize.ToString("F2");
    }

    public void SetGuidePanel()
    {
        Destroy(guide_panel, 20);
    }

   public void IncreaseWood()
    {
        c_wood++;        
    }
    public void IncreaseRock()
    {
        c_rock++;
    }
    public void IncreaseLeather()
    {
        c_leather++;
    }
}
