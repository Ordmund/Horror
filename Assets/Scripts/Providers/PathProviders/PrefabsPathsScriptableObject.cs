using System;
using System.Collections.Generic;
using ColoredLogger;
using Constants.Logs;
using Core.MVC;
using UnityEngine;

namespace Providers
{
    public class PrefabsPathsScriptableObject : ScriptableObject
    {
        [SerializeField] private List<PathNamePair> paths = new();

        public string GetPath<T>() where T : BaseView
        {
            var typeName = typeof(T).Name;
            
            foreach (var pathNamePair in paths)
            {
                if (pathNamePair.viewClassName == typeName)
                    return pathNamePair.path;
            }
            
            $"Path for {typeName} not found".Error(LogColor.Red, LogChannel.ScriptableObjects);
            
            return string.Empty;
        }
    }

    [Serializable]
    public class PathNamePair
    {
        public string viewClassName;
        public string path;
    }
}