/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — PhysicsAttractor
/// PURPOSE — pulls object w/ RigidBodies towards it (like being sucked out into space, a
///           black hole, magnet, etc.)
/*///////////////////////////////////////////////////////////////////////////////////////////*/
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
                sCollider.radius = radius;
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

    SphereCollider sCollider;
    Vector3 position;
    float timer = 0;
    bool delay = false;
    #endregion

    #region INITIALIZE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// set attributes and reflect changes in inspector
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void OnValidate()
    {
        position = transform.position;
        sCollider = GetComponent<SphereCollider>();

        _Radius = radius;
        sCollider.isTrigger = true;
        
        targets = new List<GameObject>();

        timer = 0;
        delay = false;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// upon script awake
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        OnValidate();
    }
    #endregion

    #region TRIGGERS
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// on trigger enter
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter(Collider _otherCollider)
    {
        if(!_otherCollider.isTrigger)
            targets.Add(_otherCollider.gameObject);         
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// on trigger exit
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void OnTriggerExit(Collider _otherCollider)
    {
        if(!_otherCollider.isTrigger)
            targets.Remove(_otherCollider.gameObject);
    }
    #endregion
    #region UPDATE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// reliable phyics update
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    private void FixedUpdate()
    {
        if(isPulse)
            Pulsate();
        else
            AttractTargets();
    }
    #endregion
    
    #region METHODS
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// reset pulse timer helper so it isn't writen out a lot
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void ResetPulseTimer()
    {
        timer = 0f;
        delay = !delay;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// pull objects and then rest in a 'pulsate-like' fashion
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Pulsate()
    {
        if(!delay)
        {
            timer += Time.fixedDeltaTime;
            if(timer <= pulseDuration)
            {
                //change colors so we can see it working, but delete this or change it later
                this.GetComponent<Renderer>().material = pulseMaterials[0];
                AttractTargets();
                return;
            }
            else if (timer >= pulseDuration)
                ResetPulseTimer();
        }
        else if (delay)
        {
            //change colors so we can see it working, but delete this or change it later
            this.GetComponent<Renderer>().material = pulseMaterials[1];
            timer += Time.fixedDeltaTime;
            if(timer >= delayBetweenPulse)
                ResetPulseTimer();
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// pull targets towards this, but don't disallow them to fight back w/ their own movement
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void AttractTargets()
    {
        foreach (GameObject _target in targets)
            _target.GetComponent<Rigidbody>().AddForce(CalculateForce(_target.transform.position), ForceMode.Impulse);
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// calculate force either based off distance from this or just use a fixed value
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    Vector3 CalculateForce(Vector3 _targetPosition)
    {
        //distance/strength based pull
        if(isStrengthDistanceBased)
        {
            float _distance = Vector3.Distance(position, _targetPosition);
            _targetPosition = position - _targetPosition;
            return (_targetPosition.normalized * (strength * (1 - _distance / radius)) * Time.fixedDeltaTime);
        }
        //fixed value pull
        else
        {
            _targetPosition = position - _targetPosition;
            return (_targetPosition.normalized * strength * Time.fixedDeltaTime);
        }
    }
    #endregion
}