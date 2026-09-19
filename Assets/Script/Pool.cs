using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Pool
{
    static readonly Dictionary<GameObject, Stack<GameObject>> pools = new();
    static readonly Dictionary<GameObject, GameObject> prefabOf = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Reset() { pools.Clear(); prefabOf.Clear(); SceneManager.sceneUnloaded += _ => Reset(); }

    public static GameObject Get(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (!pools.TryGetValue(prefab, out var stack)) pools[prefab] = stack = new();
        var go = stack.Count > 0 ? stack.Pop() : Object.Instantiate(prefab);
        prefabOf[go] = prefab;
        go.transform.SetPositionAndRotation(pos, rot);
        go.SetActive(true);
        return go;
    }

    public static void Release(GameObject go)
    {
        if (!go.activeSelf) return;
        go.SetActive(false);
        pools[prefabOf[go]].Push(go);
    }
}
