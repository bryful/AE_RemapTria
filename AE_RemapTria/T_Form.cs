using BRY;
using System.Diagnostics;
using System.IO.Compression;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;

namespace AE_RemapTria
{
#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。

	public partial class T_Form : Form
	{
		private enum MDPos
		{
			None,
			Header,
			BottomRight,
		}

		private T_Grid? m_grid = null;
		private string m_FileName = "";
		public static bool _execution = true;
		NavBar m_navBar = new NavBar();

		private bool m_IsMouseBR = false;
		private MDPos m_mdPos = MDPos.None;
		private Point m_MD = new Point(0, 0);
		private Size m_MDS = new Size(0, 0);

		private Bitmap[] kagi = new Bitmap[5];
		// ********************************************************************
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
		// ********************************************************************
		public T_Form()
		{
			kagi[0] = Properties.Resources.Kagi00;
			kagi[1] = Properties.Resources.Kagi01;
			kagi[2] = Properties.Resources.Kagi02;
			kagi[3] = Properties.Resources.Kagi03;
			kagi[4] = Properties.Resources.Kagi02_D;

			InitializeComponent();
			NavBarSetup();
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
			this.KeyPreview = true;
		}

		// ********************************************************************
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				ToCenter();
			}
			else
			{
				PrefFile pf = new PrefFile((Form)this);
				this.Text = pf.AppName;
				if (pf.Load() == true)
				{
					pf.RestoreForm();
				}
				else
				{
					ToCenter();
				}
			}
			//
			ChkGrid();
			Command(Environment.GetCommandLineArgs().Skip(1).ToArray(), PIPECALL.StartupExec);
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			//this.Text = nameof(MainForm.Parent) + "/aa";
		}
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			PrefFile pf = new PrefFile((Form)this);
			pf.StoreForm();
			pf.Save();
			base.OnFormClosed(e);
		}
		// ********************************************************************
		private void NavBarSetup()
		{
			if (DesignMode == false)
			{
				m_navBar.Form = this;
				m_navBar.SizeSet();
				m_navBar.LocSet();
				m_navBar.Show();
			}

		}
		// ********************************************************************
		protected override void InitLayout()
		{
			base.InitLayout();
			this.KeyPreview = true;
			this.MaximumSize = new Size(65536,65536);
		}
		// ********************************************************************
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_mdPos != MDPos.None) return;
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int headerY = 25;
				int x0 = 15;
				int x1 = this.Width - 15;
				int xmid = this.Width/2;
				int y1 = this.Height - 15;
				m_mdPos = MDPos.None;
				if ((e.Y < headerY)||(e.X<x0))
				{
					m_mdPos = MDPos.Header;
				}
				else if ((e.X > x1) || (e.Y > y1))
				{
					m_mdPos = MDPos.BottomRight;
				}
				if (m_mdPos != MDPos.None)
				{
					m_MD = new Point(e.X,e.Y);
					m_MDS = new Size(this.Size.Width, this.Size.Height);
				}
			}
			base.OnMouseDown(e);
		}
		// ********************************************************************
		protected override void OnMouseLeave(EventArgs e)
		{
			if (m_IsMouseBR == true)
			{
				m_IsMouseBR = false;
				this.Refresh();
			}
			base.OnMouseLeave(e);
		}

		// ********************************************************************
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if((e.X>this.Width-20)&& (e.Y > this.Height - 20))
			{
				if (m_IsMouseBR == false)
				{
					m_IsMouseBR =true;
					this.Refresh();
				}
			}
			else
			{
				if (m_IsMouseBR == true)
				{
					m_IsMouseBR = false;
					this.Refresh();
				}
			}

			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				if (m_mdPos != MDPos.None)
				{
					int ax = e.X - m_MD.X;
					int ay = e.Y - m_MD.Y;
					switch (m_mdPos)
					{
						case MDPos.Header:
							this.Location = new Point(this.Location.X + ax, this.Location.Y + ay);
							break;
						case MDPos.BottomRight:
							this.Size = new Size(m_MDS.Width + ax, m_MDS.Height + ay);
							break;
					}
					this.Refresh();
				}
			}
			//base.OnMouseMove(e);
		}
		// ********************************************************************
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_mdPos!=MDPos.None)
			{
				m_mdPos = MDPos.None;
			}
			base.OnMouseUp(e);
		}
		// ********************************************************************
		protected override void OnDoubleClick(EventArgs e)
		{
			if (m_mdPos == MDPos.Header)
			{
				HeightMax();
				m_mdPos=MDPos.None;	
			}
			base.OnDoubleClick(e);
		}

		// ********************************************************************
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			Pen p = new Pen(Color.White);
			SolidBrush sb = new SolidBrush(Color.Black);
			try
			{
				int w0 = 5;
				int w1 = this.Width - 20 - w0;
				int h0 = 25;
				int h1 = this.Height -20 -5;

				g.DrawImage(kagi[0], new Point(w0, h0));
				g.DrawImage(kagi[1], new Point(w1, h0));
				if (m_IsMouseBR)
				{
					g.DrawImage(kagi[4], new Point(w1, h1));

				}
				else
				{
					g.DrawImage(kagi[2], new Point(w1, h1));
				}
				g.DrawImage(kagi[3], new Point(w0, h1));

				if (m_grid != null)
				{
					Rectangle r0 = new Rectangle(0, 0, this.Width, 20);
					sb.Color = m_grid.Colors.TopBar;
					g.FillRectangle(sb, r0);
				}

				if (m_grid != null) p.Color = m_grid.Colors.LineA;
				Rectangle r = new Rectangle(0,0,this.Width-1,this.Height-1);
				g.DrawRectangle(p, r);
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
		// ********************************************************************
		

		// ********************************************************************
		public T_Grid Grid
		{
			get { return m_grid; }
			set
			{
				m_grid = value;
				ChkGrid();
			}
		}
		private void ChkGrid()
		{
			if (m_grid != null)
			{
				SetMinMax();
				SetLocSize();
				m_grid.SetForm(this);
				m_grid.SizeChanged += M_grid_SizeChanged;
				m_grid.LocationChanged += M_grid_SizeChanged;
				m_grid.Sizes.ChangeGridSize += Sizes_ChangeGridSize;
			}

		}
		private void Sizes_ChangeGridSize(object? sender, EventArgs e)
		{
			SetMinMax();
		}

		private void M_grid_SizeChanged(object? sender, EventArgs e)
		{
			SetLocSize();
		}

		// ********************************************************************
		private void SetLocSize()
		{
			if (m_grid == null) return;
			int leftW = m_grid.Sizes.FrameWidth + m_grid.Sizes.InterWidth;
			int topW = 25 + m_grid.Sizes.CaptionHeight + m_grid.Sizes.CaptionHeight2;
			Point p = new Point(leftW, topW);
			if (m_grid.Location != p) m_grid.Location = p;
			int ww = leftW + m_grid.Sizes.InterWidth + 22 + 5;
			int hh = topW + m_grid.Sizes.InterHeight + 22 + 5;
			Size sz = new Size(
				this.ClientSize.Width - ww,
				this.ClientSize.Height - hh
				);
			if (m_grid.Size != sz) m_grid.Size = sz;

		}
		// ********************************************************************
		protected override void OnResize(EventArgs e)
		{
			SetLocSize();
			base.OnResize(e);
		}
		// ********************************************************************
		private void SetMinMax()
		{
			if (m_grid == null) return;

			T_Size ts = m_grid.Sizes;
			int cc = m_grid.CellData.CellCount;
			int fc = m_grid.CellData.FrameCount;

			Size csz = this.ClientSize;
			Size sz = this.Size;
			int x = sz.Width -csz.Width;
			int y = sz.Height - csz.Height;


			this.MinimumSize = new Size(
				x +ts.FrameWidth+ ts.InterWidth+ts.CellWidth*6+ ts.InterWidth + 22 +10,
				y + 25 + ts.CaptionHeight2 
					+ ts.CaptionHeight +ts.InterHeight + ts.CellHeight*6+ ts.InterHeight+22 + 10
				);
			this.MaximumSize = new Size(
				x + ts.FrameWidth + ts.InterWidth + ts.CellWidth * cc + 22 + 10,
				y + 25 + ts.CaptionHeight2 + ts.CaptionHeight + ts.CellHeight * fc + 22 + 10
				);
		}
		// ********************************************************************
		public void ToCenter()
		{
			Rectangle rct = Screen.PrimaryScreen.Bounds;
			Point p = new Point((rct.Width - this.Width) / 2, (rct.Height - this.Height) / 2);
			this.Location = p;
			ForegroundWindow();
		}
		// ********************************************************************
		public bool Export(string p)
		{
			bool ret = false;
			ForegroundWindow();
			try
			{
				string s = "";// textBox1.Text;
				File.WriteAllText(p, s, Encoding.GetEncoding("utf-8"));
				m_FileName = p;
				this.Text = Path.GetFileName(p);
				ret = true;
			}
			catch
			{
				ret = false;
			}

			return ret;
		}
		// ********************************************************************
		public bool Import(string p)
		{
			bool ret = false;
			ForegroundWindow();
			if (File.Exists(p) == true)
			{
				try
				{
					if (File.Exists(p) == true)
					{
						string str = File.ReadAllText(p, Encoding.GetEncoding("utf-8"));
						//textBox1.Text = str;
						//textBox1.Select(0, 0);
						m_FileName = p;
						this.Text = Path.GetFileName(p);
						ret = true;
					}
				}
				catch
				{
					ret = false;
				}
			}

			return ret;
		}
		// ********************************************************************
		protected override bool ProcessDialogKey(Keys keyData)
		{
			this.Text = String.Format("{0}", keyData.ToString());
			if (m_grid != null)
			{
				FuncItem fi = m_grid.Funcs.FindKeys(keyData);
				if (fi != null)
				{
					if (fi.Func()) this.Invalidate();
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if(m_grid!=null)
			{
				int v = e.Delta * SystemInformation.MouseWheelScrollLines / 15;
				m_grid.Sizes.DispY -= v;
			}
			base.OnMouseWheel(e);
		}
		// ********************************************************************
		public void ForegroundWindow()
		{
			SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
		}
		// ********************************************************************
		public bool HeightMax()
		{
			bool ret = false;
			Rectangle r = PrefFile.NowScreen(this.Bounds);
			if(r.Width>100)
			{
				int h = r.Height - 25;
				this.Location = new Point(this.Left, r.Top + 25);
				this.Size = new Size(this.Width, h);
				ret = true;
			}
			return ret;
		}
		// ********************************************************************
		public void Command(string[] args, PIPECALL IsPipe = PIPECALL.StartupExec)
		{
			bool err = true;
			Args args1 = new Args(args);
			if (args1.OptionCount > 0)
			{
				for (int i = 0; i < args1.OptionCount; i++)
				{
					if (err == false) break;
					Param p = args1.Option(i);
					switch (p.OptionStr.ToLower())
					{
						case "tocenter":
						case "center":
							ToCenter();
							break;
						case "foregroundWindow":
						case "foreground":
							ForegroundWindow();
							break;
						case "load":
						case "ld":
							int idx = p.Index + 1;
							if (idx < args1.ParamsCount)
							{
								if (args1.Params[idx].IsOption == false)
								{
									err = Import(args1.Params[idx].Arg);
								}
							}
							break;
						case "save":
						case "sv":
							int idx2 = p.Index + 1;
							if (idx2 < args1.ParamsCount)
							{
								if (args1.Params[idx2].IsOption == false)
								{
									err = Export(args1.Params[idx2].Arg);
								}
							}
							break;
						case "exit":
						case "quit":
							if ((args1.ParamsCount == 1) && (IsPipe == PIPECALL.DoubleExec))
							{
								Application.Exit();
							}
							break;
					}
				}
			}
			else
			{
				if (args1.ParamsCount > 0)
				{
					if (args1.ParamsCount == 1)
					{
						err = Import(args1.Params[0].Arg);
					}
					else
					{
						//textBox1.Lines = args1.ParamStrings;
						//textBox1.Select(0, 0);
					}
				}
			}
		}
		// *******************************************************************************
		static public void ArgumentPipeServer(string pipeName)
		{
			Task.Run(() =>
			{ //Taskを使ってクライアント待ち
				while (_execution)
				{
					//複数作ることもできるが、今回はwhileで1つずつ処理する
					using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1))
					{
						// クライアントの接続待ち
						pipeServer.WaitForConnection();

						StreamString ssSv = new StreamString(pipeServer);

						while (true)
						{ //データがなくなるまで                       
							string read = ssSv.ReadString(); //クライアントの引数を受信 
							if (string.IsNullOrEmpty(read))
								break;

							//引数が受信できたら、Applicationに登録されているだろうForm1に引数を送る
							FormCollection apcl = Application.OpenForms;

							if (apcl.Count > 0)
							{
								PipeData pd = new PipeData(read);
								((T_Form)apcl[0]).Command(pd.GetArgs(), pd.GetPIPECALL()); //取得した引数を送る
							}

							if (!_execution)
								break; //起動停止？
						}
						ssSv = null;
					}
				}
			});
		}

	}
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。

}