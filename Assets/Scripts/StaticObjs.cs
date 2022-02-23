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
        var isfirst = isfirstlogin;
        isfirstlogin = false;
        if (GameJoltAPI.Instance.HasSignedInUser || gamejolt_AutoLogin)
        {
            ShowConfirm("現在\"" + GameJoltAPI.Instance.CurrentUser.Name + "\"でログインしています。\nログアウトしますか?", "はい", "いいえ", (bool yes) => {
                if (yes && GameJoltAPI.Instance.CurrentUser != null)
                {
                    GameJoltAPI.Instance.CurrentUser.SignOut();
                }

                PlaySound(sound_ui_submit);
                if (isfirst) LoadScene("Title"); else LoadScene("Settings");
            });
        }
        else
        {
            if (!Application.isEditor)
            {
                GameJoltUI.Instance.ShowSignIn(
                    (bool signInSuccess) => {
                        Debug.Log(string.Format("Sign-in {0}", signInSuccess ? "successful" : "failed or user's dismissed the window"));
                    },
                    (bool userFetchedSuccess) => {
                        Debug.Log(string.Format("User details fetched {0}", userFetchedSuccess ? "successfully" : "failed"));
                        PlaySound(sound_ui_submit);
                        if (isfirst) LoadScene("Title"); else LoadScene("Settings");
                    }
                );
            }
        }
    }

    private bool isfirstlogin;

    public void URL(string url)
    {
        Application.OpenURL(url);
    }

    private static bool gamejolt_AutoLogin;

    public void GameJolt_IsAutoLogin(bool auto)
    {
        gamejolt_AutoLogin = auto;
        ShowLogin();
    }

    public delegate void ConfirmDialogEvent(bool yes);

    public void ShowConfirm(string message, string yes, string no, ConfirmDialogEvent on)
    {
        if (mestext)
        {
            mestext.gameObject.SetActive(true);
            mestext.text = message;
        }
        if (yesbtn)
        {
            yesbtn.gameObject.SetActive(true);
            var txt = yesbtn.GetComponentInChildren<Text>();
            if (txt)
            {
                txt.text = yes;
            }
            yesbtn.onClick.AddListener(() =>
            {
                if (on != null) on.Invoke(true);
                yesbtn.gameObject.SetActive(false);
                yesbtn.onClick.RemoveAllListeners();
                if (nobtn)
                {
                    nobtn.gameObject.SetActive(false);
                    nobtn.onClick.RemoveAllListeners();
                }
                if (mestext)
                {
                    mestext.gameObject.SetActive(false);
                }
            });
        }
        if (nobtn)
        {
            nobtn.gameObject.SetActive(true);
            var txt = nobtn.GetComponentInChildren<Text>();
            if (txt)
            {
                txt.text = no;
            }
            nobtn.onClick.AddListener(() =>
            {
                if (on != null) on.Invoke(false);
                nobtn.gameObject.SetActive(false);
                nobtn.onClick.RemoveAllListeners();
                if (yesbtn)
                {
                    yesbtn.gameObject.SetActive(false);
                    yesbtn.onClick.RemoveAllListeners();
                }
                if (mestext)
                {
                    mestext.gameObject.SetActive(false);
                }
            });
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

    public void ExitApp()
    {
        Application.Quit();
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
