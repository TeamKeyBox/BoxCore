using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJolt.API;
using GameJolt.API.Objects;
using UnityEngine.UI;

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
            offset = TDCam.transform.localPosition;
            TDCam.transform.SetParent(null);
            
            pubOffset = offset;
        }
        this.Health = 100;
        StaticObjs.currentPly = this;

        if (coreTypes.Count <= 0)
        {
            coreTypes.Add(1, new CoreMode("Speed", 1, Color.blue,() => { this.Speed = 2; },() => { this.Speed = 1; }));
            coreTypes.Add(2, new CoreMode("Place", 2, Color.green, () => { CoreValue1 = 1; }, () => {}));
        }

        if (StaticObjs.default_fp)
        {
            SwitchCam(StaticObjs.default_fp);
        }
    }

    private void Awake()
    {
        if (postProcessssss)
        {
            postProcessssss.transform.SetParent(null);
            postProcessssss = null;
        }
    }

    public void SetCore(int id)
    {
        CoreMode core = null;
        bool suc = coreTypes.TryGetValue(this.CurrentCore, out core);
        //Debug.Log("a" + suc);
        if (suc)
        {
            if (core.OnDisabled != null) core.OnDisabled.Invoke();
        }
        this.CurrentCore = id;
        suc = coreTypes.TryGetValue(id, out core);
        //Debug.Log("b" + suc);
        if (suc)
        {
            if (core.OnCaught != null) core.OnCaught.Invoke();
            if (canvas.coreEffect)
            {
                canvas.coreEffect.gameObject.SetActive(true);
                canvas.coreEffect.color = new Color(core.color.r, core.color.g, core.color.b, 0.2f);
            }
        }
        else
        {
            if (canvas.coreEffect)
            {
                canvas.coreEffect.gameObject.SetActive(false);
            }
        }
    }

    public void DisableCore()
    {
        SetCore(0);
    }

    public int CurrentCore { get; private set; }

    public int CoreValue1 { get; private set; }

    public Vector3 pubOffset;
    private Vector3 offset;

    public void SetCameraOffsetZ(float ofs)
    {
        pubOffset.z = ofs;
    }

    public void ResetCameraOffset()
    {
        pubOffset = offset;
    }

    public void Damage(float amount)
    {
        if (damagetime > 0 || Controlable) return;
        this.Health -= amount;
        damagetime = 0.2f;
        if (this.canvas.fade)
        {
            this.canvas.fade.FadeSafely(Color.red, 2f, 0.5f);
        }
    }

    private float damagetime;

    public void SetPause(bool paused)
    {
        Controlable = paused;
        Cursor.lockState = Controlable && IsFP ? CursorLockMode.Locked : CursorLockMode.None;
        if (canvas.pauseScreen)
        {
            canvas.pauseScreen.SetActive(!Controlable);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (TDCam)
        {
            TDCam.transform.position = transform.position + new Vector3(pubOffset.x, pubOffset.y, -pubOffset.z);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!Controlable);
        }
        if (rig && Controlable)
        {
            Debug.DrawRay(rig.position, rig.transform.forward, Color.red);
            Debug.DrawRay(rig.position, rig.transform.right, Color.blue);
            if (damagetime > 0) damagetime -= Time.deltaTime;
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
                if (this.Health < 100) this.Health += 0.1f;
                if (this.Health > 100 && this.Health < 101)
                {
                    this.Health = 100;
                }
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
                        rig.AddForce(rig.transform.up * 8.5f * JumpHeight, ForceMode.VelocityChange);
                    }
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    RaycastHit hit;
                    if (Physics.BoxCast(FPCam.transform.position, new Vector3(0.2f, 0.2f, 0.2f), FPCam.transform.forward, out hit,  FPCam.transform.rotation, 5f))
                    {
                        var btn = hit.collider.GetComponent<BoxButton>();
                        if (btn)
                        {
                            btn.Press();
                        }
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
            if (asource && stepsound && rig.velocity.magnitude >= 0.05f && !asource.isPlaying && OnGround())
            {
                asource.clip = stepsound;
                asource.pitch = UnityEngine.Random.Range(0.75f, 1.25f);
                asource.Play();
            }
            else if (asource && rig.velocity.magnitude < 0.05f && asource.isPlaying)
            {
                asource.Stop();
            }
            if (Math.Abs(rig.velocity.y) < 0.01f)
            {
                rig.velocity = new Vector3(rig.velocity.x, 0, rig.velocity.z);
            }
        }
        if (this.transform.position.y < this.DeathPosition)
        {

            this.Health = 0;
            if (GameJoltAPI.Instance && GameJoltAPI.Instance.HasSignedInUser)
            {
                Trophies.TryUnlock(157702);
            }
        }
        if (this.Health <= 0)
        {
            if (rig)
            {
                rig.velocity = Vector3.zero;
            }
            this.transform.localPosition = firstPos;
            this.transform.localRotation = firstAng;
            this.Health = 100;
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

    public const float MaxSpeed = 20;

    public void MoveTo(Vector3 to)
    {
        if (!rig) return;
        to.y = 0;
        to = to.normalized;
        var nomt = to;
        var maxx = MaxSpeed * Math.Abs(nomt.x);
        var maxz = MaxSpeed * Math.Abs(nomt.z);
        to = rig.velocity + (to * 3f);
        /*if (Mathf.Abs(to.x) > maxx && Math.Abs(nomt.x) > 0)
        {
            to.x = to.x > 0 ? maxx : -maxx;
        }
        if (Mathf.Abs(to.z) > maxz && Math.Abs(nomt.z) > 0)
        {
            to.z = to.z > 0 ? maxz : -maxz;
        }*/
        if (Mathf.Abs(to.magnitude) > MaxSpeed)
        {
            to = to.normalized * MaxSpeed;
        }
        to = to * Speed;
        to.y = 0;
        //Debug.Log(to);
        //rig.AddForce(new Vector3(to.x, rig.velocity.y, to.z), ForceMode.VelocityChange);
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
    public float Health;
    public AudioClip stepsound;
    public AudioSource asource;
    public GameObject postProcessssss;

    public bool Controlable { get; private set; } = true;

    public MainCanvas canvas;
    private static Dictionary<int, CoreMode> coreTypes = new Dictionary<int, CoreMode>();

    public Rigidbody rig;
    public Collider col;
    //private const float jumpbound = 0.5f;

    public bool OnGround()
    {
        if (rig && col)
        {
            var jumpbound = col.bounds.size.x / 2;
            //RaycastHit hit;
            if (Physics.BoxCast(rig.transform.position, new Vector3(jumpbound, 0.04f, jumpbound), -Vector3.up, new Quaternion(), 0.4f))
            {
                //Debug.Log(hit.collider);
                return true;
            }
        }
        return false;
    }
}
