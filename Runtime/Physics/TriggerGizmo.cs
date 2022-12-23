using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dGameBoy101b.CustomisableGizmos
{
	/**
	 * A customisable gizmo that tracks the colliders entering and exiting a trigger
	 * @author dGameBoy101b
	 * @date 2022-12-22
	 */
	[AddComponentMenu("Customisable Gizmo/Physics/Trigger Gizmo")]
	public class TriggerGizmo : Gizmo
	{
		[SerializeField]
		[Tooltip("The colour used to highlight colliders that entered a collision with this")]
		private Color _enterColour = Color.blue;

		/**
		 * The colour used to highlight colliders that entered a collision with this
		 */
		public Color EnterColour
		{
			get => this._enterColour;
			set => this._enterColour = value;
		}

		[SerializeField]
		[Tooltip("The colour used to highlight colliders that stayed in a collision with this")]
		private Color _stayColour = Color.magenta;

		/**
		 * The colour used to highlight colliders that stayed in a collision with this
		 */
		public Color StayColour
		{
			get => this._stayColour;
			set => this._stayColour = value;
		}

		[SerializeField]
		[Tooltip("The colour used to highlight colliders that exited a collision with this")]
		private Color _exitColour = Color.red;

		/**
		 * The colour used to highlight colliders that exited a collision with this
		 */
		public Color ExitColour
		{
			get => this._exitColour;
			set => this._exitColour = value;
		}

		[SerializeField]
		[Tooltip("The colour used to highlight colliders that have been destroyed while being tracked")]
		private Color _destroyColour = Color.grey;

		/**
		 * The colour used to highlight colliders that have been destroyed while being tracked
		 */
		public Color DestroyColour
		{
			get => this._destroyColour;
			set => this._destroyColour = value;
		}

		[SerializeField]
		[Tooltip("The number of seconds to wait before switching from the enter colour to the stay colour")]
		[Min(0f)]
		private float _enterDelay = 0.1f;

		/**
		 * The number of seconds to wait before switching from the enter colour to the stay colour
		 */
		public float EnterDelay 
		{
			get => this._enterDelay;
			set => Mathf.Max(0, value);
		}

		[SerializeField]
		[Tooltip("The number of seconds to wait before switching off the exit colour")]
		[Min(0f)]
		private float _exitDelay = 0.1f;

		/**
		 * The number of seconds to wait before switching off the exit colour
		 */
		public float ExitDelay
		{
			get => this._exitDelay;
			set => Mathf.Max(0, value);
		}

		/**
		 * An enumeration of the different phases of a collision
		 */
		public enum CollisionPhase
		{
			Enter,
			Stay,
			Exit,
			Destroyed
		}

		/**
		 * The collision phase of the colliders this is currently tracking
		 */
		private Dictionary<Collider, CollisionPhase> _colliders = new Dictionary<Collider, CollisionPhase>();

		/**
		 * The collision phase of the colliders this is currently tracking
		 */
		public IReadOnlyDictionary<Collider, CollisionPhase> Colliders
		{
			get => this._colliders;
		}

		/**
		 * The last known position of the colliders this is tracking
		 */
		private Dictionary<Collider, Vector3> _lastPositions = new Dictionary<Collider, Vector3>();

		/**
		 * The last known position of the colliders this is tracking
		 */
		public IReadOnlyDictionary<Collider, Vector3> LastPositions
		{
			get => this._lastPositions;
		}

		/**
		 * The coroutines this is executing to manage tracked colliders
		 */
		private Dictionary<Collider, Coroutine> _coroutines = new Dictionary<Collider, Coroutine>();

		protected override void Draw()
		{
			var start_position = this.transform.position;
			foreach (var pair in this.Colliders)
			{
				Color colour = this.DestroyColour;
				switch (pair.Value)
				{
					case CollisionPhase.Stay:
						colour = this.StayColour;
						break;
					case CollisionPhase.Enter:
						colour = this.EnterColour;
						break;
					case CollisionPhase.Exit:
						colour = this.ExitColour;
						break;
				}
				Gizmos.color = colour;
				Gizmos.DrawLine(start_position, this.LastPositions[pair.Key]);
			}
		}

		/**
		 * Update the last position of every tracked collider if they still exist
		 */
		public void UpdateColliderPositions()
		{
			foreach (var pair in this.Colliders)
				if (pair.Key != null)
					this._lastPositions[pair.Key] = pair.Key.transform.position;
		}

		/**
		 * Update the destroyed status of every tracked collider
		 */
		public void CheckColliderDestruction()
		{
			HashSet<Collider> newly_destroyed = new HashSet<Collider>();
			foreach (var pair in this.Colliders)
				if (pair.Value != CollisionPhase.Destroyed && pair.Key == null)
					newly_destroyed.Add(pair.Key);
			foreach (var key in newly_destroyed)
				this.StopTrackingCollider(key);
		}

		/**
		 * A coroutine for transitioning a collider from the enter phase to the stay phase
		 */
		private IEnumerator StayCollider(Collider collider)
		{
			yield return new WaitForSecondsRealtime(this.EnterDelay);
			this._colliders[collider] = CollisionPhase.Stay;
		}

		/** 
		 * A coroutine for removing a collider that has exited
		 */
		private IEnumerator RemoveCollider(Collider collider)
		{
			yield return new WaitForSecondsRealtime(this.ExitDelay);
			this._colliders.Remove(collider);
			this._lastPositions.Remove(collider);
		}

		/**
		 * Start tracking the given collider
		 * @param other The collider to start tracking
		 */
		public void StartTrackingCollider(Collider other)
		{
			if (this.Colliders.ContainsKey(other))
				return;
			this._colliders[other] = CollisionPhase.Enter;
			this._lastPositions[other] = other.transform.position;
			try
			{
				this.StopCoroutine(this._coroutines[other]);
			}
			catch (KeyNotFoundException) { }
			this._coroutines[other] = this.StartCoroutine(this.StayCollider(other));
		}

		/** 
		 * Stop tracking the given collider
		 * @param other The collider to stop tracking
		 */
		public void StopTrackingCollider(Collider other)
		{
			if (!this.Colliders.ContainsKey(other) || this.Colliders[other] == CollisionPhase.Destroyed
				|| (this.Colliders[other] == CollisionPhase.Exit && other != null))
				return;
			this._colliders[other] = other == null ? CollisionPhase.Destroyed : CollisionPhase.Exit;
			try
			{
				this.StopCoroutine(this._coroutines[other]);
			}
			catch (KeyNotFoundException) { }
			this._coroutines[other] = this.StartCoroutine(this.RemoveCollider(other));
		}

		/**
		 * Stop tracking any colliders that are currently being tracked
		 */
		public void StopTrackingAllColliders()
		{
			this.StopAllCoroutines();
			this._colliders.Clear();
			this._lastPositions.Clear();
			this._coroutines.Clear();
		}

		private void OnTriggerEnter(Collider other)
		{
			this.StartTrackingCollider(other);
		}

		private void OnTriggerExit(Collider other)
		{
			this.StopTrackingCollider(other);
		}

		private void Update()
		{
			this.CheckColliderDestruction();
			this.UpdateColliderPositions();
		}

		private void Reset()
		{
			this.StopTrackingAllColliders();
		}
	}
}
