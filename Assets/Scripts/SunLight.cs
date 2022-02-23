using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;

public class SunLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0.2f, 0f, 0f) * Time.deltaTime * Speed);
        if ((this.transform.eulerAngles.x > 175 && this.transform.eulerAngles.x < 180) || (this.transform.eulerAngles.x > -180 && this.transform.eulerAngles.x < -170))
        {
            foreach (var light in lights)
            {
                light.intensity -= Time.deltaTime * Speed;
            }
        }
    }
    //175 - 190

    public List<Light> lights;
    public float Speed = 1f;
    public bool InGame;
}
