﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — KeyboardGamepadLayout.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardGamepadLayout : MonoBehaviour
{
    #region FIELDS
    [Header("Left Analog Stick")]
    public KeyCode up = KeyCode.W;
    [Space]
    public KeyCode right = KeyCode.D;
    [Space]
    public KeyCode down = KeyCode.S;
    [Space]
    public KeyCode left = KeyCode.A;

    [Header("Right Analog Stick")]
    public KeyCode upAlt = KeyCode.I;
    [Space]
    public KeyCode rightAlt = KeyCode.L;
    [Space]
    public KeyCode downAlt = KeyCode.K;
    [Space]
    public KeyCode leftAlt = KeyCode.J;

    [Header("DPad")]
    public KeyCode DPadUp = KeyCode.UpArrow;
    [Space]
    public KeyCode DPadRight = KeyCode.RightArrow;
    [Space]
    public KeyCode DPadDown = KeyCode.DownArrow;
    [Space]
    public KeyCode DPadLeft = KeyCode.LeftArrow;
    [Space]

    [Header("Buttons")]
    public KeyCode y = KeyCode.LeftControl;
    [Space]
    public KeyCode b = KeyCode.Q;
    [Space]
    public KeyCode a = KeyCode.Space;
    [Space]
    public KeyCode x = KeyCode.E;
    [Space]

    [Header("Misc Buttons")]
    public KeyCode view = KeyCode.Tab;
    [Space]
    public KeyCode menu = KeyCode.Return;
    [Space]
    public KeyCode l3 = KeyCode.O;
    [Space]
    public KeyCode r3 = KeyCode.P;
    [Space]

    [Header("Bumpers")]
    public KeyCode lb = KeyCode.LeftAlt;
    [Space]
    public KeyCode rb = KeyCode.LeftShift;
    [Space]

    [Header("Triggers")]
    public KeyCode lt = KeyCode.RightAlt;
    [Space]
    public KeyCode rt = KeyCode.RightShift;
    [Space]

    bool enableKeyboard = false;
    List<InputData> keys;
    #endregion
    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //create keys dictionary and list (list needed to traverse dictionary and update dictionary value(cannot update value by reference))
        keys = new List<InputData>();

        //check for assigned keys and store them in keys
        AddAssignedKeys();
        SetSubscriptions();
    }
    #endregion
    #region SUBSCRIPTIONS
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// RemoveSubscriptions()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void RemoveSubscriptions()
    {
        
    }
    #endregion
    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        //go through the keyboard input update loop
        UpdateKeyboardInput();
        
        //DEBUG
        //print out to the console the contents of keys
        //foreach(KeyValuePair<KeyCode, InputStatus> key in keys)
        //{
        //    Debug.Log("key = " + key.Key + ", and its status is = " + key.Value);
        //}
    }
    #endregion
    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Adds only the assigned buttons from Unity Inspector to keys
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void AddAssignedKeys()
   {
        #region LEFT ANALOG STICK
        if (up != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("up");
            _inputData.SetKey(up);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (right != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("right");
            _inputData.SetKey(right);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (down != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("down");
            _inputData.SetKey(down);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (left != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("left");
            _inputData.SetKey(left);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        #endregion
        #region RIGHT ANALOG STICK
        if (upAlt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("upAlt");
            _inputData.SetKey(upAlt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (rightAlt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("rightAlt");
            _inputData.SetKey(rightAlt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (downAlt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("downAlt");
            _inputData.SetKey(downAlt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (leftAlt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("leftAlt");
            _inputData.SetKey(leftAlt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        #endregion
        #region DPAD
        if (DPadUp != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("dpad_up");
            _inputData.SetKey(DPadUp);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (DPadRight != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("dpad_right");
            _inputData.SetKey(DPadRight);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (DPadDown != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("dpad_down");
            _inputData.SetKey(DPadDown);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (DPadLeft != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("dpad_left");
            _inputData.SetKey(DPadLeft);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        #endregion
        #region BUTTONS
        if (y != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("y");
            _inputData.SetKey(y);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (b != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("b");
            _inputData.SetKey(b);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (a != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("a");
            _inputData.SetKey(a);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (x != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("x");
            _inputData.SetKey(x);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        #endregion
        #region BUMPERS
        if (lb != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("lb");
            _inputData.SetKey(lb);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (rb != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("rb");
            _inputData.SetKey(rb);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        #endregion
        #region TRIGGERS
        if (lt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("lt");
            _inputData.SetKey(lt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (rt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("rt");
            _inputData.SetKey(rt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        #endregion
        #region MISC. BUTTONS
        if (l3 != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("l3");
            _inputData.SetKey(l3);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (r3 != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("r3");
            _inputData.SetKey(r3);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (view != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("view");
            _inputData.SetKey(view);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        if (menu != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("menu");
            _inputData.SetKey(menu);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// cycles through each key in keys and updates their InputData
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateKeyboardInput()
    {
        //loop through each InputData in keys
        for(int _index = 0; _index < keys.Count; ++_index)
        {
            #region RELEASED
            if (Input.GetKeyUp(keys[_index].Key))
            {
                //update data
                keys[_index].SetStatus(InputStatus.RELEASED);
                keys[_index].SetXYRawValue(Vector2.zero);
                keys[_index].AddXYValue(Vector2.zero);
                keys[_index].SetHeldDuration(0f);
                keys[_index].SetInactiveDuration(Time.deltaTime);

                Events.instance.Raise(new EVENT_KEYBOARD_KEY_BROADCAST(keys[_index]));

                //DEBUG — check which key is released and print its status
                //Debug.Log("key released: " + keys[_index].Key + " status = " + keys[_index].Status);
            }
            #endregion
            #region HELD
            if (Input.GetKey(keys[_index].Key))
            {
                //update data
                keys[_index].SetStatus(InputStatus.HELD);
                keys[_index].SetXYRawValue(Vector2.one);
                keys[_index].AddXYValue(Vector2.one);
                keys[_index].SetHeldDuration(Time.deltaTime);
                keys[_index].SetInactiveDuration(0);

                //broadcast a message
                Events.instance.Raise(new EVENT_KEYBOARD_KEY_BROADCAST(keys[_index]));

                //DEBUG — check which key is held and print its status
                //Debug.Log("key held: " + keys[_index].Key + " status = " + keys[_index].Status);
            }
            #endregion
            #region PRESSED
            if (Input.GetKeyDown(keys[_index].Key))
            {
                //update data
                keys[_index].SetStatus(InputStatus.RELEASED);
                keys[_index].SetXYRawValue(Vector2.one);
                keys[_index].AddXYValue(Vector2.one);
                keys[_index].SetHeldDuration(Time.deltaTime);
                keys[_index].SetInactiveDuration(0f);

                //broadcast a message
                Events.instance.Raise(new EVENT_KEYBOARD_KEY_BROADCAST(keys[_index]));

                //DEBUG — check which key is pressed and print its status
                //Debug.Log("key pressed: " + keys[_index].Key + " status = " + keys[_index].Status);
            }
            #endregion
            #region INACTIVE
            //inactive
            else
            {
                //update data
                keys[_index].SetStatus(InputStatus.INACTIVE);
                keys[_index].SetXYRawValue(Vector2.zero);
                keys[_index].AddXYValue(Vector2.zero);
                keys[_index].SetHeldDuration(0f);
                keys[_index].SetInactiveDuration(Time.deltaTime);

                //DEBUG — check which key is inactive and print its status
                //Debug.Log("key inactive: " + keys[_index].Key + " status = " + keys[_index].Status);
            }
            #endregion
        }
    }
    #endregion
    #region ONDESTROY
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        RemoveSubscriptions();
    }
    #endregion
}