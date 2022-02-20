using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        firstPos = this.transform.localPosition;
        firstAng = this.transform.localRotation;
        if (!rig) rig = GetComponent<Rigidbody>();
        if (!col) col = GetComponent<Collider>();
        if (TDCam)
        {
            TDCam.transform.SetParent(null);
            offset = TDCam.transform.position;
        }
    }

    private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (TDCam)
        {
            TDCam.transform.position = this.transform.position + offset;
        }
        if (rig)
        {
            if (!IsFP)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    rig.transform.eulerAngles = new Vector3(0, 180, 0);
                    MoveTo(-rig.transform.right);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    rig.transform.eulerAngles = new Vector3(0, 0, 0);
                    MoveTo(-rig.transform.right);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (OnGround())
                    {
                        rig.AddForce(rig.transform.up * 10f * JumpHeight, ForceMode.VelocityChange);
                    }
                }
            }
        }
        if (this.transform.position.y < this.DeathPosition)
        {
            if (rig)
            {
                rig.velocity = Vector3.zero;
            }
            this.transform.localPosition = firstPos;
            this.transform.localRotation = firstAng;

        }
    }

    public void SwitchCam(bool fp)
    {
        if (FPCam && TDCam)
        {
            FPCam.enabled = fp;
            TDCam.enabled = !fp;
        }
        isfp = fp;
    }

    public const float MaxSpeed = 12;

    public void MoveTo(Vector3 to)
    {
        if (!rig) return;
        to.y = 0;
        to = to.normalized;
        to = rig.velocity + (to * 3f);
        if (Mathf.Abs(to.x) > MaxSpeed)
        {
            to.x = to.x > 0 ? MaxSpeed : -MaxSpeed;
        }
        if (Mathf.Abs(to.z) > MaxSpeed)
        {
            to.z = to.z > 0 ? MaxSpeed : -MaxSpeed;
        }
        to = to * Speed;
        rig.velocity = new Vector3(to.x, rig.velocity.y, to.z);
    }

    public bool IsFP
    {
        get
        {
            return (!FPCam && isfp) || (FPCam && FPCam.enabled);
        }
        set
        {
            SwitchCam(value);
        }
    }

    private Vector3 firstPos;
    private Quaternion firstAng;
    public float DeathPosition = -100f;
    private bool isfp;
    public Camera FPCam;
    public Camera TDCam;
    public float Speed = 1f;
    public float JumpHeight = 1f;

    public Rigidbody rig;
    public Collider col;
    //private const float jumpbound = 0.5f;

    public bool OnGround()
    {
        if (rig && col)
        {
            var jumpbound = col.bounds.size.x / 2;
            //RaycastHit hit;
            if (Physics.BoxCast(rig.transform.position, new Vector3(jumpbound, 0, jumpbound), -Vector3.up, new Quaternion(), 0.3f))
            {
                //Debug.Log(hit.collider);
                return true;
            }
        }
        return false;
    }
}
