using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTeleport : TriggerObj
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void OnTriggerEnter(Collider other)
    {
        var rig = other.GetComponent<Rigidbody>();
        if (linkedTo && rig && !isin.Contains(rig))
        {
            other.transform.position = linkedTo.transform.position;
            other.transform.rotation = linkedTo.transform.rotation;
            linkedTo.isin.Add(rig);
        }
        base.OnTriggerEnter(other);
    }

    public new void OnTriggerExit(Collider other)
    {
        var rig = other.GetComponent<Rigidbody>();
        if (linkedTo && rig && isin.Contains(rig))
        {
            isin.Remove(rig);
        }
        base.OnTriggerEnter(other);
    }

    public TTeleport linkedTo;
    private List<Rigidbody> isin = new List<Rigidbody>();
}
