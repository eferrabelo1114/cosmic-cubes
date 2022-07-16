using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimator : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public Image buttonImage;
    public Sprite buttonDown;
    public Sprite buttonUp;
    public Transform buttonText;

    void Start() {
        buttonImage.sprite = buttonUp;
        buttonText.localPosition = new Vector3(0, 3.47f, 0);
    }

    void ChangeButtonToDown() {
        buttonImage.sprite = buttonDown;
        buttonText.localPosition = new Vector3(0, 1.33f, 0);
    }

    void ChangeButtonToUp() {
        buttonImage.sprite = buttonUp;
        buttonText.localPosition = new Vector3(0, 3.47f, 0);
    }

    public void OnPointerUp(PointerEventData eventData){
       Debug.Log("Pointer Up");
       ChangeButtonToUp();
    }

    public void OnPointerDown(PointerEventData eventData){
       Debug.Log("Pointer Down");
       ChangeButtonToDown();
    }
}
