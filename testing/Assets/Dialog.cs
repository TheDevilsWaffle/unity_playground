﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : Interactable
{
    #region PROPERTIES
    [SerializeField] DialogSystem dialogSystem;
    [TextArea(3, 5)]
    [SerializeField] List<string> dialogs = new List<string>();
    [SerializeField] Vector3 offsetPosition = Vector3.zero;

    Vector3 position;
    int dialogCount;
    int currentDialog = 0;
    bool isDialogActive = false;
    #endregion

    #region INITIALIZATION
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate()
    {
        dialogSystem = FindObjectOfType<DialogSystem>();
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        ResetDialog();
    }
    #endregion
    #region METHODS
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// ResetDialog()
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void ResetDialog()
    {
        dialogCount = dialogs.Count;
        currentDialog = 0;
        isDialogActive = false;
    }
    #endregion
    #region OVERRIDE METHODS
    public override void Activate()
    {
        dialogSystem.DisplayDialog(this.gameObject, dialogs[0]);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Activate();
        }
    }
    #endregion
}