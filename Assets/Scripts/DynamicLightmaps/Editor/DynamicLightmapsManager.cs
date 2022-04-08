using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace DynamicLightmaps
{
    public class DynamicLightmapsManager : EditorWindow
    {
        [MenuItem("Window/Dynamic Lightmaps")]
        private static void ShowWindow()
        {
            GetWindow<DynamicLightmapsManager>("Dynamic Lightmaps");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Add Map Data Attacher to Selections")) AddDataAttacherToSelections();
            stateName = EditorGUILayout.TextField("Light State Name", stateName);
            if (GUILayout.Button("Save Current Light State")) SaveCurrentLightState();
        }

        private static void AddDataAttacherToSelections()
        {
            var selectedObjects = Selection.gameObjects;
            foreach (var selectedObject in selectedObjects)
            {
                foreach (var meshRenderer in selectedObject.GetComponentsInChildren<MeshRenderer>())
                {
                    var attacher = meshRenderer.GetComponent<MapDataAttacher>();
                    if (attacher == null)
                        attacher = Undo.AddComponent<MapDataAttacher>(meshRenderer.gameObject);

                    if (attacher.GUID == string.Empty)
                    {
                        var so = new SerializedObject(attacher);
                        so.FindProperty("guid").stringValue = GUID.Generate().ToString();
                        so.ApplyModifiedProperties();
                    }
                }

                if (PrefabUtility.IsPartOfAnyPrefab(selectedObject))
                    PrefabUtility.ApplyPrefabInstance(selectedObject, InteractionMode.UserAction);
            }
        }

        private static bool CheckDataAttachers()
        {
            var result = FindObjectsOfType<MeshRenderer>().Where(x => x.gameObject.isStatic);
            foreach (var temp in result)
            {
                if (temp.GetComponent<MapDataAttacher>() == null)
                    return false;
            }
            return true;
        }

        private static string stateName = "";
        private static void SaveCurrentLightState()
        {
            // Checking Data Attachers in Scene
            if (!CheckDataAttachers())
            {
                throw new System.Exception("Some objects in the scene don't have data attacher");
            }

            var state = CreateInstance<LightState>(); state.name = stateName;
            var scenePath = SceneManager.GetActiveScene().path.Replace(".unity", "");
            var statePath = $"Assets/Light States/{stateName}";

            // checking LightState main folder
            if (!AssetDatabase.IsValidFolder("Assets/Light States"))
                AssetDatabase.CreateFolder("Assets", "Light States");

            // removing old state with the same name
            AssetDatabase.DeleteAsset(statePath);
            AssetDatabase.CreateFolder("Assets/Light States", stateName);

            // gettings GUID for the lightmaps
            var directionMapsGUID = AssetDatabase.FindAssets("_comp_dir", new string[] { scenePath });
            var colorMapsGUID = AssetDatabase.FindAssets("_comp_light", new string[] { scenePath });

            // saving directional maps
            state.directionMaps = new Texture2D[directionMapsGUID.Length];
            var i = 0;
            foreach (var direction in directionMapsGUID)
            {
                var source = AssetDatabase.GUIDToAssetPath(direction);
                var li = source.LastIndexOf('/');
                var destination = statePath + "/" + source.Substring(li + 1);

                AssetDatabase.CopyAsset(source, destination);
                state.directionMaps[i++] = AssetDatabase.LoadAssetAtPath<Texture2D>(destination);
            }

            // saving color maps
            state.colorMaps = new Texture2D[colorMapsGUID.Length];
            i = 0;
            foreach (var color in colorMapsGUID)
            {
                var source = AssetDatabase.GUIDToAssetPath(color);
                var li = source.LastIndexOf('/');
                var destination = statePath + "/" + source.Substring(li + 1);

                AssetDatabase.CopyAsset(source, destination);
                state.colorMaps[i++] = AssetDatabase.LoadAssetAtPath<Texture2D>(destination);
            }

            // saving light probes' spherical harmonics
            state.lightProbesData = new SphericalHarmonicsL2[LightmapSettings.lightProbes.bakedProbes.Length];
            LightmapSettings.lightProbes.bakedProbes.CopyTo(state.lightProbesData, 0);

            // saving MapData for all the attachers
            var attachers = FindObjectsOfType<MapDataAttacher>();
            state.mapData = new MapData[attachers.Length];
            i = 0;
            foreach (var attacher in attachers)
            {
                state.mapData[i++] = attacher.GetObjectMapData();
            }

            // saving the state into the drive
            AssetDatabase.CreateAsset(state, statePath + $"/{stateName}" + ".asset");
            AssetDatabase.SaveAssets();
        }
    }
}
