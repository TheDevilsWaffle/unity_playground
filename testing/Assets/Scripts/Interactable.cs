using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivationType
{
    AUTOMATIC,
    MANUAL
};

public class Interactable : MonoBehaviour
{
    [SerializeField] ActivationType activationType = ActivationType.AUTOMATIC;
    public ActivationType _ActivationType
    {
        get { return activationType; }
        set { activationType = value; }
    }
    public virtual void Activate(GameObject _activator) { }
}
