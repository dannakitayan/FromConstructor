using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class ResourcesLoader
{
    public static void GetMaterial(string name, Action<Material> onSetMaterial)
    {
        Addressables.LoadAssetAsync<Material>($"{name}.mat").Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                onSetMaterial.Invoke(op.Result);
            }
            else
            {
                Debug.Log($"Get a material result error: {op.OperationException}");
            }
        };
    }

    public static void GetPAK(string name, Action<PAK> onSetPAK)
    {
        Addressables.LoadAssetAsync<PAK>($"{name}.asset").Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                onSetPAK.Invoke(op.Result);
            }
            else
            {
                Debug.Log($"Get a PAK result error: {op.OperationException}");
            }
        };
    }

    public static void GetLevel(string name, Action<TextAsset> onSetLevel)
    {
        Addressables.LoadAssetAsync<TextAsset>($"{name}.txt").Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                onSetLevel.Invoke(op.Result);
            }
            else
            {
                Debug.Log($"Get a level result error: {op.OperationException}");
            }
        };
    }

    public static void GetLevelPrefab(string name, Action<GameObject> onSetLevelPrefab)
    {
        Addressables.LoadAssetAsync<GameObject>($"Assets/GameResources/Objects/Levels/{name}.prefab").Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                onSetLevelPrefab.Invoke(op.Result);
            }
            else
            {
                Debug.Log($"Get a level prefab result error: {op.OperationException}");
            }
        };
    }
}
