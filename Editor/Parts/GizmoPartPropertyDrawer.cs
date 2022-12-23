using UnityEngine;
using UnityEditor;

namespace dGameBoy101b.CustomisableGizmos.Parts
{
	[CustomPropertyDrawer(typeof(GizmoPart))]
	public class GizmoPartPropertyDrawer : PropertyDrawer
	{
		private bool foldout;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			this.foldout = EditorGUILayout.BeginFoldoutHeaderGroup(this.foldout, new GUIContent(property.FindPropertyRelative("Name").stringValue));
			if (this.foldout)
			{
				base.OnGUI(position, property, label);
			}
			EditorGUILayout.EndFoldoutHeaderGroup();
		}
	}
}
