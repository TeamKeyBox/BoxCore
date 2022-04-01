using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Grid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!ghostTile || !ghostTile.isActiveAndEnabled)
        {
            ghostTile = Instantiate(tile.gameObject).GetComponent<Green_Tile>();
            if (ghostTile)
            {
                ghostTile.transform.SetParent(this.transform);
                ghostTile.enabled = false;
                ghostTile.gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (raycol)
        {
            raycol.size = size;
        }
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, StaticObjs.currentPly.transform.position.z);
        }
        if (IsEnabled())
        {
            if (ghostTile) {
                var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ghostTile.transform.position = new Vector3(mousepos.x, mousepos.y, StaticObjs.currentPly.transform.position.z);
                ghostTile.gameObject.SetActive(true);
            }
            if (Input.GetMouseButtonDown(0) && CanBePlaced())
            {
                RaycastHit2D hit;
                LayerMask mask = LayerMask.GetMask("TDCamOnly");
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), this.transform.forward, 0.001f, mask);
                if (hit && hit.collider == base.GetComponent<Collider2D>())
                {
                    var til = Instantiate(ghostTile.gameObject).GetComponent<Green_Tile>();
                    til.transform.SetParent(this.transform);
                    til.transform.position = ghostTile.transform.position;
                    til.enabled = true;
                }
            }

        }
        else
        {
            if (ghostTile) ghostTile.gameObject.SetActive(false);
        }
        debug = IsEnabled() + "," + CanBePlaced();
    }

    private bool IsEnabled()
    {
        return StaticObjs.currentPly && StaticObjs.currentPly.Controlable && !StaticObjs.currentPly.IsFP && StaticObjs.currentPly.CurrentCore == 2;
    }

    private bool CanBePlaced()
    {
        return IsEnabled();
    }

    public Vector2 size;
    public BoxCollider2D raycol;
    public Green_Tile tile;
    private static Green_Tile ghostTile;
    public string debug { get; private set; }
}
