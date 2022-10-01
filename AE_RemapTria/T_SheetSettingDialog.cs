using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class T_SheetSettingDialog : T_BaseDialog
	{
		private T_Button[] m_btns = new T_Button[13];

		public string Caption
		{
			get { return lbStatus.Text; }
			set { lbStatus.Text = value; }
		}
		public T_Fps Fps
		{
			get
			{
				T_Fps fps = T_Fps.FPS24;
				if (btn24fps.Checked)
				{
					fps = T_Fps.FPS24;
				}else if (btn30fps.Checked)
				{
					fps = T_Fps.FPS30;
				}
				return fps;
			}
			set
			{
				if(value== T_Fps.FPS24)
				{
					btn24fps.Checked = true;
					btn30fps.Checked = false;
				}
				else
				{
					btn24fps.Checked = false;
					btn30fps.Checked = true;
				}
				t_DurationBox1.Fps = value;
			}
		}
		public int Frame
		{
			get { return t_DurationBox1.Frame; }
			set
			{
				t_DurationBox1.Frame = value;
			}
		}
		public T_SheetSettingDialog()
		{
			InitializeComponent();
			m_btns[0] = btn0;
			m_btns[1] = btn1;
			m_btns[2] = btn2;
			m_btns[3] = btn3;
			m_btns[4] = btn4;
			m_btns[5] = btn5;
			m_btns[6] = btn6;
			m_btns[7] = btn7;
			m_btns[8] = btn8;
			m_btns[9] = btn9;
			m_btns[10] = btn10Sec;
			m_btns[11] = btn11BS;
			m_btns[12] = btn12CL;
		}



		private void btnLockFps_CheckedChanged(object sender, EventArgs e)
		{
			btn24fps.Enabled = btn30fps.Enabled = !btnLockFps.Checked;

		}

		private void btn24fps_CheckedChanged(object sender, EventArgs e)
		{
			if(sender==null) return;
			T_Button tb = (T_Button)sender;
			if (tb == null) return;
			if (tb.Id == 24)
			{
				btn30fps.Checked = !btn24fps.Checked;

			}
			else if (tb.Id == 30)
			{
				btn24fps.Checked = !btn30fps.Checked;
			}
			T_Fps fps = T_Fps.FPS24;
			if (btn24fps.Checked) { fps = T_Fps.FPS24; } else { fps = T_Fps.FPS30; }

			Fps = fps;

		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if(Frame<12) return;
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
		private int m_mdkey = -1;
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (m_mdkey < 0)
			{
				int v = t_DurationBox1.KeyToNum(e.KeyData);
				if (v >= 0)
				{
					m_mdkey = v;
					m_btns[v].IsMouseDown = true;
					t_DurationBox1.InputKey(v);
				}
				else
				{
					if (e.KeyData == Keys.Enter)
					{
						if (Frame > 12)
						{
							DialogResult = DialogResult.OK;
						}
					}
					else if (e.KeyData == Keys.Escape)
					{
						DialogResult = DialogResult.Cancel;
					}

				}
			}
			base.OnKeyDown(e);
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if(m_mdkey>=0)
			{
				m_btns[m_mdkey].IsMouseDown = false;
				m_mdkey = -1;
			}
			base.OnKeyUp(e);
		}


		private void btn_MouseDown(object sender, MouseEventArgs e)
		{
			T_Button tb = (T_Button)sender;
			if (tb != null)
			{
				t_DurationBox1.InputKey(tb.Id);
			}
		}
	}
}
