using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TS
{
	public class TS_Selection 
	{
		public event EventHandler ChangeSelectionEvent;
		//----------------------------------------------
		private int m_LayerIndex =0;
		private int m_StartIndex = 0;
		private int m_Length = 1;
		private int m_LastIndex = 1;
		//----------------------------------------------
		public TS_Selection()
		{

		}
		//---------------------------------------
		protected virtual void OnChangeSelectionEvent(EventArgs e)
		{
			if (ChangeSelectionEvent != null)
			{
				ChangeSelectionEvent(this, e);
			}
		}       
		//----------------------------------------------
		public int LayerIndex
		{
			get { return m_LayerIndex; }
			set
			{
				bool b = (m_LayerIndex != value);
				m_LayerIndex = value;
				if (b) OnChangeSelectionEvent(new EventArgs());
			}
		}
		//----------------------------------------------
		public int StartIndex
		{
			get { return m_StartIndex; }
			set
			{
				bool b = (m_StartIndex != value);
				m_StartIndex = value;
				m_LastIndex = m_StartIndex + m_Length;
				if (b) OnChangeSelectionEvent(new EventArgs());
			}
		}
		//----------------------------------------------
		public int Length
		{
			get { return m_Length; }
			set
			{
				int v = value;
				if (v < 1) v = 1;
				bool b = (m_Length != v);
				m_Length = v;
				m_LastIndex = m_StartIndex + m_Length;
				if (b) OnChangeSelectionEvent(new EventArgs());
			}
		}
		//----------------------------------------------
		public int LastIndex
		{
			get { return m_LastIndex; }
			set
			{
				int v = value;
				if (v < m_StartIndex + 1) v = m_StartIndex + 1;
				bool b = (m_LastIndex != v);
				m_LastIndex = v;
				m_Length = m_LastIndex - m_StartIndex;
				if (b) OnChangeSelectionEvent(new EventArgs());
			}
		}
		//----------------------------------------------
		private void chkLastIndex()
		{
			m_LastIndex = m_StartIndex + m_Length;
		}
		//----------------------------------------------
		public bool MoveLeft(TS_CellData cd)
		{
			bool ret = false;
			if(m_LayerIndex>0)
			{
				ret = true;
				m_LayerIndex--;
				OnChangeSelectionEvent(new EventArgs());
			}
			return ret;
		}
		//----------------------------------------------
		public bool MoveRight(TS_CellData cd)
		{
			bool ret = false;
			if (m_LayerIndex < cd.LayerCount-1)
			{
				ret = true;
				m_LayerIndex++;
				OnChangeSelectionEvent(new EventArgs());
			}
			return ret;
		}
		//----------------------------------------------
		public bool MoveUp(TS_CellData cd)
		{
			bool ret = false;
			int st = m_StartIndex;
			int lt = m_StartIndex + m_Length;
			st -= m_Length;
			lt -= m_Length;
			if (lt>0)
			{
				ret = true;
				m_StartIndex = st;
				m_LastIndex = m_StartIndex + m_Length;
				OnChangeSelectionEvent(new EventArgs());
			}
			return ret;
		}
		//----------------------------------------------
		public bool MoveDown(TS_CellData cd)
		{
			bool ret = false;
			int st = m_StartIndex;
			int lt = m_StartIndex + m_Length;
			st += m_Length;
			lt += m_Length;
			if (st < cd.FrameCount-1)
			{
				ret = true;
				m_StartIndex = st;
				m_LastIndex = m_StartIndex + m_Length;
				OnChangeSelectionEvent(new EventArgs());
			}
			return ret;
		}
		//----------------------------------------------
		public bool IsTargetLayer(int l)
		{
			return (m_LayerIndex == l);
		}
		//----------------------------------------------
		public bool IsTargetFrame(int f)
		{
			return ((m_StartIndex <= f) && (m_LastIndex > f));
		}
		//----------------------------------------------
		public bool IsTarget(int l,int f)
		{
			bool ret = false;
			if (IsTargetLayer(l)==true)
			{
				ret = IsTargetFrame(f);
			}
			return ret;
		}
	}
}
