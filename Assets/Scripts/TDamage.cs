using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDamage : TriggerObj
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            var ply = other.GetComponentInParent<Player>();
            ply.Damage(amount);

            //if (OnTrigger != null) OnTrigger.Invoke();
        }
        base.OnTriggerEnter(other);
    }

    public float amount;
}
