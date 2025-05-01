using System.Collections.Generic;
using UnityEngine;

using Technie.PhysicsCreator.Rigid;

namespace Technie.PhysicsCreator
{
	public class RigidSceneOverlay : ISceneOverlay
	{
		private const string PREVIEW_ROOT_NAME = "OVERLAY ROOT (Rigid Collider Creator)";

		private RigidColliderCreatorWindow parentWindow; // We only really need this for access to some global settings, so maybe need a Settings class later

		// Hidden scene object for all overlay objects
		private GameObject overlayRoot;

		// Hull overlay drawn via a single mesh with vertex colours
		private GameObject overlayObject;
		private MeshFilter overlayFilter;
		private MeshRenderer overlayRenderer;
		private Material overlayMaterial;

		// Skew-tolerant matrix for local->world for the selected mesh
		private Matrix4x4 localToWorld;

		public RigidSceneOverlay(RigidColliderCreatorWindow parentWindow)
		{
			this.parentWindow = parentWindow;
		}

		public void Destroy()
		{
			if (overlayObject != null)
			{
				GameObject.DestroyImmediate(overlayObject);
				overlayObject = null;
			}

			if (overlayRoot != null)
			{
				GameObject.DestroyImmediate(overlayRoot);
				overlayRoot = null;
			}
		}

		public void Disable()
		{
			if (overlayObject != null)
			{
				overlayRenderer.enabled = false;
			}
		}

		public void FindOrCreateOverlay()
		{
			if (overlayObject == null)
			{
				overlayObject = GameObject.Find(PREVIEW_ROOT_NAME);
			}

			if (overlayObject != null)
			{
				//Console.output.Log ("Use existing overlay");

				overlayFilter = overlayObject.GetComponent<MeshFilter>();
				overlayRenderer = overlayObject.GetComponent<MeshRenderer>();
			}
			else
			{
				Console.output.Log ("Create new overlay from scratch");

				overlayObject = new GameObject(PREVIEW_ROOT_NAME);
				overlayObject.transform.localPosition = Vector3.zero;
				overlayObject.transform.localRotation = Quaternion.identity;
				overlayObject.transform.localScale = Vector3.one;

				overlayObject.hideFlags = Console.SHOW_SHADOW_HIERARCHY ? HideFlags.None : HideFlags.HideAndDontSave;
			}

			if (overlayFilter == null)
			{
				overlayFilter = overlayObject.AddComponent<MeshFilter>();
			}

			if (overlayFilter.sharedMesh == null)
			{
				overlayFilter.sharedMesh = new Mesh();
			}

			if (overlayRenderer == null)
			{
				overlayRenderer = overlayObject.AddComponent<MeshRenderer>();
				overlayRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
				overlayRenderer.receiveShadows = false;
			}

			if (overlayMaterial == null)
			{
				Console.output.Log("Recreate overlay material");

				string[] assetGuids = UnityEditor.AssetDatabase.FindAssets("Rigid Hull Preview t:Material");
				string path = UnityEditor.AssetDatabase.GUIDToAssetPath(assetGuids[0]);
				overlayMaterial = (Material)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(Material));
			}

			overlayRenderer.sharedMaterial = null;

			overlayObject.transform.SetParent(overlayRoot.transform, false);
		}

