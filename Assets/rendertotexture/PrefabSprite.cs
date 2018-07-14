using System.Collections;
using System.Collections.Generic;
using UnityEngine;




#if UNITY_EDITOR 
using UnityEditor;

public class CreatePrefabSprite
{
    [MenuItem("Assets/Create/Data/PrefabSprite")]
    public static PrefabSprite Create()
    {
        PrefabSprite asset = ScriptableObject.CreateInstance<PrefabSprite>();

        AssetDatabase.CreateAsset(asset, "Assets/prefabsprite.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
#endif

[System.Serializable]
public class PrefabSpriteItem
{
    public GameObject prefab;
    public RenderTexture rt;
}


public class PrefabSprite : ScriptableObject
{
    public List<PrefabSpriteItem> items;
}