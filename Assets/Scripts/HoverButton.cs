using UnityEngine;
using System.Collections;

public class HoverButton : MonoBehaviour {

    public Sprite hoverSprite;
    public Sprite normalSprite;

    // We want to change the sprite when the mouse over the button
    void OnMouseOver()
    {
        transform.GetComponent<SpriteRenderer>().sprite = hoverSprite;
    }

    // We want to change the sprite when the mouse over the button
    void OnMouseExit()
    {
        transform.GetComponent<SpriteRenderer>().sprite = normalSprite;
    }
}
