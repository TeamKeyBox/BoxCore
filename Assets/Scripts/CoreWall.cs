using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreWall : MonoBehaviour
{
    private void Start()
    {
        StaticObjs.currentPly.OnCoreUpdate += CurrentPly_OnCoreUpdate;
        this.gameObject.SetActive(false);
    }

    private void CurrentPly_OnCoreUpdate(int bc, int ac)
    {
        if (bc == coreID && ac != coreID)
        {
            this.gameObject.SetActive(false);
        }
        if (ac == coreID)
        {
            this.gameObject.SetActive(true);
        }
    }

    public int coreID;
}
