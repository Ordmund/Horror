#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using ColoredLogger;
using Constants.Logs;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace ScriptableObjects
{
    public class ScriptableObjectsCreator : EditorWindow
    {
        private DefaultAsset _targetFolder;
        private Type[] _scriptableObjects;
        private string _scriptableObjectName;
        private int _selectedIndex;

        [MenuItem("Window/ScriptableObjects/Creator")]
        private static void OpenWindow()
        {
            var window = GetWindow<ScriptableObjectsCreator>();
            if (window)
                window.GetScriptableObjects();
        }

        private void GetScriptableObjects()
        {
            var currentAssembly = typeof(SceneContext).Assembly;
            var scriptableObjectType = typeof(ScriptableObject);
            _scriptableObjects = currentAssembly.GetTypes().Where(classType => scriptableObjectType.IsAssignableFrom(classType)).ToArray();
        }

        private void OnGUI()
        {
            _targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Select Folder", _targetFolder, typeof(DefaultAsset), false);
            _scriptableObjectName = EditorGUILayout.TextField("Name: ", _scriptableObjectName);
            _selectedIndex = EditorGUILayout.Popup("ScriptableObject", _selectedIndex, _scriptableObjects.Select(type => type.Name).ToArray());

            if (GUILayout.Button("Create"))
            {
                var assetPath = AssetDatabase.GetAssetPath(_targetFolder);
                var fullPath = Path.Combine(assetPath, _scriptableObjectName + ".asset");
                if (!HasErrors(assetPath, fullPath))
                    CreateScriptableObject(fullPath);
            }
        }

        private void CreateScriptableObject(string fullPath)
        {
            var asset = CreateInstance(_scriptableObjects[_selectedIndex]);
            AssetDatabase.CreateAsset(asset, fullPath);

            $"{_scriptableObjects[_selectedIndex]} successfully created!".Log(LogColor.LimeGreen);
        }

        private bool HasErrors(string assetPath, string fullPath)
        {
            if (string.IsNullOrEmpty(_scriptableObjectName))
            {
                "Name of a ScriptableObject is empty!".Error(channel: LogChannel.ScriptableObjects);
                return true;
            }

            if (string.IsNullOrEmpty(assetPath))
            {
                "Folder not selected!".Error(channel: LogChannel.ScriptableObjects);
                return true;
            }

            if (File.Exists(fullPath))
            {
                $"ScriptableObject with name {_scriptableObjectName} already exist by path {assetPath}!".Error(channel: LogChannel.ScriptableObjects);
                return true;
            }

            if (_scriptableObjects.Length != 0) return false;

            "Not found any available ScriptableObject to create!".Error(channel: LogChannel.ScriptableObjects);
            return true;
        }
    }
}
#endif