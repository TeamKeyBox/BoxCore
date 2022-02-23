using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (text) text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            var dist = Vector3.Distance(player.transform.position, this.transform.position);
            var fadeandview = 1 - ((dist - viewDistance) / (fadeDistance - viewDistance));
            if (dist <= fadeDistance)
            {
                if (dist <= viewDistance)
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                }
                else
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, fadeandview);
                }
            }
            else
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            }
        }
    }

    public TextMesh text;

    public float fadeDistance
    {
        get
        {
            return viewDistance + 20;
        }
    }
    public float viewDistance = 40;
    public Player player;
}
