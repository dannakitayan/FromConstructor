using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
public class LoadLevel : MonoBehaviour
{
    const int step = 2;
    const char nextLine = '\n';
    const char emptySpace = 'x';
    TextAsset levelAsset;
    [SerializeField] PAK defaultCollection;
    Map objects;
    List<Transform> walls = new List<Transform>();
    List<GameObject> wallsForDelete = new List<GameObject>();
    GameObject ground;
    GameObject door;

    [Range(1, 20)]
    public int LevelNumber = 1;
    int lastLevelNumber = 0;

    public static Action onNextLevelLoad;

    const string levelsPath = "Assets/GameResources/Levels/game";
    const string ceilingsPath = "Assets/Materials/CeilingsMaterials/ceiltex";
    const string floorsPath = "Assets/Materials/FloorsMaterials/floortex";
    const string doorsPath = "Assets/Materials/Doors/Default/doortex";
    const string doorwaysPath = "Assets/Materials/Doors/Doorways/doorway1";
    const string paksPath = "Assets/PAKs/";


    public async void BuildLevelInEditor()
    {
        if (objects == null) objects = new Map(defaultCollection);
        await LevelBuild();
        BuildLevel();
        Optimisation();
        walls.Clear();
        wallsForDelete.Clear();
        DestroyLastScene();
        lastLevelNumber = LevelNumber;
    }

    void Awake()
    {
        //onNextLevelLoad += BuildNewScene;
    }

    void OnDestroy()
    {
        //onNextLevelLoad -= BuildNewScene;
    }

    //void BuildNewScene()
    //{
    //    DestroyLastScene();
    //    StartCoroutine(BuildNewScene(false));
    //}

    //IEnumerator BuildNewScene(bool isFirstStart)
    //{
    //    Debug.Log("Start building");
    //    if (isFirstStart)
    //    {
    //        yield return StartCoroutine(LoadBase("PAK1"));
    //    }
    //    yield return StartCoroutine(LevelBuild(LevelNumber));
    //    yield return StartCoroutine(BuildLevel());
    //    Optimisation();
    //}

    async Task LevelBuild()
    {
        levelAsset = null;
        Material floorMaterial = null, ceilingMaterial = null, doorMaterial = null, doorwayMaterial = null;

        //Current level floor material;
        TaskCompletionSource<bool> flooLoaded = new TaskCompletionSource<bool>();
        ResourcesLoader.GetMaterial($"{floorsPath}{LevelNumber}", (material) =>
        {
            floorMaterial = material;
            flooLoaded.SetResult(true);
        });
        await flooLoaded.Task;
        Debug.Log("Current level floor material;" + $" {LevelNumber}");

        //Current level ceiling material;
        TaskCompletionSource<bool> ceilingLoaded = new TaskCompletionSource<bool>();
        ResourcesLoader.GetMaterial($"{ceilingsPath}{LevelNumber}", (material) =>
        {
            ceilingMaterial = material;
            ceilingLoaded.SetResult(true);
        });
        await ceilingLoaded.Task;
        Debug.Log("Current level ceiling material;" + $" {LevelNumber}");

        //Current level door material;
        TaskCompletionSource<bool> doorLoaded = new TaskCompletionSource<bool>();
        ResourcesLoader.GetMaterial($"{doorsPath}{LevelNumber}", (material) =>
        {
            doorMaterial = material;
            doorLoaded.SetResult(true);
        });
        await doorLoaded.Task;
        Debug.Log("Current level door material;" + $" {LevelNumber}");

        //Current level doorway material;
        TaskCompletionSource<bool> doorwayLoaded = new TaskCompletionSource<bool>();
        ResourcesLoader.GetMaterial(doorwaysPath, (material) =>
        {
            doorwayMaterial = material;
            doorwayLoaded.SetResult(true);
        });
        await doorwayLoaded.Task;
        Debug.Log("Current level doorway material;" + $" {LevelNumber}");

        //Current level text asset;
        TaskCompletionSource<bool> levelLoaded = new TaskCompletionSource<bool>();
        ResourcesLoader.GetLevel($"{levelsPath}{LevelNumber}", (textasset) =>
        {
            levelAsset = textasset;
            levelLoaded.SetResult(true);
        });
        await levelLoaded.Task;
        Debug.Log("Current level text asset;" + $" {LevelNumber}" + "\n" + "---------");


        ground = BuildCurrentLevelObject(defaultCollection.Ground, (floorMaterial, ceilingMaterial));
        door = BuildCurrentLevelObject(defaultCollection.Door, (doorMaterial, doorwayMaterial));
    }

    void DestroyLastScene()
    {
        if (lastLevelNumber == LevelNumber) return;
        GameObject obj = GameObject.Find($"game{lastLevelNumber}");

        if (obj != null)
        {
            DestroyImmediate(obj);
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
                    wallsForDelete.Add(element.gameObject);
                }
            }
            //break;
        }

        foreach(GameObject wall in wallsForDelete)
        {
            DestroyImmediate(wall);
        }
        Debug.Log("Optimisation end");
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
                            case 'h':
                            case 'i':
                            case 'j':
                            case 'k':
                            case 'l':
                                CreateEntity(newObject, -x, y, levelParent.transform, 0);
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