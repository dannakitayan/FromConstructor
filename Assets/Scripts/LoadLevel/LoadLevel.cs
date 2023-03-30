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
    GameObject door;

    [Range(1,20)]
    public int LevelNumber;


    void Start()
    {
        DefaultCollection = Resources.Load<PAK>("PAK1");
        objects = new Map(DefaultCollection);
        ground = BuildCurrentLevelObject(DefaultCollection.Ground, GetMaterialsFromFolder("Materials/FloorsMaterials/floortex", "Materials/CeilingsMaterials/ceiltex"));
        door = BuildCurrentLevelObject(DefaultCollection.Door, GetMaterialsFromFolder("Materials/Doors/Default/doortex"));

        BuildLevel();
        Optimisation();
    }

    void CreateEntity(GameObject newObject, int x, int y, Transform parent, int height = 0, bool rotate = false)
    {
        GameObject entity = null;
        switch (rotate)
        {
            case false:
                entity = GameObject.Instantiate(newObject, new Vector3(x * step, height, y * step), Quaternion.identity, parent);
                break;
            case true:
                entity = GameObject.Instantiate(newObject, new Vector3(x * step, height, y * step), Quaternion.Euler(0, 90, 0), parent);
                break;
        }
        if (height == 1)
        {
            var quads = entity.GetComponentsInChildren<MeshFilter>();
            foreach(var quad in quads)
            {
                walls.Add(quad.GetComponent<Transform>());
            }
        }
    }

    (Material, Material) GetMaterialsFromFolder(string material1, string material2)
    {
        Material firstMaterial = Resources.Load<Material>($"{material1}{LevelNumber}");
        Material secondMaterial = Resources.Load<Material>($"{material2}{LevelNumber}");
        return (firstMaterial, secondMaterial);
    }

    (Material, Material) GetMaterialsFromFolder(string material1)
    {
        Material firstMaterial = Resources.Load<Material>($"{material1}{LevelNumber}");
        Material secondMaterial = Resources.Load<Material>($"Materials/Doors/Doorways/doorway1");
        return (firstMaterial, secondMaterial);
    }

    GameObject BuildCurrentLevelObject(GameObject baseObject, (Material, Material) materials)
    {
        var script = baseObject.GetComponent<BuildTexturesForPrefab>();
        script.SetMaterials(materials.Item1, materials.Item2);
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

    void BuildDoor(GameObject objectToSpawn, int x, int y, Transform parent, char symbol)
    {
        if (symbol != emptySpace)
        {
            CreateEntity(objectToSpawn, x, y, parent, 1);
        }
        else
        {
            CreateEntity(objectToSpawn, x, y, parent, 1, true);
        }
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
                                break;
                            case '8':
                                BuildDoor(door, -x, y, levelParent.transform, charArray[i - 1]);
                                break;
                            case '9':
                                BuildDoor(newObject, -x, y, levelParent.transform, charArray[i - 1]);
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

        CreateEntity(ground, 0, 0, levelParent.transform, 1);
    }
}
