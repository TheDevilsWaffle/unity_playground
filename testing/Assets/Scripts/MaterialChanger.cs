using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MaterialChanger : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] List<Material> materials = new List<Material>();
    public List<Material> _Materials
    {
        get { return materials; }
        set { materials = value; }
    }
    MeshRenderer mr;
    #endregion
    #region INITIALIZATION
    void Awake()
    {
        mr = GetComponent<MeshRenderer>();    
    }
    public void ChangeMaterial(int _index)
    {
        _index = IndexCorrection(_index);

        mr.material = materials[_index];
    }
    int IndexCorrection(int _index)
    {
        if(_index < 0)
        {
            _index = 0;
        }
        else if(_index >= materials.Count)
        {
            _index = materials.Count - 1;
        }
        return _index;
    }
    #endregion
}
