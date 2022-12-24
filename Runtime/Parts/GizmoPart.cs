using System;
using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos.Parts
{
	[Serializable]
	public abstract class GizmoPart
	{
		[SerializeField]
		private string _name;

		/**
		 * The name of this part shown as a header in the inspector
		 */
		public string Name
		{
			get => this._name;
			private set => this._name = value;
		}

		/**
		 * Whether this part should be drawn
		 */
		public bool ShouldDraw { get; set; } = true;

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
