using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MazeGeneratorVisual : MonoBehaviour
{
    [SerializeField] int size = 10;

    [Header("References")]
    [SerializeField] GameObject CornerPrefab;
    [SerializeField] GameObject WallPrefab;

    private bool[,] visitedCells;

    private List<Ray> rays = new();

    void CreateBaseGrid()
    {
        visitedCells = new bool[size, size];
        for (int i = 0; i < size + 1; i++)
        {
            for (int j = 0; j < size + 1; j++)
            {
                Instantiate(CornerPrefab, new Vector3(j, 0, i), Quaternion.identity, transform);
                if (j < size)
                    Instantiate(WallPrefab, new Vector3(j + 0.5f, 0, i), Quaternion.identity, transform);
                if (i < size)
                    Instantiate(WallPrefab, new Vector3(j, 0, i + 0.5f), Quaternion.Euler(0, 90, 0), transform);
            }
        }
    }

    async Task visitCell(int x, int y, int px, int py)
    {
        Vector3 origin = new(x + 0.5f, 0.5f, y + 0.5f);
        Vector3 direction = new Vector3(px + 0.5f, 0.5f, py + 0.5f) - origin;
        if (visitedCells[y, x] == true)
        {
            //rays.Remove(new Ray(origin, direction));
            return;
        }
        visitedCells[y, x] = true;
        
        // Destroy wall between current cell and previous cell
        RaycastHit hit;
        Physics.Raycast(origin, direction, out hit, direction.magnitude);
        if (hit.collider != null)
        {
#if UNITY_EDITOR
            rays.Add(new Ray(origin, direction));
            DestroyImmediate(hit.collider.gameObject);
            await Task.Delay(TimeSpan.FromSeconds(0.1));
#else
            Destroy(hit.collider.gameObject);
#endif
        }

        List<Vector2> neighbours = new List<Vector2>();
        if (y > 0 && visitedCells[y - 1, x] != true)
        {
            neighbours.Add(new Vector2(x, y - 1));
        }
        if (y < size - 1 && visitedCells[y + 1, x] != true)
        {
            neighbours.Add(new Vector2(x, y + 1));
        }
        if (x > 0 && visitedCells[y, x - 1] != true)
        {
            neighbours.Add(new Vector2(x - 1, y));
        }
        if (x < size - 1 && visitedCells[y, x + 1] != true)
        {
            neighbours.Add(new Vector2(x + 1, y));
        }

        neighbours.Shuffle();
        foreach (Vector2 v in neighbours)
        {
            await visitCell((int)v.x, (int)v.y, x, y);
        }
        rays.Remove(new Ray(origin, direction));
    }

    public async Task GenerateAsync()
    {
        Clear();
        CreateBaseGrid();
        await visitCell(0, 0, 0, 0);
    }

    public void Clear()
    {
        List<Transform> childrenList = new List<Transform>();
        //Adds all children to the list
        foreach (Transform childTrans in transform)
            childrenList.Add(childTrans);

        foreach (Transform child in childrenList)
#if UNITY_EDITOR
            DestroyImmediate(child.gameObject);
#else
            Destroy(child.gameObject);
#endif
        rays.Clear();
    }

    private void OnDrawGizmos()
    {
        foreach (var ray in rays)
        {
            Gizmos.DrawRay(ray.origin, ray.direction);
        }
    }
}
