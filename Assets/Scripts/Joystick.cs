/*
 * Virtual Joystick 구현
 * Canvas 밑에 Prefab을 넣어서 활용
 * GetHorizontalValue()와 GetVerticalValue() 값을 통해 Joystick의 위치를 알 수 있음 (각각 -1 ~ 1, 원 밖에서 normalize)
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image _backgroundImage;
    private Image _joystickImage;
    private Vector3 _inputVector;

    private void Start()
    {
        _backgroundImage = GetComponent<Image>();
        _joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // get position in [-.5, .5]
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_backgroundImage.rectTransform, eventData.position,
            eventData.pressEventCamera, out var pos);
        var sizeDelta = _backgroundImage.rectTransform.sizeDelta;
        pos.x /= sizeDelta.x;
        pos.y /= sizeDelta.y;
        
        // normalize in [-1, 1]
        _inputVector = new Vector3(pos.x * 2, pos.y * 2, 0);
        _inputVector = _inputVector.magnitude > 1 ? _inputVector.normalized : _inputVector;
        
        // move joystick image
        _joystickImage.rectTransform.anchoredPosition = new Vector3(_inputVector.x * (sizeDelta.x / 3),
            _inputVector.y * (sizeDelta.y / 3));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // reset
        _inputVector = Vector3.zero;
        _joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // begin drag
        OnDrag(eventData);
    }
    
    public float GetHorizontalValue()
    {
        return _inputVector.x;
    }
    
    public float GetVerticalValue()
    {
        return _inputVector.y;
    }
}