using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{

    void Update()
    {
        if (img)
        {
            if (time >= 0f)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, time / maxtime);
                time -= Time.deltaTime;
            }
            if (time < 0f)
            {
                time = 0;
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
                this.gameObject.SetActive(false);
            }
            
        }

    }

    private float maxtime;
    private float time;
    public Image img;
    
    public void FadeCustom(Color col, float time)
    {
        if (img)
        {
            img.color = col;
            this.time = time;
            maxtime = time;
            this.gameObject.SetActive(true);
        }
    }

    public void FadeShort(Color col)
    {
        FadeCustom(col, 1f);
    }

    public void FadeSafely(Color col, float time, float startTime)
    {
        FadeCustom(col, time);
        this.time = startTime;
    }
}
