using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreCleaner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (col)
        {
            col.isTrigger = true;
            var trigger = col.gameObject.AddComponent<TriggerObj>();
            trigger.OnTrigger.AddListener(() => {
                
                var core = StaticObjs.currentPly.CurrentCore;
                if (!OnlyScan) StaticObjs.currentPly.DisableCore();
                if (OnPast != null)
                {
                    OnPast.Invoke();
                }
                if (OnCleaned != null && core != 0)
                {
                    StaticObjs.currentPly.canvas.fade.FadeSafely(Color.black, 1, 0.25f);
                    OnCleaned.Invoke();
                }
            });
        }
    }

    public Collider col;
    public bool OnlyScan;
    public UnityEvent OnCleaned;
    public UnityEvent OnPast;
}
