using System;
using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos.Parts
{
	/**
	 * A gizmo part for a ray
	 * @author dGameBoy101b
	 * @date 2022-12-23
	 */
	[Serializable]
	public class RayGizmoPart : GizmoPart
	{
		[SerializeField]
		[Tooltip("The colour used to draw this ray")]
		private Color _colour;

		/**
		 * The colour used to draw this ray
		 */
		public Color Colour
		{
			get => this._colour;
			set => this._colour = value;
		}

		[SerializeField]
		[Tooltip("The scaling applied to this ray")]
		private float _scale = 1f;

		/**
		 * The scaling applied to this ray
		 */
		public float Scale
		{
			get => this._scale;
			set => this._scale = value;
		}

		/**
		 * The origin from which to draw this
		 */
		[HideInInspector]
		public Vector3 Origin = Vector3.zero;

		/**
		 * The direction in which to draw this
		 */
		[HideInInspector]
		public Vector3 Direction = Vector3.zero;

		public override void Draw()
		{
			Gizmos.color = this.Colour;
			Gizmos.DrawRay(this.Origin, this.Direction * this.Scale);
		}

		/**
		 * Constructor
		 * @param name The name of this part used as the header in the inspector
		 * @param should_draw Whether this start drawn
		 * @param colour The initial colour of the ray
		 * @param scale The initial scaling applied to the ray
		 */
		public RayGizmoPart(string name, bool should_draw = true, Color colour = new Color(), float scale = 1f): base(name, should_draw)
		{
			this.Colour = colour;
			this.Scale = scale;
		}
	}
}
