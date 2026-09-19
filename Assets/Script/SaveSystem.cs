using System.IO;
using UnityEngine;

public static class SaveSystem
{
    [System.Serializable] public class Data { public float bestTime; public int bestKills; }

    static string Path => Application.persistentDataPath + "/save.json";

    public static Data Load() => File.Exists(Path) ? JsonUtility.FromJson<Data>(File.ReadAllText(Path)) : new Data();
    public static void Save(Data d) => File.WriteAllText(Path, JsonUtility.ToJson(d));
}
