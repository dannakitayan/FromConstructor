using System.Collections;
using System;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]string[] levelsNames;
    GameObject level;
    int levelNumber = 0;

    public static Action onLevelLoad;

    void Awake()
    {
        onLevelLoad += InstanceNextLevel;
    }

    void OnDestroy()
    {
        onLevelLoad -= InstanceNextLevel;
    }

    private void Start()
    {
        InstanceNextLevel();
    }

    void InstanceNextLevel()
    {
        Frames.onShowLoading?.Invoke();
        if(level != null)
        {
            Debug.Log(level.name);
            Destroy(level);
        }
        level = null;
        StartCoroutine(Loader(levelNumber));
    }

    IEnumerator Loader(int id)
    {
        ResourcesLoader.GetLevelPrefab(levelsNames[id], (prefab) =>
        {
            level = Instantiate(prefab);
        });
        yield return new WaitUntil(() => level != null);
        Frames.onShowUI?.Invoke();
        levelNumber += 1;
    }
}
