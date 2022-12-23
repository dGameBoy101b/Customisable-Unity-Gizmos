using System;
using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos.Parts
{
	[Serializable]
	public abstract class GizmoPart
	{
		/**
		 * The name of this part shown as a header in the inspector
		 */
		[SerializeField]
		public readonly string Name;

		/**
		 * Whether this part should be drawn
		 */
		[HideInInspector]
		public bool ShouldDraw = true;

		/**
		 * Constructor
		 * @param name The header name used for this part
		 * @param should_draw Whether this should start drawn
		 */
		public GizmoPart(string name, bool should_draw = true)
		{
			this.Name = name;
			this.ShouldDraw = should_draw;
		}

		/**
		 * Draw this gizmo part
		 */
		public abstract void Draw();
	}
}
