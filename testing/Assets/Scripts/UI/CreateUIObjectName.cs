/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — CreateUIObjectName
/// PURPOSE — add a little UI name above owner to help identify it
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUIObjectName : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] Vector2 offset = new Vector3(0, 25);

    Transform tr;
    RectTransform rt;
    GameObject uiObjectName;
    RectTransform hud;
    #endregion

    #region INITIALIZATION
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Awake
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        tr = GetComponent<Transform>();
        hud = GameObject.FindGameObjectWithTag("UI").GetComponent<RectTransform>();
        InstantiateUIObjectName();
    }
    #endregion

     #region UPDATE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// update every frame
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        UpdatePosition();
    }
    #endregion
    
    #region METHODS
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// create, parent, and name the ui object name
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void InstantiateUIObjectName()
    {
        GameObject prefab = (GameObject)Resources.Load("ObjectName");
        uiObjectName = GameObject.Instantiate(prefab, hud.position, Quaternion.identity);
        uiObjectName.transform.SetParent(hud, false);
        rt = uiObjectName.GetComponent<RectTransform>();
        uiObjectName.GetComponent<UI_ObjectName>()._Name = this.gameObject.name;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// update position based on viewport/worldobjectscreenposition
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void UpdatePosition()
    {
        //calculate where to place the ui element
        Vector2 _viewportPosition = Camera.main.WorldToViewportPoint(tr.position);
        Vector2 _worldObjectScreenPosition = new Vector2(
        ((_viewportPosition.x * hud.sizeDelta.x) - (hud.sizeDelta.x * 0.5f)),
        ((_viewportPosition.y * hud.sizeDelta.y) - (hud.sizeDelta.y * 0.5f)));

        //correct position so it doesn't obscure the object
        _worldObjectScreenPosition += offset;

        //now you can set the position of the ui element
        rt.anchoredPosition = _worldObjectScreenPosition;
    }
    #endregion
}
