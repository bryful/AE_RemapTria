using BRY;

namespace AE_RemapTria
{
	public partial class NavBar : Form
	{
		bool _refFlag = true;
		public enum ModePos
		{
			LEFT,
			TOPLEFT,
			TOPRIGHT,
			RIGHT,
			BOTTOMRIGHT,
			BOTTOMLEFT
		};
		private ModePos m_PosMode = ModePos.TOPLEFT;
		public ModePos PosMode
		{
			get { return m_PosMode; }

			set
			{
				if (m_PosMode != value)
				{
					m_PosMode = value;
					LocSet();
				}
			}
		}
#pragma warning disable CS8625 // null リテラルを null 非許容参照型に変換できません。
		private Form m_form = null;
#pragma warning restore CS8625 // null リテラルを null 非許容参照型に変換できません。
		public Form Form
		{
			get { return m_form; }
			set
			{
				m_form = value;
				if (m_form != null)
				{
					label1.Text = m_form.Text;
					SizeSet();
					LocSet();
					SetIsFront(m_IsFront);
#pragma warning disable CS8622 // パラメーターの型における参照型の NULL 値の許容が、ターゲット デリゲートと一致しません。おそらく、NULL 値の許容の属性が原因です。
					m_form.SizeChanged += M_form_SizeChanged;
					m_form.TextChanged += M_form_TextChanged;
					m_form.Move += M_form_Move;
#pragma warning restore CS8622 // パラメーターの型における参照型の NULL 値の許容が、ターゲット デリゲートと一致しません。おそらく、NULL 値の許容の属性が原因です。

				}
			}
		}

		private void M_form_TextChanged(object? sender, EventArgs e)
		{
			label1.Text = m_form.Text;
		}

		private bool m_IsFront = false;
		public bool IsFront
		{
			get { return m_IsFront; }
			set { SetIsFront(value); }
		}

		public string Caption
		{
			get { return label1.Text; }
			//set { label1.Text = value; }
		}
		private void M_form_Move(object sender, EventArgs e)
		{
			LocSet();
		}

		private void M_form_SizeChanged(object sender, EventArgs e)
		{
			SizeSet();
			LocSet();
		}

		private Point mousePoint;
		
		public NavBar()
		{
			this.Size = new Size(160, 20);
			InitializeComponent();
			SizeSet();
			IsFront = true;
		}
		// *****************************************************************
		public void LocSet()
		{
			if (m_form == null) return;

			if (_refFlag == false) return;
			_refFlag = false;
			switch (m_PosMode)
			{
				case ModePos.LEFT:
					this.Location = new Point(m_form.Left - this.Width, m_form.Top);
					break;
				case ModePos.TOPLEFT:
					this.Location = new Point(m_form.Left+7, m_form.Top-this.Height);
					break;
				case ModePos.TOPRIGHT:
					this.Location = new Point(m_form.Left+m_form.Width- this.Width, m_form.Top - this.Height);
					break;
				case ModePos.RIGHT:
					this.Location = new Point(m_form.Left + m_form.Width, m_form.Top);
					break;
				case ModePos.BOTTOMRIGHT:
					this.Location = new Point(m_form.Left + m_form.Width - this.Width, m_form.Top + m_form.Height);
					break;
				case ModePos.BOTTOMLEFT:
					this.Location = new Point(m_form.Left+7 , m_form.Top + m_form.Height);
					break;
			}
			_refFlag = true;


		}
		// *****************************************************************
		public void SizeSet()
		{
			//if (m_form == null) return;

			this.Size = new Size(200, 25);

		}
		// *****************************************************************
		private void NavBar_MouseDown(object sender, MouseEventArgs e)
		{
			formActive();
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				mousePoint = new Point(e.X, e.Y);
			}
		}

		private void NavBar_MouseMove(object sender, MouseEventArgs e)
		{
			if(_refFlag==false) return;
			Point p = this.Location;
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int xx = e.X - mousePoint.X;
				int yy = e.Y - mousePoint.Y;
				_refFlag = false;
				this.Location = new Point(p.X + xx, p.Y + yy);
				if (m_form != null)
				{
					switch (m_PosMode)
					{
						case ModePos.LEFT:
							m_form.Location = new Point(this.Location.X + this.Width, this.Location.Y);
							break;
						case ModePos.TOPLEFT:
							m_form.Location = new Point(this.Location.X-7, this.Location.Y + this.Height);
							break;
						case ModePos.TOPRIGHT:
							m_form.Location = new Point(this.Location.X - m_form.Width + this.Width, this.Location.Y + this.Height);
							break;
						case ModePos.RIGHT:
							m_form.Location = new Point(this.Location.X - m_form.Width, this.Location.Y);
							break;
						case ModePos.BOTTOMRIGHT:
							m_form.Location = new Point(this.Location.X - m_form.Width + this.Width, this.Location.Y-m_form.Height);
							break;
						case ModePos.BOTTOMLEFT:
							m_form.Location = new Point(this.Location.X -7, this.Location.Y - m_form.Height);
							break;
					}

				}
				_refFlag = true;
			}
		}
		// *****************************************************************
		public void SetIsFront(bool b)
		{
			//if (m_IsFront == b) return;
			m_IsFront = b;
			if (m_IsFront == true)
			{
				formActive();
			}
			else
			{
				if (m_form == null) return;
				m_form.TopMost = false;
			}

		}
		// *****************************************************************
		private void formActive()
		{
			if (m_form == null) return;
			m_form.BringToFront();
			m_form.TopMost = true;
			if (m_IsFront == false)
			{
				m_form.TopMost = false;
			}
		}
		// *****************************************************************
		private void checkBox1_Click(object sender, EventArgs e)
		{
			SetIsFront(checkBox1.Checked);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void MenuPosLeft_Click(object sender, EventArgs e)
		{
			PosMode = ModePos.LEFT;
		}

		private void MenuPostRight_Click(object sender, EventArgs e)
		{
			PosMode = ModePos.RIGHT;
		}

		private void MenuPosTopLeft_Click(object sender, EventArgs e)
		{
			PosMode = ModePos.TOPLEFT;

		}

		private void MenuPosTopRight_Click(object sender, EventArgs e)
		{
			PosMode = ModePos.TOPRIGHT;
		}

		private void MenuPostBottomRight_Click(object sender, EventArgs e)
		{
			PosMode = ModePos.BOTTOMRIGHT;
		}

		private void MenuPostBottomLeft_Click(object sender, EventArgs e)
		{
			PosMode = ModePos.BOTTOMLEFT;
		}

		// *****************************************************************
	}
}
