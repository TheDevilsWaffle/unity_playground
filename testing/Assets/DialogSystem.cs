using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] GameObject dialogContainer;
    #endregion
    public void DisplayDialog(string _name, string _dialog, Vector3 _position)
    {
        //turn on dialog if it needs to be turned on
        ActivateDialog(_position);
    }

    void ActivateDialog(Vector3 _position)
    {
        if (dialogContainer.activeSelf)
            return;
        else
        {
            dialogContainer.SetActive(true);
            dialogContainer.GetComponent<RectTransform>().position = _position;
        }
    }
}
