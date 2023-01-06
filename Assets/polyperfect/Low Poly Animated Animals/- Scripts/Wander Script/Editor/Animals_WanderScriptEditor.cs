using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Polyperfect.Common;

#if UNITY_EDITOR
namespace Polyperfect.Animals
{
    [CustomEditor(typeof(Animal_WanderScript))]
    [CanEditMultipleObjects]
    public class Animals_WanderScriptEditor : Common_WanderScriptEditor { }
}
#endif