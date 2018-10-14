using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Emitter : MonoBehaviour {
	[SerializeField]
	private float halfWidth = 1f;
	[Range(0, 25f)]
	[SerializeField]
	private int rayCount = 10;
	private float angle;
	[Range(-25f, 25f)]
	[SerializeField]
	private float angleChange = 0f;
	[SerializeField]
	private Color lightColor;
	[SerializeField]
	private float rayMaxLength = 20f;
	[SerializeField]
	private MeshFilter filter;
	[SerializeField]
	private PolygonCollider2D polyCollider;
	private Vector2[] vertices2D;
	private Vector2[] colliderVertices;


	private void Awake() {
		polyCollider.CreatePrimitive(rayCount);
		colliderVertices = new Vector2[rayCount + 3];
		vertices2D = new Vector2[rayCount + 2];
	}

	private void Update() {
		Emit();
	}
	private void FixedUpdate() {
		EmitCollider();
	}

	private void Emit() {
		angle = transform.rotation.eulerAngles.z + angleChange;
		float spaceBetweenRays = (halfWidth * 2) / rayCount;
		float theta = angle * Mathf.PI / 180;
		Vector2 direction = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
		Vector2 directionPerp = Vector2.Perpendicular(direction);
		Vector2 initialPos = new Vector2(this.transform.position.x, this.transform.position.y) + (directionPerp * halfWidth);
		vertices2D[0] = initialPos;
		colliderVertices[0] = initialPos;
		vertices2D[rayCount + 1] = initialPos - (directionPerp * rayCount * spaceBetweenRays);
		colliderVertices[rayCount + 1] = initialPos - (directionPerp * rayCount * spaceBetweenRays);
		colliderVertices[rayCount + 2] = initialPos;
		for (int i = 0; i < rayCount; i++) {
			Vector2 displacement = directionPerp * (i * spaceBetweenRays);
			Vector2 actualPos = initialPos - displacement;
			RaycastHit2D hit = Physics2D.Raycast(actualPos, direction, rayMaxLength);
			if (hit.collider == null) {
				hit.point = direction * rayMaxLength + (Vector2) actualPos;
			}
			vertices2D[i+1] = hit.point;
			colliderVertices[i+1] = hit.point;
			Debug.DrawLine(actualPos, hit.point, color: Color.red);
		}
		Poly(vertices2D);
	}

	private void Poly(Vector2[] vertices2D) {
		// Use the triangulator to get indices for creating triangles
		Triangulator triangulator = new Triangulator(vertices2D);
		int[] indices = triangulator.Triangulate();
		Vector3[] vertices3D = vertices2D.ToVector3();
		Color[] colors = Enumerable.Range(0, vertices3D.Length)
			.Select(_ => lightColor)
			.ToArray();
		Mesh mesh = new Mesh {
			name = "LightMesh",
			vertices = vertices3D,
			triangles = indices,
			colors = colors
		};
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		filter.mesh = mesh;
	}

	private void EmitCollider() {
		polyCollider.SetPath(0, colliderVertices);
	}
}
