using UnityEditor;
using UnityEngine;

public class CustomAssetCreate
{
    [MenuItem("Assets/Create/Custom Asset/Input Data")]
    public static void CreateInputData()
    {
        ScriptableObject asset = ScriptableObject.CreateInstance(typeof(InputData));
        ProjectWindowUtil.CreateAsset(asset, "New InputData.asset");
    }
}