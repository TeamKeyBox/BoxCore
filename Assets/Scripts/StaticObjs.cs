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
        if (first && OnFirst != null) OnFirst.Invoke();
        sound_ui_submit = Resources.Load<AudioClip>("Assets/Sounds/UI/ui_submit");
        sound_ui_cancel = Resources.Load<AudioClip>("Assets/Sounds/UI/ui_cancel");
        sound_ui_move = Resources.Load<AudioClip>("Assets/Sounds/UI/ui_move");
        if (first)
        {
            if (PlayerPrefs.HasKey("screen_resolution"))
            {
                SetResolution(PlayerPrefs.GetInt("screen_resolution", 0));
            }
            if (PlayerPrefs.HasKey("cam_fp"))
            {
                SetDefaultCam(PlayerPrefs.GetInt("cam_fp", 0));
            }
        }
        first = false;
        if (OnGameJolt != null && GameJoltAPI.Instance)
        {
            if (GameJoltAPI.Instance.HasSignedInUser)
            {
                OnGameJolt.Invoke(GameJoltAPI.Instance.CurrentUser.Name);
            }
        }
    }

    private static bool first = true;

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
        if (GameJoltAPI.Instance.HasSignedInUser || gamejolt_AutoLogin)
        {
            ShowConfirm("現在\"" + GameJoltAPI.Instance.CurrentUser.Name + "\"でログインしています。\nログアウトしますか?", "はい", "いいえ", (bool yes) => {
                if (yes && GameJoltAPI.Instance.CurrentUser != null)
                {
                    GameJoltAPI.Instance.CurrentUser.SignOut();
                }

                PlaySound(sound_ui_submit);
                if (isfirst) {
                    isfirstlogin = false;
                    LoadScene("Title");
                }
                else
                {
                    LoadScene("Settings");
                }
            });
        }
        else
        {
            if (!GameJoltAPI.Instance.Settings.DebugAutoConnect || !Application.isEditor)
            {
                GameJoltUI.Instance.ShowSignIn(
                    (bool signInSuccess) => {
                        Debug.Log(string.Format("Sign-in {0}", signInSuccess ? "successful" : "failed or user's dismissed the window"));
			PlaySound(sound_ui_submit);
                	if (isfirst) {
                	    isfirstlogin = false;
                	    LoadScene("Title");
                	}
                	else
                	{
                	    LoadScene("Settings");
                	}
                    },
                    (bool userFetchedSuccess) => {
                        Debug.Log(string.Format("User details fetched {0}", userFetchedSuccess ? "successfully" : "failed"));
                    }
                );
            }
        }
    }

    private static bool isfirstlogin = true;

    public void URL(string url)
    {
        Application.OpenURL(url);
    }

    private static bool gamejolt_AutoLogin;

    public void GameJolt_IsAutoLogin(bool auto)
    {
        gamejolt_AutoLogin = auto;
        isfirstlogin = true;
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

    public void SetResolution(int choose)
    {
        switch (choose)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.SetResolution(1280, 720, false);
                break;
            case 2:
                Screen.SetResolution(1280, 780, false);
                break;
            default:
                break;
        }
        PlayerPrefs.SetInt("screen_resolution", choose);
    }

    public void SetDefaultCam(int fp)
    {
        default_fp = fp == 1;
        PlayerPrefs.SetInt("cam_fp", default_fp ? 1 : 0);
    }

    public static bool default_fp { private set; get; }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void SetPaused(bool paused)
    {
        if (currentPly)
        {
            currentPly.SetPause(paused);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetTrophies()
    {
        Trophies.Remove(157702);
    }

    public AudioSource uisound;
    public Text mestext;
    public Button yesbtn;
    public Button nobtn;

    public UnityEvent OnStarted;
    public UnityEvent OnFirst;
    public UnityEvent<string> OnGameJolt;
    public static AudioClip sound_ui_submit;
    public static AudioClip sound_ui_cancel;
    public static AudioClip sound_ui_move;

    public static Player currentPly;
}
