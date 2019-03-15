
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTiles : MonoBehaviour
{
    [SerializeField] IntVector2 size;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] float delay = 0.5f;
    [SerializeField] KeyCode generateTilesButton = KeyCode.Space;
    Tile[,] tiles;
    Coroutine co_generate = null;
    void Awake()
    {
        tiles = new Tile[size.x, size.z];
    }
    void Update()
    {
        if(Input.GetKeyDown(generateTilesButton))
        {
            StopAndGenerateTiles();   
        }
    }
    void DestroyTiles()
    {
        Array.Clear(tiles, 0, tiles.Length);
        foreach(Transform _child in this.transform)
        {
            GameObject.Destroy(_child.gameObject);
        }
    }
    void StopAndGenerateTiles()
    {
        if(co_generate != null)
        {
            StopCoroutine(co_generate);
        }
        DestroyTiles();
        co_generate = StartCoroutine(Generate());
    }
    IEnumerator Generate()
    {
        WaitForSeconds _delay = new WaitForSeconds(delay);
        tiles = new Tile[size.x, size.z];
        for (int _x = 0; _x < size.x; ++_x)
        {
            for (int _z = 0; _z < size.z; ++_z)
            {
                yield return _delay;
                CreateTile(new IntVector2(_x, _z));
            }
        }
    }
    void CreateTile(IntVector2 _coordinates)
    {
        GameObject _newTile = GameObject.Instantiate(tilePrefab);
        tiles[_coordinates.x, _coordinates.z] = _newTile.GetComponent<Tile>();
        _newTile.name = "Tile(" + _coordinates.x + "," + _coordinates.z + ")";
        _newTile.transform.SetParent(this.transform);
        _newTile.transform.localPosition = new Vector3(_coordinates.x, 0, _coordinates.z);
    }
}
