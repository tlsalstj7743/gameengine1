using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollide : MonoBehaviour
{
    int counted = 0;
    int destroy_counted = 10;
    float time;
    float check;
    
    bool isaction = false;
    public GameObject effect_prefab;
    public AudioSource audioSource;
    IngameUI ingame;
    private void Start()
    {
        ingame= GameObject.Find("UIManager").GetComponent<IngameUI>();
        audioSource = GetComponent<AudioSource>();
        time = 0.0f;
        check = 2.0f;
      

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("sword"))
        {
            audioSource.Play();
            isaction = true;
            Debug.Log(this.name+counted);
            counted++;
            time = 0;
            Instantiate(effect_prefab, this.transform.position, Quaternion.identity);

            
        }
        if (counted == destroy_counted)
        {
            Destroy(this.gameObject);
            ingame.IncreaseWood();
            Debug.Log("sds");
            counted = 0;
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("sword"))
        {
            isaction = false;
        }

    }

    private void Update()
    {
        if (isaction == true)
            return;

        time += Time.deltaTime;
        if (check <= time)
        {
            counted = 0;
            time = 0;
        }



    }
}
