using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!anim) anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        anim.Play("Open");
    }

    public void Close()
    {
        anim.Play("Close");
    }

    public Animator anim;

    public float speed = 1f;
}
