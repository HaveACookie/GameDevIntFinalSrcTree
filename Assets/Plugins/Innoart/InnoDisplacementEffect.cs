using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UnityEngine.Camera))]
[AddComponentMenu("Inno/DisplacementEffect")]
public class InnoDisplacementEffect : MonoBehaviour {
	[Header("Normal Map")]
	public Texture texture;
	[Header("Magnitude")]
	[Range(0f, 1f)] public float magnitude;
	
	private Material m_material;
	private Shader shader;
	
	private Material material {
		get {
			if (m_material == null) {
				shader = Shader.Find("Inno/AnimDisplaceShader");
				m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				m_material.SetTexture("_DisplaceTex", texture);
				Update();
			}

			return m_material;
		}
	}
	
	private void Start() {
		if (!SystemInfo.supportsImageEffects)
			enabled = false;
	}

	private void Update() {
		if (m_material) {
			magnitude = Mathf.Clamp(magnitude, 0, 1);
			m_material.SetFloat("_Magnitude", magnitude * 0.1f);
		}
	}

	public void OnRenderImage(RenderTexture src, RenderTexture dest) {
		if (material && magnitude > 0) {
			Graphics.Blit(src, dest, material);
		} else {
			Graphics.Blit(src, dest);
		}
	}

	private void OnDisable() {
		if (m_material) {
			DestroyImmediate(m_material);
		}
	}
}
