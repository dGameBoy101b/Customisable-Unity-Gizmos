using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos.Parts
{
	[Serializable]
	public class GizmoPartCollection : IReadOnlyCollection<GizmoPart>, IEnumerable<GizmoPart>, IReadOnlyList<GizmoPart>
	{
		[SerializeField]
		private GizmoPart[] _parts;

		public GizmoPartCollection(params GizmoPart[] parts)
		{
			this._parts = parts;
		}

		public GizmoPart this[int index] => throw new NotImplementedException();

		public int Count => this._parts.Length;

		public IEnumerator<GizmoPart> GetEnumerator()
		{
			return this._parts.GetEnumerator() as IEnumerator<GizmoPart>;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._parts.GetEnumerator();
		}

		public void DrawAll()
		{
			foreach (var part in this._parts)
				if (part.ShouldDraw)
					part.Draw();
		}
	}
}
