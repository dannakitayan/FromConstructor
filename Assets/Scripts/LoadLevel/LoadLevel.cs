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
                    switch(charArray[i])
                    {
                        case nextLine:
                            break;
                        case emptySpace:
                            break;
                            //Sprites;
                        case '@':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'A':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'B':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'C':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'D':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'E':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'F':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'G':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'H':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'I':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                            //Ammunition;
                        case 'X':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'W':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            //Weapons;
                            break;
                        case 'Q':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'R':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'S':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'T':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'U':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        case 'V':
                            CreateEntity(newObject, x, y, levelParent.transform);
                            break;
                        default:
                            CreateEntity(newObject, x, y, levelParent.transform, 1);
                            break;
                    }
                    i += 1;
                }
            }
        }

        Debug.Log("all: " + i);
    }

    void CreateEntity(GameObject newObject, int x, int y, Transform parent, int height = 0)
    {
        GameObject.Instantiate(newObject, new Vector3(x * step, height, y * step), Quaternion.identity, parent);
    }
}
