using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos
{
	public abstract class AttachedGizmo<SourceType> : Gizmo
	{
		[SerializeField]
		[Tooltip("The component this visualises information from")]
		private SourceType _source;

		/** The component this visualises information from */
		public SourceType Source
		{
			get => this._source;
			set => this._source = value;
		}

		public override bool ShouldDraw()
		{
			return this.Source != null;
		}
	}
}
