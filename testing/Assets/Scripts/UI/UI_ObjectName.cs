/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — UI_ObjectName
/// PURPOSE — float some nice little ui text above an object to describe what it is
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using TMPro;

public class UI_ObjectName : MonoBehaviour
{
    #region PROPERTIES
    TMP_Text tmpText;
    public string _Name
    {
        get { return gameObject.name; }
        set { gameObject.name = value;
              tmpText.text = value; }
    }
    #endregion

    #region INITIALIZATION  
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// upon script enable
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        tmpText.text = this.gameObject.name;
    }
    #endregion
}
