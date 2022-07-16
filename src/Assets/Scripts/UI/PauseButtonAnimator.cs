using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 startPos;

    public GameObject buttonText;
    
    public Vector3 hoverPosDifference;
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
       Debug.Log("Pointer Entered");
       ButtonHover();
    }

    public void OnPointerExit(PointerEventData eventData){
       Debug.Log("Pointer Exited");
       ResetButton();
    }
}
