using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    EMPTY,
    WALL_SINGLE_0_2,
    WALL_SINGLE_1_3,
    WALL_CORNER_0_1,
    WALL_CORNER_1_2,
    WALL_CORNER_2_3,
    WALL_CORNER_3_4,
    WALL_CORNER_4_0,
    WALL_T_0_1_2,
    WALL_T_1_2_3,
    WALL_T_2_3_4,
    WALL_T_3_4_0,
    WALL_X
};

public class Tile : MonoBehaviour
{
    Tile[] neighbors; //0 north, 1 east, 2 south 3 west
    [SerializeField] TileType tileType;
    [SerializeField] List<GameObject> contents = new List<GameObject>();

    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// OnValidate
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void OnValidate()
    {
        contents = new List<GameObject>();
        foreach (Transform _child in this.transform)
        {
            contents.Add(_child.gameObject);
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// Awake
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        OnValidate();    
    }
    void SetNeighbor(Tile _tile, int _index)
    {
        neighbors[_index] = _tile;
    }
}
