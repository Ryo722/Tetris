using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // ミノの座標を管理する二次元配列
    private Transform[,] grid;

    [SerializeField]
    private Transform emptySprite;

    [SerializeField]
    private int height = 30, width = 10, header = 8;

    private void Awake()
    {
        grid = new Transform[width, height];
    }

    private void Start()
    {
        CreateStage();
    }

    void CreateStage()
    {
        if (emptySprite)
        {
            for (int y = 0; y < height - header; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Transform clone = Instantiate(emptySprite, new Vector3(x, y, 0), Quaternion.identity);

                    clone.transform.parent = transform;
                }

            }
        }
    }

    public bool CheckPosition(Mino mino)
    {
        foreach (Transform item in mino.transform)
        {
            Vector2 pos = Rounding.Round(item.position);


            if (!StageOutCheck((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (MinoCheck((int)pos.x, (int)pos.y, mino))
            {
                return false;
            }
        }

        return true;

    }

    bool StageOutCheck(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0);
    }

    bool MinoCheck(int x, int y, Mino mino)
    {
        return (grid[x, y] != null && grid[x, y].parent != mino.transform);
    }

    public void SaveMinoInGrid(Mino mino)
    {
        foreach (Transform item in mino.transform)
        {
            Vector2 pos = Rounding.Round(item.position);

            grid[(int)pos.x, (int)pos.y] = item;
        }
    }

    public void ClearAllRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsComplete(y))
            {
                ClearRow(y);

                ShiftRowsDown(y + 1);

                y--;
            }

        }
    }

    bool IsComplete(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y].gameObject);
            }
            grid[x, y] = null;
        }
    }

    void ShiftRowsDown(int startY)
    {
        for (int y = startY; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x,y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    public bool OverLimit(Mino mino)
    {
        foreach (Transform item in mino.transform)
        {
            if (item.transform.position.y >= (height - header))
            {
                return true;
            }
        }

        return false;
    }
}
