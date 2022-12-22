using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos
{
	[ExecuteAlways]
	public abstract class ComponentGizmo<SourceType> : AttachedGizmo<SourceType> where SourceType : Component
	{
		/** Automatically set the source to the nearest match on same the GameObject if not set already */
		public void FindSource()
		{
			this.Source ??= this.GetComponent<SourceType>();
		}

		private void Awake()
		{
			this.FindSource();
		}
	}
}
