using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (hithantei)
        {
            var trigger = hithantei.gameObject.AddComponent<TriggerObj>();
            trigger.OnTrigger = new UnityEngine.Events.UnityEvent();
            trigger.OnTrigger.AddListener(() =>
            {
                if (!disabled) Goal();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Goal()
    {
        if (fade)
        {
            fade.gameObject.SetActive(true);
            fade.Play("FadeIn");
            var da = fade.gameObject.AddComponent<DelayAble>();
            da.anim = fade;
            da.time = 1;
            da.onDelayed = new UnityEngine.Events.UnityEvent();
            da.TargetState = "FadeIn";
            da.onDelayed.AddListener(() => {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene(sceneTo);
            });
        }
    }

    public bool disabled;
    public Animator fade;
    public Collider hithantei;

    public string sceneTo;
}
