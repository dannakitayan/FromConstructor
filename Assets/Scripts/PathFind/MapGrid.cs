using System.Collections.Generic;
using UnityEngine;

public static class MapGrid
{
    const int step = 2;

    static readonly List<char> solidObjects = new List<char>()
    { 
        '', '', '', '', '', '', '', '', '', '',
        '', '', '', '', '', '', ' ', '!', '"', '#',
        '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-',
        '.', '/', '0', '1', '2', '3', '4', '5', '6', '7',
        'x', '\n'
    };

    public static List<Vector2> OpenMap = new List<Vector2>();

    public static void GenerateCells(char[] array)
    {
        int i = 0;
        for (int y = 0; y < 66; y++)
        {
            for (int x = 0; x < 66; x++)
            {
                if (i < array.Length)
                {
                    if (y < 65 && x < 64)
                    {
                        if (solidObjects.Contains(array[i])) continue;
                        else OpenMap.Add(new Vector2(-x * step, y * step));
                    }
                }
                i += 1;
            }
        }
    }
}
