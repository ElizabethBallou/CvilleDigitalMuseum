using UnityEngine;

namespace NprPaintFilter
{
	[RequireComponent(typeof(Camera))]
	public class Halftone : MonoBehaviour
	{
		public Material m_Mat;
		[Range(1f, 4f)] public float m_Lightness = 2f;
		[Range(70f, 200f)] public float m_Density = 200f;
		[Range(0.1f, 4f)] public float m_SmoothEdge = 1f;
		[Range(0.1f, 2f)] public float m_Radius = 1f;
		[Range(0f, 1f)] public float m_OrigFactor = 0.5f;
		[Range(0f, 1f)] public float m_HalfToneFactor = 0.5f;
		public Color m_Color1 = Color.black;
		public Color m_Color2 = Color.white;

		void Update ()
		{
			m_Mat.SetFloat ("_Lightness", m_Lightness);
			m_Mat.SetFloat ("_Density", m_Density);
			m_Mat.SetFloat ("_SmoothEdge", m_SmoothEdge);
			m_Mat.SetFloat ("_Radius", m_Radius);
			m_Mat.SetFloat ("_OrigFactor", m_OrigFactor);
			m_Mat.SetFloat ("_HalfToneFactor", m_HalfToneFactor);
			m_Mat.SetColor ("_Color1", m_Color1);
			m_Mat.SetColor ("_Color2", m_Color2);
		}
		void OnRenderImage (RenderTexture src, RenderTexture dst)
		{
			Graphics.Blit (src, dst, m_Mat);
		}
	}
}