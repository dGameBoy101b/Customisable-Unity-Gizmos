using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos
{
	public abstract class Gizmo : MonoBehaviour
	{
		public enum SelectionDrawMode : short
		{
			None = 0,
			Selected = 1,
			Always = 3
		}

		[SerializeField]
		[Tooltip("When this should be drawn with respect to gizmo selection")]
		public SelectionDrawMode SelectionMode;

		protected abstract void Draw();

		public virtual bool ShouldDraw()
		{
			return true;
		}

		private void OnDrawGizmos()
		{
			if (this.SelectionMode == SelectionDrawMode.Always && this.ShouldDraw())
				this.Draw();
		}

		private void OnDrawGizmosSelected()
		{
			if (this.SelectionMode == SelectionDrawMode.Selected && this.ShouldDraw())
				this.Draw();
		}
	}
}
