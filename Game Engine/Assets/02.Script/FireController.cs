using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule mainModule;
    IngameUI ingame;
  
    public float particlesize;

    void Start()
    {
        
        ingame = GameObject.Find("UIManager").GetComponent<IngameUI>();
        particleSystem = GetComponent<ParticleSystem>();
        mainModule = particleSystem.main;
        StartCoroutine(ReduceParticleSize());
    }
    private void Update()
    {
        particlesize = particleSystem.main.startSize.constant;
    }
    IEnumerator ReduceParticleSize()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            mainModule.startSize = Mathf.Max(mainModule.startSize.constant - 0.05f, 0f);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ingame.c_wood > 0)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("fire");
                mainModule.startSize = Mathf.Max(mainModule.startSize.constant + 0.05f, 0.6f);
                ingame.c_wood--;

            }
        }
    }
}