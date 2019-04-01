using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPosition;
    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distance;

    Node[,] grid;
    public List<Node> finalPath;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);    
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);   

        CreateGrid(); 
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for(int _y = 0; _y < gridSizeY; ++_y)
        {
            for(int _x = 0; _x < gridSizeX; ++_x)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (_x * nodeDiameter - nodeRadius) + Vector3.forward * (_y * nodeDiameter + nodeRadius);
                bool wall = true;
                if (Physics.CheckSphere(worldPoint, nodeRadius, wallMask))
                {
                    wall = false;
                }

                grid[_y, _x] = new Node(wall, worldPoint, _x, _y);
            }
        }
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(grid != null)
        {
            foreach (Node _node in grid)
            {
                if(_node.isWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }
                if (finalPath != null)
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(_node.position, Vector3.one * (nodeDiameter - distance));
            }
        }
    }
}
