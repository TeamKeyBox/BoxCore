using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJolt.UI;
using GameJolt.API;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaticObjs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (uisound) uisound.playOnAwake = false;
        if (yesbtn) yesbtn.gameObject.SetActive(false);
        if (nobtn) nobtn.gameObject.SetActive(false);
        if (mestext) mestext.gameObject.SetActive(false);
        if (OnStarted != null) OnStarted.Invoke();
        sound_ui_submit = Resources.Load<AudioClip>("Assets/Sounds/UI/ui_submit");
        sound_ui_cancel = Resources.Load<AudioClip>("Assets/Sounds/UI/ui_cancel");
        sound_ui_move = Resources.Load<AudioClip>("Assets/Sounds/UI/ui_move");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            var now = DateTime.Now;
            ScreenCapture.CaptureScreenshot("scrshot_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + now.Minute + "_" + now.Second + "_" + now.Millisecond + ".png");
            Debug.Log("ScreenShot Captured!");
        }
    }

    public void ShowLogin()
    {
        if (GameJoltAPI.Instance.HasSignedInUser)
        {
            ShowConfirm("現在\"" + GameJoltAPI.Instance.CurrentUser.Name + "\"でログインしています。\nログアウトしますか?", "はい", "いいえ");
        }
        else
        {
            GameJoltUI.Instance.ShowSignIn(
                (bool signInSuccess) => {
                    Debug.Log(string.Format("Sign-in {0}", signInSuccess ? "successful" : "failed or user's dismissed the window"));
                },
                (bool userFetchedSuccess) => {
                    Debug.Log(string.Format("User details fetched {0}", userFetchedSuccess ? "successfully" : "failed"));
                    PlaySound(sound_ui_submit);
                    LoadScene("Title");
                }
            );
        }
    }

    public void ShowConfirm(string message, string yes, string no)
    {
        if (mestext)
        {
            mestext.gameObject.SetActive(true);
            mestext.text = message;
        }
        if (yesbtn)
        {
            yesbtn.gameObject.SetActive(false);
            var txt = yesbtn.GetComponentInChildren<Text>();
            if (txt)
            {
                txt.text = yes;
            }
        }
        if (nobtn)
        {
            nobtn.gameObject.SetActive(false);
            var txt = nobtn.GetComponentInChildren<Text>();
            if (txt)
            {
                txt.text = no;
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (uisound && clip)
        {
            uisound.clip = clip;
            uisound.Play();
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public AudioSource uisound;
    public Text mestext;
    public Button yesbtn;
    public Button nobtn;

    public UnityEvent OnStarted;
    public static AudioClip sound_ui_submit;
    public static AudioClip sound_ui_cancel;
    public static AudioClip sound_ui_move;

}
