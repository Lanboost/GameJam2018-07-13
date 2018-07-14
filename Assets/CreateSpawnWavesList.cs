using UnityEngine;
using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;

public class CreateSpawnWavesList
{
    [MenuItem("Assets/Create/Data/Inventory Item List")]
    public static SpawnWaves Create()
    {
        SpawnWaves asset = ScriptableObject.CreateInstance< SpawnWaves>();

        AssetDatabase.CreateAsset(asset, "Assets/spawnwaves.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
#endif