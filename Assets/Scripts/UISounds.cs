using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!sound_ui_submit) sound_ui_submit = Resources.Load<AudioClip>("Assets/Sounds/UI/ui_submit");
        if (!sound_ui_cancel) sound_ui_cancel = Resources.Load<AudioClip>("Assets/Sounds/UI/ui_cancel");
        if (!sound_ui_move) sound_ui_move = Resources.Load<AudioClip>("Assets/Sounds/UI/ui_move");
        if (!source) source = this.GetComponent<AudioSource>();
    }

    public void Submit()
    {
        Debug.Log("Submit");
        if (source && sound_ui_submit)
        {
            source.clip = sound_ui_submit;
            source.Play();
        }

    }

    public void Move()
    {
        Debug.Log("Move");
        if (source && sound_ui_move)
        {
            source.clip = sound_ui_move;
            source.Play();
        }

    }

    public void Cancel()
    {
        Debug.Log("Cancel");
        if (source && sound_ui_cancel)
        {
            source.clip = sound_ui_cancel;
            source.Play();
        }

    }

    public AudioSource source;

    public AudioClip sound_ui_submit;
    public AudioClip sound_ui_cancel;
    public AudioClip sound_ui_move;
}
