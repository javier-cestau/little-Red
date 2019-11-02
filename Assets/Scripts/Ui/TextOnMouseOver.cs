using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.EventSystems;
public class TextOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor;
    Text text;

    Color originalColor;

    void Awake() {
        text = GetComponentInChildren<Text>();
        originalColor = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        text.color = originalColor;
    }
}
