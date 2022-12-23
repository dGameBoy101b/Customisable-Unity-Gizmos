using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos.Parts
{
	public class RigidbodyVelocityCompositeGizmo : ComponentGizmo<Rigidbody>
	{
		[SerializeField]
		private GizmoPartCollection _parts = new GizmoPartCollection(
			new RayGizmoPart("Linear Velocity", true, Color.yellow),
			new RayGizmoPart("Angular Velocity", true, Color.cyan)
		);

		protected override void Draw()
		{
			this._parts.DrawAll();
		}
	}
}
