# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added 

 - Gizmo base class that merges Monobehaviour.OnDrawGizmos and Monobehaviour.OnDrawGizmosSelected callbacks
 - AttachedGizmo base class that attaches a gizmo to another object
 - ComponentGizmo base class that automatically attaches a gizmo to another component
 - BehaviourGizmo base class that draws its gizmo conditionally based on the enabled state of an attached behaviour
 - TriggerGizmo class that tracks the colliders entering and exiting a trigger
