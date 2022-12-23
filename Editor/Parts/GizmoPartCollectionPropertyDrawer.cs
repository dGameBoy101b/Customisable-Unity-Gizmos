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
			foreach (var ele in property)
				elements.Add("\t\"" + ele.ToString() + "\"\n");
			Debug.Log("{\n" + string.Join(" ", elements) + "}");
			Debug.Log(elements);
			foreach (SerializedProperty part in property.FindPropertyRelative("_parts"))
			{
				base.OnGUI(position, part, label);
			}
		}
	}
}
