using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] GameObject dialogContainer;
    List<GameObject> activeDialogs = new List<GameObject>();

    #endregion
    public void DisplayDialog(GameObject _go, string _dialog)
    {
        activeDialogs.Add(_go);

        //turn on dialog if it needs to be turned on
        ActivateDialog();
    }
    void Update()
    {
        if(activeDialogs.Count > 0)
        {
            UpdateDialogPositions();
        }
    }
    void UpdateDialogPositions()
    {
        foreach (var _dialogs in activeDialogs)
        {
            dialogContainer.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(_dialogs.transform.position);
        }
    }
    void ActivateDialog()
    {
        if (dialogContainer.activeSelf)
            return;
        else
        {
            dialogContainer.SetActive(true);
        }
    }
}
