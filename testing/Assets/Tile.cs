using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Tile[] neighbors; //0 north, 1 east, 2 south 3 west

    void SetNeighbor(Tile _tile, int _index)
    {
        neighbors[_index] = _tile;
    }
}
