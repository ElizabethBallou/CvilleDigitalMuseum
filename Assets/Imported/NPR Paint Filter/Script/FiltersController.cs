using UnityEngine;

namespace NprPaintFilter
{
	[RequireComponent(typeof(Camera))]
	public class FiltersController : MonoBehaviour
	{
		public enum EFilter { None, BluePrint, CmykHalftone, Halftone, OilPaint, PencilSketch, PencilDot, AbstractPainting, WaterColor }
		public EFilter m_EnableFilter = EFilter.None;
		EFilter m_PrevEnableFilter = EFilter.None;
		BluePrint m_CompBluePrint;
		CmykHalftone m_CompCmykHalftone;
		OilPaint m_CompOilPaint;
		PencilSketch m_CompPencilSketch;
		PencilDot m_CompPencilDot;
		AbstractPainting m_CompAbstractPainting;
		Halftone m_CompHalftone;
		WaterColor m_CompWaterColor;

		void Start ()
		{
			m_CompBluePrint = GetComponent<BluePrint> ();
			if (m_CompBluePrint == null)
				m_CompBluePrint = gameObject.AddComponent<BluePrint> ();
			
			m_CompCmykHalftone = GetComponent<CmykHalftone> ();
			if (m_CompCmykHalftone == null)
				m_CompCmykHalftone = gameObject.AddComponent<CmykHalftone> ();
			
			m_CompOilPaint = GetComponent<OilPaint> ();
			if (m_CompOilPaint == null)
				m_CompOilPaint = gameObject.AddComponent<OilPaint> ();
			
			m_CompPencilSketch = GetComponent<PencilSketch> ();
			if (m_CompPencilSketch == null)
				m_CompPencilSketch = gameObject.AddComponent<PencilSketch> ();
				
			m_CompPencilDot = GetComponent<PencilDot> ();
			if (m_CompPencilDot == null)
				m_CompPencilDot = gameObject.AddComponent<PencilDot> ();
				
			m_CompAbstractPainting = GetComponent<AbstractPainting> ();
			if (m_CompAbstractPainting == null)
				m_CompAbstractPainting = gameObject.AddComponent<AbstractPainting> ();
			
			m_CompHalftone = GetComponent<Halftone> ();
			if (m_CompHalftone == null)
				m_CompHalftone = gameObject.AddComponent<Halftone> ();
			
			m_CompWaterColor = GetComponent<WaterColor> ();
			if (m_CompWaterColor == null)
				m_CompWaterColor = gameObject.AddComponent<WaterColor> ();
			
			ApplyFilter ();
		}
		void Update ()
		{
			if (m_PrevEnableFilter != m_EnableFilter)
			{
				m_PrevEnableFilter = m_EnableFilter;
				ApplyFilter ();
			}
		}
		void ApplyFilter ()
		{
			m_CompBluePrint.enabled  = false;
			m_CompCmykHalftone.enabled  = false;
			m_CompOilPaint.enabled  = false;
			m_CompPencilSketch.enabled  = false;
			m_CompPencilDot.enabled = false;
			m_CompAbstractPainting.enabled = false;
			m_CompHalftone.enabled = false;
			m_CompWaterColor.enabled = false;

			if (EFilter.BluePrint == m_EnableFilter)
				m_CompBluePrint.enabled = true;
			if (EFilter.CmykHalftone == m_EnableFilter)
				m_CompCmykHalftone.enabled = true;
			if (EFilter.OilPaint == m_EnableFilter)
				m_CompOilPaint.enabled = true;
			if (EFilter.PencilSketch == m_EnableFilter)
				m_CompPencilSketch.enabled = true;
			if (EFilter.PencilDot == m_EnableFilter)
				m_CompPencilDot.enabled = true;
			if (EFilter.AbstractPainting == m_EnableFilter)
				m_CompAbstractPainting.enabled = true;
			if (EFilter.Halftone == m_EnableFilter)
				m_CompHalftone.enabled = true;
			if (EFilter.WaterColor == m_EnableFilter)
				m_CompWaterColor.enabled = true;
		}
	}
}