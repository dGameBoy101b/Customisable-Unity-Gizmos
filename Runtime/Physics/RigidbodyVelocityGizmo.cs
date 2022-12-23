using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos
{
	/**
	 * A gizmo that attaches to a rigidbody and visualises its angular and linear velocity
	 * @author dGameBoy101b
	 * @date 2022-12-23
	 */
	[AddComponentMenu("Customisable Gizmo/Physics/Rigidbody Velocity Gizmo")]
	public class RigidbodyVelocityGizmo : ComponentGizmo<Rigidbody>
	{
		[Header("Linear Velocity")]

		[SerializeField]
		[Tooltip("The colour used to draw the linear velocity of the attached rigidbody")]
		private Color _linearVelocityColour = Color.yellow;

		/**
		 * The colour used to draw the linear velocity of the attached rigidbody
		 */
		public Color LinearVelocityColour
		{
			get => this._linearVelocityColour;
			set => this._linearVelocityColour = value;
		}

		[SerializeField]
		[Tooltip("Scaling applied to the linear velocity ray")]
		private float _linearVelocityScale = 1f;

		/**
		 * Scaling applied to the linear velocity ray
		 */
		public float LinearVelocityScale
		{
			get => this._linearVelocityScale;
			set => this._linearVelocityScale = value;
		}

		[Header("Angular Velocity")]

		[SerializeField]
		[Tooltip("The colour used to draw the angular velocity of the attached rigidbody")]
		private Color _angularVelocityColour = Color.cyan;

		/**
		 * The colour used to draw the angular velocity of the attached rigidbody
		 */
		public Color AngularVelocityColour
		{
			get => this._angularVelocityColour;
			set => this._angularVelocityColour = value;
		}

		[SerializeField]
		[Tooltip("Scaling applied to the angular velocity ray")]
		private float _angularVelocityScale = 1f;

		/**
		 * Scaling applied to the angular velocity ray
		 */
		public float AngularVelocityScale
		{
			get => this._angularVelocityScale;
			set => this._angularVelocityScale = value;
		}

		protected override void Draw()
		{
			Vector3 center = this.Source.centerOfMass + this.Source.transform.position;
			Gizmos.color = this.LinearVelocityColour;
			Gizmos.DrawRay(center, this.Source.velocity * this.LinearVelocityScale);
			Gizmos.color = this.AngularVelocityColour;
			Gizmos.DrawRay(center, this.Source.angularVelocity * this.AngularVelocityScale);
		}
	}
}
