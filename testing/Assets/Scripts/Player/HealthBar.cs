using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DentedPixel;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Vector2 positionCorrection = new Vector2(0, 75);
    [SerializeField] Image healthBarImage;
    [Header("Animation")]
    [SerializeField] AnimationCurve decreaseAC;
    [SerializeField] float decreaseTime = 1f;
    [SerializeField] float decreaseDelay = 0.5f;
    [SerializeField] AnimationCurve increaseAC;
    [SerializeField] float increaseTime = 0.25f;
    [SerializeField] float increaseDelay = 0.25f;

    RectTransform canvasRectTransform;
    RectTransform rt;
    Transform ownerTransform;
    #region METHODS
    public void SetHealthBar(Transform _ownerTransform, RectTransform _canvasRectTransform, float _value)
    {
        rt = GetComponent<RectTransform>();
        ownerTransform = _ownerTransform;
        canvasRectTransform = _canvasRectTransform;

        UpdateHealthBarValue(_value);
        
        UpdateHealthBarPosition();

        rt.gameObject.SetActive(true);
    }
    void UpdateHealthBarPosition()
    {
        //calculate where to place the ui element
        Vector2 _viewportPosition = Camera.main.WorldToViewportPoint(ownerTransform.position);
        Vector2 _worldObjectScreenPosition = new Vector2(
        ((_viewportPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
        ((_viewportPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));

        //correct position so it doesn't obscure the object
        _worldObjectScreenPosition += positionCorrection;

        //now you can set the position of the ui element
        rt.anchoredPosition = _worldObjectScreenPosition;
    }
    public void UpdateHealthBarValue(float _value)
    {
        if(healthBarImage.fillAmount > _value)
        {
            AnimateDecrease(_value);
        }
        else if(healthBarImage.fillAmount < _value)
        {
            AnimateIncrease(_value);
        }
    }
    void AnimateDecrease(float _toValue)
    {
        if(!LeanTween.isTweening(this.gameObject))
        {
            LeanTween.value(this.gameObject, UpdateHealthBarImageFillAmount, healthBarImage.fillAmount, _toValue, decreaseTime).setDelay(increaseDelay).setEase(increaseAC);
        }
    }
    void AnimateIncrease(float _toValue)
    {
        if(!LeanTween.isTweening(this.gameObject))
        {
            LeanTween.value(this.gameObject, UpdateHealthBarImageFillAmount, healthBarImage.fillAmount, _toValue, increaseTime).setDelay(decreaseDelay).setEase(decreaseAC);
        }
    }
    void UpdateHealthBarImageFillAmount(float _value)
    {
        healthBarImage.fillAmount = _value;
    }
    #endregion
    void Update()
    {
        UpdateHealthBarPosition();
    }
}