using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    const int step = 2;
    const char nextLine = '\n';
    const char emptySpace = 'x';
    TextAsset levelAsset;
    PAK DefaultCollection;
    Map objects;
    List<Transform> walls = new List<Transform>();
    GameObject ground;
    GameObject door;

    //[Range(1,20)]
    public int LevelNumber = 0;

    public static Action onNextLevelLoad;

    const string levelsPath = "Assets/GameResources/Levels/game";
    const string ceilingsPath = "Assets/Materials/CeilingsMaterials/ceiltex";
    const string floorsPath = "Assets/Materials/FloorsMaterials/floortex";
    const string doorsPath = "Assets/Materials/Doors/Default/doortex";
    const string doorwaysPath = "Assets/Materials/Doors/Doorways/doorway1";
    const string paksPath = "Assets/PAKs/";


    void Start()
    {
        StartCoroutine(BuildNewScene(true));
    }

    void Awake()
    {
        onNextLevelLoad += BuildNewScene;
    }

    void OnDestroy()
    {
        onNextLevelLoad -= BuildNewScene;
    }

    void BuildNewScene()
    {
        DestroyLastScene();
        StartCoroutine(BuildNewScene(false));
    }

    IEnumerator BuildNewScene(bool isFirstStart)
    {
        LevelNumber += 1;
        if(isFirstStart)
        {
            yield return StartCoroutine(LoadBase("PAK1"));
        }
        yield return StartCoroutine(LevelBuild(LevelNumber));
        yield return StartCoroutine(BuildLevel());
        yield return StartCoroutine(Optimisation());
    }

    IEnumerator LoadBase(string pakName)
    {
        ResourcesLoader.GetPAK($"{paksPath}{pakName}", (pak) =>
        {
            DefaultCollection = pak;
            objects = new Map(DefaultCollection);
        });
        yield return new WaitUntil(() => objects != null);
    }

    IEnumerator LevelBuild(int level)
    {
        levelAsset = null;
        Material floorMaterial = null, ceilingMaterial = null, doorMaterial = null, doorwayMaterial = null;

        //Current level floor material;
        ResourcesLoader.GetMaterial($"{floorsPath}{level}", (material) =>
        {
            floorMaterial = material;
        });
        yield return new WaitUntil(() => floorMaterial != null);
        Debug.Log("Current level floor material;" + $" {level}");
        //Current level ceiling material;
        ResourcesLoader.GetMaterial($"{ceilingsPath}{level}", (material) =>
        {
            ceilingMaterial = material;
        });
        yield return new WaitUntil(() => ceilingMaterial != null);
        Debug.Log("Current level ceiling material;" + $" {level}");

        //Current level door material;
        ResourcesLoader.GetMaterial($"{doorsPath}{level}", (material) =>
        {
            doorMaterial = material;
        });
        yield return new WaitUntil(() => doorMaterial != null);
        Debug.Log("Current level door material;" + $" {level}");

        //Current level doorway material;
        ResourcesLoader.GetMaterial(doorwaysPath, (material) =>
        {
            doorwayMaterial = material;
        });
        yield return new WaitUntil(() => doorwayMaterial != null);
        Debug.Log("Current level doorway material;" + $" {level}");

        //Current level text asset;
        ResourcesLoader.GetLevel($"{levelsPath}{level}", (textasset) =>
        {
            levelAsset = textasset;
        });
        yield return new WaitUntil(() => levelAsset != null);
        Debug.Log("Current level text asset;" + $" {level}" + "\n" + "---------");


        ground = BuildCurrentLevelObject(DefaultCollection.Ground, (floorMaterial, ceilingMaterial));
        door = BuildCurrentLevelObject(DefaultCollection.Door, (doorMaterial, doorwayMaterial));
        yield return null;
    }

    void DestroyLastScene()
    {
        GameObject obj = GameObject.Find($"game{LevelNumber}");

        if (obj != null)
        {
            Destroy(obj);
        }
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

    GameObject BuildCurrentLevelObject(GameObject baseObject, (Material, Material) materials)
    {
        var script = baseObject.GetComponent<BuildTexturesForPrefab>();
        script.SetMaterials(materials.Item1, materials.Item2);
        return baseObject;
    }

    IEnumerator Optimisation()
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

        yield return null;
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

    IEnumerator BuildLevel()
    {
        var charArray = levelAsset.text.ToCharArray();

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
                                BuildDoor(door, -x, y, levelParent.transform, charArray[i - 1]); //A negative index may appear here, you need to correct;
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
        yield return null;
    }
}
