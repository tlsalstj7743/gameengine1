using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class UI_Manager : MonoBehaviour
{   
    public TMP_InputField Name_Input;
    public TMP_InputField Password_Input;
    public GameObject Panel;
    public TMP_Text tmp1;
     

    bool is_correct_id = false;
    bool is_correct_ps = false;
  


    
    public void Alarm_false()
    {   
        
        Panel.SetActive(false);
              
    }

    public void Next_Scene()
    {
        SceneManager.LoadScene("IngameScene");
    }


    public void UI_Input_Name_Check()
    {
        if (Name_Input.text == "Minseo")
        {
            is_correct_id = true;
        }
        else
        { 
            is_correct_id = false;
        }
       
    }
    public void UI_Input_Password_Check()
    {
        if (Password_Input.text == "6785")
        {
            is_correct_ps = true;
        }
        else
        { 
            is_correct_ps = false; 
        }


    }
    
    public void Login()
    {


        if (is_correct_id == true && is_correct_ps == true)
        {
            
            Panel.SetActive(true);
            tmp1.text = "Loading...";
            Invoke("Alarm_false", 2.0f);
            Invoke("Next_Scene", 2.0f);
           
            
            
        }
        else
        {

            Panel.SetActive(true); 
            tmp1.text = "Login failed";
            Invoke("Alarm_false", 2.0f);

        }
    }


}
