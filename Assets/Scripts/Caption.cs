using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caption : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (prTime > 0)
        {
            prTime -= Time.deltaTime;
        }
        if (Showing && prTime <= 0)
        {
            if (anim)
            {
                anim.Play("captionOut");
            }
            Showing = false;
        }
    }

    public void SetTime(float time)
    {
        this.time = time;
    }

    public void ShowCaption(string str)
    {
        if (text)
        {
            text.text = str;

        }
        if (anim && !Showing)
        {
            anim.Play("captionIn");
        }
        prTime = time;
        Showing = true;
    }

    public bool Showing { get; private set; }
    public Animator anim;
    public Text text;
    public float time;
    private float prTime;
}
