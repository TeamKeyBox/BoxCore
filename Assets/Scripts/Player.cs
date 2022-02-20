using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
                        rig.AddForce(rig.transform.up * 15f * JumpHeight, ForceMode.VelocityChange);
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    MoveTo(-rig.transform.right);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    MoveTo(rig.transform.right);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    MoveTo(rig.transform.forward);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    MoveTo(-rig.transform.forward);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (OnGround())
                    {
                        rig.AddForce(rig.transform.up * 8f * JumpHeight, ForceMode.VelocityChange);
                    }
                }
                {
                    var syuusei = 0f;
                    var syuusei2 = 0f;
                    var tate = Input.GetAxis("Mouse Y") * -10f;
                    var yoko = Input.GetAxis("Mouse X") * this.MouseKando;
                    var eulang = this.FPCam.transform.eulerAngles;
                    if (eulang.x < 280f & eulang.x > 270f & tate < 0f)
                    {
                        tate = 0f;
                    }
                    else if (eulang.x > 80f & eulang.x < 180f & tate > 0f)
                    {
                        tate = 0f;
                    }
                    if (eulang.z > 170f & eulang.z < 190f)
                    {
                        syuusei = eulang.z * -1f;
                    }
                    //syuusei2 = rig.transform.eulerAngles.y - eulang.y;
                    var camdekita = eulang + new Vector3(tate, 0, syuusei);
                    this.FPCam.transform.localEulerAngles = new Vector3(camdekita.x, -90, camdekita.z);
                    rig.transform.eulerAngles = new Vector3(rig.transform.eulerAngles.x, rig.transform.eulerAngles.y + yoko + syuusei2, rig.transform.eulerAngles.z);
                }
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                this.SwitchCam(!isfp);
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
            Cursor.lockState = fp ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !fp;
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
    public float MouseKando = 1f;

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
