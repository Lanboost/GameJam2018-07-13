using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SpawnWavesEditor : EditorWindow
{

    public SpawnWaves spawnwaves;
    private int viewIndex = 1;

    [MenuItem("Window/Spawn Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(SpawnWavesEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            spawnwaves = AssetDatabase.LoadAssetAtPath(objectPath, typeof(SpawnWaves)) as SpawnWaves;
        }

    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Spawner Editor", EditorStyles.boldLabel);
        /*if (spawnwaves != null)
        {
            if (GUILayout.Button("Show Item List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = spawnwaves;
            }
        }*/
        if (GUILayout.Button("Open Item List"))
        {
            OpenItemList();
        }/*
        if (GUILayout.Button("New Item List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = spawnwaves;
        }*/
        GUILayout.EndHorizontal();

        if (spawnwaves == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false)))
            {
                CreateNewItemList();
            }
            /*if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false)))
            {
                OpenItemList();
            }*/
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (spawnwaves != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < spawnwaves.waves.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (spawnwaves.waves == null)
                Debug.Log("wtf");
            if (spawnwaves.waves.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, spawnwaves.waves.Count);
                EditorGUILayout.LabelField("of   " + spawnwaves.waves.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                spawnwaves.waves[viewIndex - 1].monster = EditorGUILayout.ObjectField("Monster Prefab", spawnwaves.waves[viewIndex - 1].monster, typeof(GameObject), false) as GameObject;
                spawnwaves.waves[viewIndex - 1].count = EditorGUILayout.IntField("count", spawnwaves.waves[viewIndex - 1].count);
                spawnwaves.waves[viewIndex - 1].delay = EditorGUILayout.IntField("delay", spawnwaves.waves[viewIndex - 1].delay);
                spawnwaves.waves[viewIndex - 1].interval = EditorGUILayout.IntField("interval", spawnwaves.waves[viewIndex - 1].interval);

                GUILayout.Space(10);

            }
            else
            {
                GUILayout.Label("This Inventory List is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(spawnwaves);
        }
    }

    void CreateNewItemList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        spawnwaves = CreateSpawnWavesList.Create();
        if (spawnwaves)
        {
            spawnwaves.waves = new List<SpawnWave>();
            string relPath = AssetDatabase.GetAssetPath(spawnwaves);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Spawner List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            spawnwaves = AssetDatabase.LoadAssetAtPath(relPath, typeof(SpawnWaves)) as SpawnWaves;
            if (spawnwaves.waves == null)
                spawnwaves.waves = new List<SpawnWave>();
            if (spawnwaves)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem()
    {
        SpawnWave newItem = new SpawnWave();
        //newItem.itemName = "New Item";
        spawnwaves.waves.Add(newItem);
        viewIndex = spawnwaves.waves.Count;
    }

    void DeleteItem(int index)
    {
        spawnwaves.waves.RemoveAt(index);
    }
}