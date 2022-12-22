using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos
{
	public abstract class BehaviourGizmo<SourceType> : ComponentGizmo<SourceType> where SourceType : Behaviour
	{
		public enum EnabledDrawMode : byte
		{
			None = 0,
			EnabledOnly = 1,
			DisabledOnly = 2,
			Always = 3
		}

		[SerializeField]
		[Tooltip("When this should draw depending on whether the source behaviour is enabled")]
		private EnabledDrawMode _enabledMode = EnabledDrawMode.Always;

		/** When this should draw depending on whether the source behaviour is enabled */
		public EnabledDrawMode EnabledMode
		{
			get => this._enabledMode;
			set => this._enabledMode = value;
		}

		public override bool ShouldDraw()
		{
			return base.ShouldDraw() 
				&& (this.EnabledMode & (this.Source.enabled ? EnabledDrawMode.EnabledOnly : EnabledDrawMode.DisabledOnly)) > 0;
		}
	}
}
