using UnityEngine;

public static class SaveSystem
{
    [System.Serializable] public class Data { public float bestTime; public int bestKills; }

    public static Data Load() { try { return JsonUtility.FromJson<Data>(PlayerPrefs.GetString("save", "{}")); } catch { return new Data(); } }
    public static void Save(Data d) { try { PlayerPrefs.SetString("save", JsonUtility.ToJson(d)); PlayerPrefs.Save(); } catch { } }
}
