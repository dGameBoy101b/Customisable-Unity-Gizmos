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
		[SerializeReference]
		private GizmoPart[] _parts;

		public GizmoPartCollection(params GizmoPart[] parts)
		{
			this._parts = parts;
		}

		public GizmoPart this[int index] => this._parts[index];

		public int Count => this._parts.Length;

		public IEnumerator<GizmoPart> GetEnumerator()
		{
			return this._parts.GetEnumerator() as IEnumerator<GizmoPart>;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._parts.GetEnumerator();
		}

		/**
		 * Draw all the parts in this collection
		 */
		public void DrawAll()
		{
			foreach (var part in this._parts)
				if (part.ShouldDraw)
					part.Draw();
		}


		/**
		 * Returns the first part matching the given type
		 * @tparam PartType The type of gizmo part to search for
		 * @return The first part matching the given type, or null otherwise
		 */
		public PartType GetPart<PartType>() where PartType : GizmoPart
		{
			foreach (var part in this._parts)
				if (part is PartType)
					return part as PartType;
			return null;
		}

		/**
		 * Returns all parts matching the given type
		 * @tparam PartType The type of gizmo part to search for
		 * @return All parts matching the given type
		 */
		public List<PartType> GetParts<PartType>() where PartType : GizmoPart
		{
			var parts = new List<PartType>();
			foreach (var part in this._parts)
				if (part is PartType)
					parts.Add(part as PartType);
			return parts;
		}

		/**
		 * Returns the first part matching the given name and type
		 * @tparam PartType The type of gizmo part to search for
		 * @param name The name to search for
		 * @return The first part matching the given name and type, or null otherwise
		 */
		public PartType GetPartByName<PartType>(string name) where PartType : GizmoPart
		{
			foreach (var part in this._parts)
				if (part is PartType && name == part.Name)
					return part as PartType;
			return null;
		}
	}
}
