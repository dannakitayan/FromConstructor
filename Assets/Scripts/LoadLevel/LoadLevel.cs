using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    const int step = 2;

    const char nextLine = '\n';
    const char emptySpace = 'x';

    public string levelName;

    static PAK DefaultCollection;
    Map objects;

    void Start()
    {
        var asset = Resources.Load<TextAsset>("Levels/game1");
        var charArray = asset.text.ToCharArray();
        Debug.Log(charArray.Length);
        DefaultCollection = Resources.Load<PAK>("PAK1");
        objects = new Map(DefaultCollection);

        var levelParent = new GameObject(levelName);

        int i = 0;
        for (int y = 0; y < 65; y++)
        {
            for (int x = 0; x < 66; x++)
            {
                if(i < charArray.Length)
                {
                    var newObject = objects.GetObject(charArray[i]);
                    if (charArray[i] == nextLine)
                    {

                    }
                    else if(charArray[i] == emptySpace)
                    {
                        
                    }
                    else if (charArray[i] == '@' || charArray[i] == 'A' || charArray[i] == 'B' || charArray[i] == 'C' || charArray[i] == 'D' || charArray[i] == 'F' || charArray[i] == 'G' || charArray[i] == 'H' || charArray[i] == 'I')
                    {
                        GameObject.Instantiate(newObject, new Vector3(x * step, 0, y * step), Quaternion.identity, levelParent.transform);
                    }
                    else
                    {
                        GameObject.Instantiate(newObject, new Vector3(x * step, 1, y * step), Quaternion.identity, levelParent.transform);
                    }
                    i += 1;
                }
            }
        }

        Debug.Log("all: " + i);
    }
}
