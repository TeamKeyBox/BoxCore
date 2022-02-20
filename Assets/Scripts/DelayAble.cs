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

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= time)
            {
                onDelayed.Invoke();
                Destroy(this);
            }
            else
            {
                if (maxtime <= anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
                {
                    maxtime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                }
                else
                {
                    onDelayed.Invoke();
                    Destroy(this);
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

    private float maxtime;

    public float time;
    public Animator anim;
    public UnityEvent onDelayed;
}
