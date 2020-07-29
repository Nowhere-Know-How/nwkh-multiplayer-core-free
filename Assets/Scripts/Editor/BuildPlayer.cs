#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;


// Output the build size or a failure depending on BuildPlayer.

public class BuildPlayer
{
    static string project_name = "MyFirstNakamas";
    static string out_dir = @"bin\";

    static string exe_name = out_dir + project_name + @".exe";

    static string[] scenes = new[] {
            @"Assets\HappyMaki\Scenes\0-BootupScene.unity",
            @"Assets\HappyMaki\Scenes\1-MainMenu.unity",
            @"Assets\HappyMaki\Scenes\2-GlobalObjects.unity",
            @"Assets\HappyMaki\Scenes\Lentils.unity",
            @"Assets\HappyMaki\Scenes\Legume.unity",
            @"Assets\HappyMaki\Scenes\Kerfuffle.unity"
        };

    [MenuItem("Build/Development %b")]
    public static void ClientBuild()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;

        buildPlayerOptions.locationPathName = exe_name;
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.Development;
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Development build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Development build failed");
        }
    }



}
#endif
