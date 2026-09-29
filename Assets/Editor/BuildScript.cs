using UnityEditor;
using UnityEditor.SceneManagement;

// CLI: Unity -batchmode -quit -projectPath . -executeMethod BuildScript.BuildWindows
public static class BuildScript
{
    const string ScenePath = "Assets/Scenes/Main.unity";

    static void EnsureScene()
    {
        if (System.IO.File.Exists(ScenePath)) return;
        System.IO.Directory.CreateDirectory("Assets/Scenes");
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        EditorSceneManager.SaveScene(scene, ScenePath);
        AssetDatabase.Refresh();
    }

    static void Build(BuildTarget target, string path)
    {
        EnsureScene();
        var report = BuildPipeline.BuildPlayer(new[] { ScenePath }, path, target, BuildOptions.None);
        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            EditorApplication.Exit(1);
    }

    [MenuItem("Build/Windows")] public static void BuildWindows() => Build(BuildTarget.StandaloneWindows64, "Builds/Windows/Survivors.exe");
    [MenuItem("Build/Linux")] public static void BuildLinux() => Build(BuildTarget.StandaloneLinux64, "Builds/Linux/Survivors.x86_64");
    [MenuItem("Build/macOS")] public static void BuildMac() => Build(BuildTarget.StandaloneOSX, "Builds/macOS/Survivors.app");
    [MenuItem("Build/WebGL")] public static void BuildWebGL() => Build(BuildTarget.WebGL, "Builds/WebGL");
}
