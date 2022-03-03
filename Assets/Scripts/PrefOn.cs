using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrefOn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var str = PlayerPrefs.GetString(key, defStr);
        var inte = PlayerPrefs.GetInt(key, defInt);
        var flo = PlayerPrefs.GetFloat(key, defFloat);
        if (str != defStr && OnString != null)
        {
            OnString.Invoke(str);
        }
        if (inte != defInt && OnInt != null)
        {
            OnInt.Invoke(inte);
        }
        if (flo != defFloat && OnFloat != null)
        {
            OnFloat.Invoke(flo);
        }
    }

    public string key;

    public string defStr = "";
    public int defInt = -1;
    public float defFloat = 0f;
    public UnityEvent<string> OnString;
    public UnityEvent<int> OnInt;
    public UnityEvent<float> OnFloat;
}
