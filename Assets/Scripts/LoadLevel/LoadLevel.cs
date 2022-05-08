using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    const int step = 2;

    const char nextLine = '\n';
    const char emptySpace = 'x';

    public string LevelName;

    static PAK DefaultCollection;
    Map objects;

    [SerializeField]
    List<Transform> walls = new List<Transform>();

    void Start()
    {
        var asset = Resources.Load<TextAsset>($"Levels/{LevelName}");
        var charArray = asset.text.ToCharArray();
        DefaultCollection = Resources.Load<PAK>("PAK1");
        objects = new Map(DefaultCollection);

        var levelParent = new GameObject(LevelName);

        int i = 0;
        for (int y = 0; y < 64; y++)
        {
            for (int x = 0; x < 66; x++)
            {
                #region Fill
                if (i < charArray.Length)
                {
                    if(y > 0 && y < 63 && x > 0 && x < 63)
                    {
                        var newObject = objects.GetObject(charArray[i]);
                        switch (charArray[i])
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
                                break;                                
                            //Weapons;
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
                            //Enemies;
                            case 'h':
                                CreateEntity(newObject, x, y, levelParent.transform);
                                break;
                            case 'i':
                                CreateEntity(newObject, x, y, levelParent.transform);
                                break;
                            case 'j':
                                CreateEntity(newObject, x, y, levelParent.transform);
                                break;
                            case 'k':
                                CreateEntity(newObject, x, y, levelParent.transform);
                                break;
                            case 'l':
                                CreateEntity(newObject, x, y, levelParent.transform);
                                break;
                            default:
                                CreateEntity(newObject, x, y, levelParent.transform, 1);
                                break;
                        }
                    }
                    
                }
                #endregion
                i += 1;
            }
        }

        Optimisation();
    }

    void CreateEntity(GameObject newObject, int x, int y, Transform parent, int height = 0)
    {
        var entity = GameObject.Instantiate(newObject, new Vector3(x * step, height, y * step), Quaternion.identity, parent);
        if(height == 1)
        {
            var quads = entity.GetComponentsInChildren<MeshFilter>();
            foreach(var quad in quads)
            {
                walls.Add(quad.GetComponent<Transform>());
            }
        }
    }

    void Optimisation()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            var wall = walls[i];
            var elements = from w in walls
                           where (w.position == wall.position)
                           select w;
            if(elements is not null && elements.Count<Transform>() > 1)
            {
                foreach (var element in elements)
                {
                    Destroy(element.gameObject);
                }
            }
            //break;
        }
    }
}
