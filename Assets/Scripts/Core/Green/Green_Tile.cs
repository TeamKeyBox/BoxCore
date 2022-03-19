using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Tile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Break", 5f);
    }

    private void OnEnable()
    {
        if (render)
        {
            render.color = new Color(render.color.r, render.color.g, render.color.b, 1f);
        }
        this.GetComponent<Collider>().enabled = true;
    }

    private void OnDisable()
    {
        if (render)
        {
            render.color = new Color(render.color.r, render.color.g, render.color.b, 0.5f);
        }
        this.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (StaticObjs.currentPly)
        {
            //this.GetComponent<Collider>().enabled = !StaticObjs.currentPly.IsFP;
            if (render)
            {
                render.color = new Color(render.color.r, render.color.g, render.color.b, StaticObjs.currentPly.IsFP ? 0.5f : 1f);
            }
        }
    }

    public void Break()
    {
        if (gibs)
        {
            for (var i = 0;i < 10;i++)
            {
                var temp = Instantiate(gibs);
                temp.SetActive(true);
                var x = Random.Range(-0.5f, 0.5f);
                var y = Random.Range(-0.5f, 0.5f);
                temp.transform.SetParent(this.transform);
                temp.transform.localPosition = new Vector3(x, y);
                temp.transform.SetParent(null);
                Destroy(temp, 3f);
                var rig = temp.GetComponent<Rigidbody2D>();
                if (rig)
                {
                    rig.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
                }
            }
        }
        Destroy(this.gameObject);
    }

    public SpriteRenderer render;
    public GameObject gibs;
}
