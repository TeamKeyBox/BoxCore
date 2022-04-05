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
            if ((!IsOnce && !anim.GetCurrentAnimatorStateInfo(0).IsName("Press")) || (anim.GetCurrentAnimatorStateInfo(0).IsName("Press") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1))
            {
                if (timer <= 0) Pressed = false;
                if (IsOnce)
                {
                    Destroy(gameObject);
                }
            }
        }
        if (curtime > 0 && Pressed)
        {
            curtime -= Time.deltaTime;
            if (prevint != Mathf.FloorToInt(curtime))
            {
                if (asource && tickSound)
                {
                    asource.clip = tickSound;
                    asource.Play();
                }
            }
            prevint = Mathf.FloorToInt(curtime);

        }
        else if (curtime <= 0 && Pressed && timer > 0)
        {
            if (OnTimered != null) OnTimered.Invoke();
            Pressed = false;
            anim.Play("Spawn");
            if (IsOnce)
            {
                Destroy(gameObject);
            }
        }
    }

    private int prevint = 0;

    public void Press()
    {
        anim.Play("Press");
        Pressed = true;
        if (OnPressed != null) OnPressed.Invoke();
        curtime = timer;
        if (asource && pressedSound)
        {
            asource.clip = pressedSound;
            asource.Play();
        }
    }

    public Animator anim;
    public bool IsOnce;
    public bool Pressed;
    public float timer;
    private float curtime;
    public AudioSource asource;
    public AudioClip pressedSound;
    public AudioClip tickSound;

    public UnityEvent OnPressed;
    public UnityEvent OnTimered;
}
