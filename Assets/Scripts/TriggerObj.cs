using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log("Trigger Enterd");
            if (OnTrigger != null) OnTrigger.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (OnExit != null) OnExit.Invoke();
        }
    }

    public UnityEvent OnTrigger = new UnityEvent();
    public UnityEvent OnExit;
}
