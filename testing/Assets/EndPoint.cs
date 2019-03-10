using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] Tile tile;
    [SerializeField] EndPoint otherEndPoint;
    Vector3 tilePos;

    void OnValidate()
    {
        tile = transform.GetComponentInParent<Tile>();
        tilePos = tile.transform.position;
    }
    void Awake()
    {
        OnValidate();
    }
    void ConnectEndPoints()
    {
        
    }
}
