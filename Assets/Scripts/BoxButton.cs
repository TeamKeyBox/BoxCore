using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!anim) anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pressed)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Press") || anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Pressed = false;
                if (IsOnce)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Press()
    {
        anim.Play("Press");
        Pressed = true;
        OnPressed.Invoke();
    }

    public Animator anim;
    public bool IsOnce;
    public bool Pressed;

    public UnityEvent OnPressed;
}