		public void SyncOverlay(ICreatorComponent creatorComponent, Mesh inputMesh)
		{
			UnityEngine.Profiling.Profiler.BeginSample("SyncOverlay");

			RigidColliderCreator hullPainter = creatorComponent as RigidColliderCreator;
			//Console.output.Log ("SyncOverlay - overlayObject: " + overlayObject);

			if (hullPainter != null && hullPainter.paintingData != null)
			{
				Mesh overlayMesh = overlayFilter.sharedMesh; // null check required?
				overlayMesh.Clear();

				// The mesh has a complete copy of the vertices of the target object
				// Plus one submesh for each hull (ie. separate set of indices)
				// Each submesh has a unique material that has the per-hull settings (ie. colour)
				//
				// If a hull is invisible then the submesh still exists, but has no indices

				int numSubMeshes = hullPainter.paintingData.hulls.Count;
				overlayMesh.subMeshCount = numSubMeshes;

				//Console.output.Log("Overlay has "+totalFaces+" faces");

				Vector3[] targetVertices = inputMesh.vertices;
				int[] targetTriangles = inputMesh.triangles;

				Vector3[] vertices = new Vector3[targetVertices.Length];

				// Rebuild vertex buffers

				for (int i=0; i<vertices.Length; i++)
				{
					vertices[i] = localToWorld.MultiplyPoint(targetVertices[i]);
				}
				overlayMesh.vertices = vertices;

				for (int i = 0; i < hullPainter.paintingData.hulls.Count; i++)
				{
					Hull hull = hullPainter.paintingData.hulls[i];

					if (hull.isVisible)
					{
						// Conver this hull's selected triangles into a separate indices array for use as a submesh
						int[] indices = new int[hull.NumSelectedTriangles * 3];

						for (int j = 0; j < hull.NumSelectedTriangles; j++)
						{
							int faceIndex = hull.GetSelectedFaceIndex(j);

							int i0 = faceIndex * 3;
							int i1 = faceIndex * 3 + 1;
							int i2 = faceIndex * 3 + 2;

							if (i0 < targetTriangles.Length && i1 < targetTriangles.Length && i2 < targetTriangles.Length)
							{
								int t0 = targetTriangles[i0];
								int t1 = targetTriangles[i1];
								int t2 = targetTriangles[i2];

								if (t0 < targetVertices.Length && t1 < targetVertices.Length && t2 < targetVertices.Length)
								{
									indices[j * 3] = t0;
									indices[j * 3 + 1] = t1;
									indices[j * 3 + 2] = t2;
								}
								else
								{
									Console.output.LogWarning(Console.Technie, "Skipping face because vertex index out of bounds");
								}
							}
							else
							{
								// Probably out of date selection data after the model has been changed
								// Maybe we should have a validate step rather than trying to solve this problem here?
								Console.output.LogWarning(Console.Technie, "Skipping face because triangle index out of bounds: " + faceIndex);
							}
						}
						overlayMesh.SetTriangles(indices, i);
					}
					else
					{
						overlayMesh.SetTriangles(new int[0], i);
					}
						
				}

				// Reallocate materials if the current array doesn't match the new hulls count

				if (overlayRenderer.sharedMaterials.Length != hullPainter.paintingData.hulls.Count || AnyNull(overlayRenderer.sharedMaterials))
				{
					Material[] oldMats = overlayRenderer.sharedMaterials;
					for (int i = 0; i < oldMats.Length; i++)
						Object.DestroyImmediate(oldMats[i]);

					Material[] newMats = new Material[numSubMeshes];
					for (int i = 0; i < newMats.Length; i++)
						newMats[i] = Object.Instantiate(overlayMaterial);

					overlayRenderer.sharedMaterials = newMats;
				}

				// Update the materials so their colour matches the hull

				bool shouldDim = parentWindow != null && parentWindow.ShouldDimInactiveHulls();
				float dimFactor = parentWindow != null ? parentWindow.GetInactiveHullDimFactor() : 0.7f;
				float baseAlpha = parentWindow != null ? parentWindow.GetGlobalHullAlpha() : 0.6f;

				Material[] hullMaterials = overlayRenderer.sharedMaterials;
				for (int i = 0; i < hullPainter.paintingData.hulls.Count; i++)
				{
					Hull hull = hullPainter.paintingData.hulls[i];

					Color baseColour = hull.colour;
					baseColour.a = baseAlpha;
					if (shouldDim && hullPainter.paintingData.HasActiveHull())
					{
						if (hullPainter.paintingData.GetActiveHull() != hull)
						{
							baseColour.a = baseAlpha * (1.0f - dimFactor);
						}
					}
					hullMaterials[i].color = baseColour;
				}

				overlayFilter.sharedMesh = overlayMesh;
				overlayRenderer.enabled = true;
			}
			else
			{
				// No hull painter selected, clear everything

				overlayFilter.sharedMesh.Clear();
				overlayRenderer.enabled = false;
			}

			UnityEngine.Profiling.Profiler.EndSample();
		}

		public void SyncParentChain(GameObject srcLeafObj)
		{
			localToWorld = Utils.CreateSkewableTRS(srcLeafObj.transform);

			if (overlayRoot == null)
			{
				overlayRoot = GameObject.Find(PREVIEW_ROOT_NAME);
			}

			if (overlayRoot == null)
			{
				// Not found, create a new one from scratch
				overlayRoot = new GameObject(PREVIEW_ROOT_NAME);

				overlayRoot.hideFlags = Console.SHOW_SHADOW_HIERARCHY ? HideFlags.None : HideFlags.HideAndDontSave;
			}

			// Ensure the overaly is in the same scene as the object we're manipulating
			// (this also ensures the overlay is moved into the prefab editing scene when that is used)
			if (overlayRoot.scene != srcLeafObj.scene)
			{
				UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(overlayRoot, srcLeafObj.scene);
			}
		}

		public static bool AnyNull(Material[] mats)
		{
			if (mats == null)
				return true;
			foreach (Material m in mats)
			{
				if (m == null)
					return true;
			}
			return false;
		}
	}

} // namespace Technie.PhysicsCreator