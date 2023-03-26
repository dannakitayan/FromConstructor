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
    PAK DefaultCollection;
    Map objects;
    List<Transform> walls = new List<Transform>();
    GameObject ground;

    [Range(1,20)]
    public int LevelNumber;


    void Start()
    {
        DefaultCollection = Resources.Load<PAK>("PAK1");
        objects = new Map(DefaultCollection);
        ground = BuildCurrentLevelGround(DefaultCollection.Ground);

        BuildLevel();
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

    GameObject BuildCurrentLevelGround(GameObject baseObject)
    {
        Material floor = Resources.Load<Material>($"Materials/FloorsMaterials/floortex{LevelNumber}");
        Material ceiling = Resources.Load<Material>($"Materials/CeilingsMaterials/ceiltex{LevelNumber}");
        var script = baseObject.GetComponent<BuildGround>();
        script.SetMaterials(floor, ceiling);
        DestroyImmediate(script);
        return baseObject;
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

        walls.Clear();
    }

    void BuildLevel()
    {
        var asset = Resources.Load<TextAsset>($"Levels/game{LevelNumber}");
        var charArray = asset.text.ToCharArray();

        var levelParent = new GameObject($"game{LevelNumber}");

        int i = 0;
        for (int y = 0; y < 66; y++)
        {
            for (int x = 0; x < 66; x++)
            {
                if (i < charArray.Length)
                {
                    if (y < 65 && x < 64)
                    {
                        var newObject = objects.GetObject(charArray[i]);
                        switch (charArray[i])
                        {
                            case nextLine:
                                break;
                            case emptySpace:
                                CreateEntity(ground, -x, y, levelParent.transform, 1);
                                break;
                            default:
                                CreateEntity(newObject, -x, y, levelParent.transform, 1);
                                break;
                        }
                    }

                }
                i += 1;
            }
        }
    }
}
