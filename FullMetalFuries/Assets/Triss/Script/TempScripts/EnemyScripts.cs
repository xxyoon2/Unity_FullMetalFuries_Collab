using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScripts : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Attack")
        {
            ColorChange(Color.red);
            Invoke("ColorChange", 1f);
        }
    }

    private void ColorChange()
    {
        ColorChange(Color.white);
    }

    private void ColorChange(Color newColor)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = newColor;
    }
}
