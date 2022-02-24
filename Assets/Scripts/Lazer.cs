using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!line) line = GetComponent<LineRenderer>();
        if (line)
        {
            line.positionCount++;
            RaycastHit hit;
            if (Physics.Raycast(line.transform.position, transform.right, out hit))
            {
                line.SetPosition(1, transform.right * hit.distance);
            }
            else
            {
                line.SetPosition(1, transform.right * MaxLength);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (line && !disabled)
        {
            var poss = new Vector3[30];
            line.GetPositions(poss);
            var prevpos = line.transform.position;
            var isfirst = true;
            foreach (var pos in poss)
            {
                //Debug.Log(pos);
                if (isfirst)
                {
                    isfirst = false;
                    prevpos = pos;
                    continue;
                }
                Vector3 dir = (pos - prevpos).normalized;
                //Debug.DrawRay(prevpos, dir, Color.blue);
                RaycastHit[] hits = Physics.RaycastAll(line.transform.position + prevpos, dir, Vector3.Distance(pos, prevpos));
                foreach (var hit in hits)
                {
                    
                    var ply = hit.collider.GetComponentInParent<Player>();
                    if (ply && ply.col == hit.collider)
                    {
                        //Debug.Log("Hit");
                        ply.Damage(10);
                        if (ply.rig)
                        {
                            var vel = -ply.rig.velocity * 0.3f;
                            vel.y = 0;
                            ply.rig.AddForce(vel, ForceMode.VelocityChange);
                        }
                    }
                }
                prevpos = pos;
            }
        }
    }

    private bool disabled;
    public bool Disabled
    {
        set
        {
            disabled = value;
            line.enabled = !value;
        }
        get
        {
            return disabled;
        }
    }
    private const float MaxLength = 999f;

    public LineRenderer line;
}
