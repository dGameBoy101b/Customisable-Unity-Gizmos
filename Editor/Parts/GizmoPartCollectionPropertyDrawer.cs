using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace dGameBoy101b.CustomisableGizmos.Parts
{
	[CustomPropertyDrawer(typeof(GizmoPartCollection))]
	public class GizmoPartCollectionPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			List<string> elements = new List<string>();
			foreach (SerializedProperty ele in property)
				elements.Add("\t\"" + ele.name + "\"\n");
			Debug.Log("Gizmo Part Collection: {\n" + string.Join(" ", elements) + "}");
			property.Reset();
			Debug.Log("_parts type: \"" + (property.FindPropertyRelative("_parts")?.type ?? "null") + "\"");
		}
	}
}
