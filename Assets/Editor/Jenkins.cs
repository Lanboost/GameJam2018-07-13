using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class Jenkins
{
    static string[] SCENES = FindEnabledEditorScenes();

    static string APP_NAME = "GameJam";

    [MenuItem("Custom/BUILD WEBGL")]
    static void PerformWebgl()
    {
        string TARGET_DIR = "webgl";
        string target_dir = APP_NAME + "";
        GenericBuild(SCENES, TARGET_DIR, BuildTargetGroup.WebGL, BuildTarget.WebGL, BuildOptions.None);
    }

    [MenuItem("Custom/BUILD WINDOWS")]
    static void PerformWinStandalone()
    {
        string TARGET_DIR = "standalone";
        string target_dir = APP_NAME + "";
        GenericBuild(SCENES, TARGET_DIR + "/"+ APP_NAME+".exe", BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTargetGroup group, BuildTarget build_target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(group, build_target);
        BuildPlayerOptions op = new BuildPlayerOptions();
        op.scenes = scenes;
        op.targetGroup = group;
        op.target = build_target;
        op.locationPathName = target_dir;
        

        var res = BuildPipeline.BuildPlayer(op);
        if (res.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            throw new Exception("BuildPlayer failure: " + res);
        }
    }
}