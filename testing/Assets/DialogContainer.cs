/*////////////////////////////////////////////////////////////////////////////////////////////////*/
/// DialogContainer.cs
/*///////////////////////////////////////////////////////////////////////////////////////////////*/
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DialogContainer : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] GameObject owner;
    public GameObject _Owner
    {
        get { return owner; }
        set { owner = value; }
    }
    [SerializeField] TMP_Text dialogName;
    public string _Name
    {
        get { return dialogName.text; }
        set { dialogName.text = value; }
    }
    [SerializeField] TMP_Text dialogMessage;
    public string _Message
    {
        get { return dialogMessage.text; }
        set { dialogMessage.text = value; }
    }
    List<string> messages;
    public List<string> _Messages
    {
        get { return messages; }
        set { messages = value; }
    }
    #endregion
    #region INITIALIZATION
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        dialogName = transform.GetChild(0).GetComponent<TMP_Text>();
        dialogMessage = transform.GetChild(1).GetComponent<TMP_Text>();
    }
    public void Initialize(GameObject _owner, List<string> _messages)
    {
        owner = _owner;
        dialogName.text = _owner.name;
        messages = _messages;
        dialogMessage.text = _messages[0];
    }
    #endregion
}
