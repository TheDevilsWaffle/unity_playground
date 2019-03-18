///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PhysicsAttractor : MonoBehaviour
{
    #region PROPERTIES
    [Header("SIZE")]
    [SerializeField] float radius;
    public float _Radius
    {
        get { return radius; }
        set { 
                radius = value;
                collider.radius = radius;
            }
    }
    [Header("STRENGTH")]
    [SerializeField] bool isStrengthDistanceBased = false;
    public bool _IsStrengthDistanceBased
    {
        get { return isStrengthDistanceBased; }
        set { isStrengthDistanceBased = value; }
    }
    [SerializeField] float strength;
    public float _Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    [Header("PULSE")]
    [SerializeField] bool isPulse = false;
    public bool _IsPulse
    {
        get { return isPulse; }
        set { isPulse = value; }
    }
    [SerializeField] float pulseDuration = 1f;
    public float _PulseDuration
    {
        get { return pulseDuration; }
        set { pulseDuration = value; }
    }
    [SerializeField] float delayBetweenPulse = 0.5f;
    public float _DelayBetweenPulse
    {
        get { return delayBetweenPulse; }
        set { delayBetweenPulse = value; }
    }

    [SerializeField] Material[] pulseMaterials; //delete this later

    [Header("TARGETS")]
    [TagSelector]
    [SerializeField] string[] targetTags;
    List<GameObject> targets;

    SphereCollider collider;
    Vector3 position;
    float timer = 0;
    bool delay = false;
    #endregion
    #region INITIALIZE
    ///
    void OnValidate()
    {
        position = transform.position;
        collider = GetComponent<SphereCollider>();

        _Radius = radius;
        collider.isTrigger = true;
        
        targets = new List<GameObject>();

        timer = 0;
        delay = false;
    }
    ///
    void Awake()
    {
        OnValidate();
    }
    #endregion
    #region TRIGGERS
    ///
    void OnTriggerEnter(Collider _otherCollider)
    {
        if(!_otherCollider.isTrigger)
        {
            targets.Add(_otherCollider.gameObject);
        }
                
    }
    ///
    void OnTriggerExit(Collider _otherCollider)
    {
        if(!_otherCollider.isTrigger)
        {
            targets.Remove(_otherCollider.gameObject);
        } 
    }
    #endregion
    #region FIXED UPDATE
    ///
    private void FixedUpdate()
    {
        if(isPulse)
        {
            Pulsate();
        }
        else
        {
            AttractTargets();
        }
    }
    #endregion
    #region METHODS
    /// 
    void ResetPulseTimer()
    {
        timer = 0f;
        delay = !delay;
    }
    ///
    void Pulsate()
    {
        if(!delay)
        {
            timer += Time.fixedDeltaTime;
            if(timer <= pulseDuration)
            {
                this.GetComponent<Renderer>().material = pulseMaterials[0]; //delete this later
                AttractTargets();
                return;
            }
            else if (timer >= pulseDuration)
            {
                ResetPulseTimer();
            }
        }
        else if (delay)
        {
            this.GetComponent<Renderer>().material = pulseMaterials[1]; //delete this later
            timer += Time.fixedDeltaTime;
            if(timer >= delayBetweenPulse)
            {
                ResetPulseTimer();
            }
        }
    }
    void AttractTargets()
    {
        foreach (GameObject _target in targets)
        {
            _target.GetComponent<Rigidbody>().AddForce(CalculateForce(_target.transform.position), ForceMode.Impulse);
        }
    }
    ///
    Vector3 CalculateForce(Vector3 _targetPosition)
    {
        if(isStrengthDistanceBased)
        {
            float _distance = Vector3.Distance(position, _targetPosition);
            _targetPosition = position - _targetPosition;
            return (_targetPosition.normalized * (strength * (1 - _distance / radius)) * Time.fixedDeltaTime);
        }
        else
        {
            _targetPosition = position - _targetPosition;
            return (_targetPosition.normalized * strength * Time.fixedDeltaTime);
        }
    }
    #endregion
}