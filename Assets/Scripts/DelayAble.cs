using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayAble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if (anim) anim = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim)
        {
            //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (stateninatta)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= time)
                {
                    //Debug.Log("Test");
                    onDelayed.Invoke();
                    Destroy(this);
                }
                else
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName(TargetState))
                    {
                        //maxtime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    }
                    else
                    {
                        //Debug.Log("Test2");
                        onDelayed.Invoke();
                        Destroy(this);
                    }
                }
            }
            else
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(TargetState))
                {
                    stateninatta = true;
                }
            }
        }
        else
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                onDelayed.Invoke();
                Destroy(this);
            }
        }
    }

    private bool stateninatta;
    public string TargetState;
    private float maxtime;
    public float time;
    public Animator anim;
    public UnityEvent onDelayed;
}
