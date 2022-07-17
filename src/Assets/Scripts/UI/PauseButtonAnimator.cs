using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 startPos;
    private bool hovering = false;

    public GameObject buttonText;
    
    public Vector3 hoverPosDifference;
    public Vector3 downPosDifference;

    public Color notHoverTextColor;
    public Color hoverTextColor;

    void Start() {
        startPos = buttonText.transform.localPosition;
        buttonText.GetComponent<Text>().color = notHoverTextColor;
    }

    void ResetButton() {
        buttonText.transform.localPosition = startPos;
        buttonText.GetComponent<Text>().color = notHoverTextColor;
    }

    void ButtonHover() {
        buttonText.transform.localPosition = startPos + hoverPosDifference;
        buttonText.GetComponent<Text>().color = hoverTextColor;
    }

    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log(eventData.pointerEnter);
        bool isHoveringSelf = eventData.pointerEnter == gameObject;
        if (!isHoveringSelf) { return; }
        if (hovering) { return; }

        AudioManager.instance.PlaySound("Button_Hover");
        ButtonHover();
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData){
        if (!hovering) { return; }

        ResetButton();
        hovering = false;
    }

    public void OnPointerDown(PointerEventData eventData) {
        buttonText.transform.localScale = new Vector3(0.97f, 0.97f, 0);
        buttonText.transform.localPosition = startPos - hoverPosDifference;
    }

    public void OnPointerUp(PointerEventData eventData) {
        AudioManager.instance.PlaySound("Button_Click");
        buttonText.transform.localScale = new Vector3(1f, 1f, 0);
        buttonText.transform.localPosition = startPos;
    }
}
