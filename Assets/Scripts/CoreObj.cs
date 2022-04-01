using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class CoreObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (col)
        {
            col.isTrigger = true;
            var trigger = col.gameObject.AddComponent<TriggerObj>();
            trigger.OnTrigger.AddListener(() => {
                StaticObjs.currentPly.canvas.fade.FadeShort(Color.white);
                StaticObjs.currentPly.SetCore(this.CoreID);
                if (OnCore != null)
                {
                    OnCore.Invoke();
                }
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnityEvent OnCore;
    public Collider col;
    public int CoreID;
    //public CoreMode Mode;
}

[Serializable]
public class CoreMode
{
    public CoreMode()
    {

    }

    public CoreMode(string name, int id, Color color, OnCoreCaught onCoreCaught, OnCoreDisabled onCoreDisabled)
    {
        this.ModeName = name;
        this.ID = id;
        this.color = color;
        this.OnCaught.AddListener(() => { if (onCoreCaught != null) onCoreCaught.Invoke(); });
        this.OnDisabled.AddListener(() => { if (onCoreDisabled != null) onCoreDisabled.Invoke(); });
    }

    public delegate void OnCoreCaught();
    public delegate void OnCoreDisabled();

    public string ModeName;
    public int ID;
    public Color color = Color.white;
    public UnityEvent OnCaught = new UnityEvent();
    public UnityEvent OnDisabled = new UnityEvent();
}
