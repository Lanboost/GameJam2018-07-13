using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR 
using UnityEditor;

public class CreateTowerBuildList
{
    [MenuItem("Assets/Create/Data/CreateTowerBuildList")]
    public static TowerBuildList Create()
    {
        TowerBuildList asset = ScriptableObject.CreateInstance<TowerBuildList>();

        AssetDatabase.CreateAsset(asset, "Assets/towerbuild.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
#endif