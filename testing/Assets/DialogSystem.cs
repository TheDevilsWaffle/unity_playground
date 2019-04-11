using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    //player goes into dialog range and tells this to create a dialog

    1 - instantiate dialog based on dialog prefab
    2 - update position of dialog based on dialog owner's position


*/

public class DialogSystem : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] GameObject dialogPrefab;
    List<DialogContainer> activeDialogs = new List<DialogContainer>();

    Transform tr;
    #endregion
    #region INITIALIZE
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        tr = GetComponent<Transform>();
    }
    #endregion
    #region METHODS
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    DialogContainer CreateDialog(GameObject _owner, List<string> _messages)
    {
        DialogContainer _dialog = GameObject.Instantiate(dialogPrefab, Vector3.zero, Quaternion.identity, tr).GetComponent<DialogContainer>();
        _dialog.Initialize(_owner, _messages);
        return _dialog;
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    public void DisplayDialog(GameObject _go, List<string> _messages)
    {
        activeDialogs.Add(CreateDialog(_go, _messages));
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    Vector3 UpdateDialogPosition(GameObject _owner)
    {
        return Camera.main.WorldToScreenPoint(_owner.transform.position);
    }
    #endregion
    #region UPDATE
    void Update()
    {
        if (activeDialogs.Count > 0)
        {
            foreach (DialogContainer _dialogContainer in activeDialogs)
            {
                _dialogContainer.transform.position = UpdateDialogPosition(_dialogContainer._Owner);
            }
        }
    }
    #endregion
}
