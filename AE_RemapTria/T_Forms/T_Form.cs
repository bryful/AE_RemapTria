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
		/// <summary>
		/// 多重起動可能フラグ。プロセス間通信が出来なくなる
		/// </summary>
		public bool IsMultExecute
		{
			get 
			{
				bool ret = false;
				if (m_grid != null)
				{
					ret =  m_grid.IsMultExecute;
				}
				return ret;
			}
			set
			{
				if (m_grid != null)
				{
					m_grid.IsMultExecute =value;
				}
			}
		}
		private enum MDPos
		{
			None,
			Header,
			BottomRight,
		}

		private T_Grid? m_grid = null;
		private T_Input? m_input = null;

		public string FileName 
		{
			get 
			{
				string ret = ""; 
				if (m_grid != null)
				{
					ret = m_grid.FileName;
				}
				return ret;
			}
			set
			{
				if(m_grid!=null)
				{
					m_grid.FileName = value;
				}
			}
		}
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
			this.AllowDrop = true;
		}

		// ********************************************************************
		protected override void OnLoad(EventArgs e)
		{
			bool reloadFlag=false;
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
					bool ok = false;
					string pp = pf.GetValueString("FileName", out ok);
					if(ok)
					{
						FileName = pp;
						reloadFlag = true;
					}
				}
				else
				{
					ToCenter();
				}
			}
			//
			ChkGrid();
			Command(Environment.GetCommandLineArgs().Skip(1).ToArray(), PIPECALL.StartupExec);
			if(reloadFlag)
			{
				if(m_grid!=null)
				{
					//
				}
			}
		}
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			// 二重起動されたものは保存しない
			if (IsMultExecute == false)
			{
				PrefFile pf = new PrefFile((Form)this);
				if(m_grid!=null)pf.SetValue("FileName", m_grid.FileName);
				pf.StoreForm();
				pf.Save();
			}
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
				int x1 = this.Width - 25;
				int xmid = this.Width/2;
				int y1 = this.Height - 25;
				m_mdPos = MDPos.None;
				if ((e.Y < headerY))
				{
					m_mdPos = MDPos.Header;
				}
				else if ((e.X >= x1) || (e.Y >= y1))
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
				m_grid.Sizes.ChangeGridSize += Sizes_ChangeGridSize;
			}

		}
		private void Sizes_ChangeGridSize(object? sender, EventArgs e)
		{
			SetMinMax();
		}
		// ********************************************************************
		private void SetLocSize()
		{
			if (m_grid != null)
			{
				int leftW = m_grid.Sizes.FrameWidth + m_grid.Sizes.InterWidth;
				int topW = m_grid.Sizes.MenuHeight+ m_grid.Sizes.InterHeight 
					+ m_grid.Sizes.CaptionHeight + m_grid.Sizes.CaptionHeight2 + m_grid.Sizes.InterHeight;
				Point p = new Point(leftW, topW);
				if (m_grid.Location != p) m_grid.Location = p;

				int ww = leftW + m_grid.Sizes.InterWidth + T_Size.VScrolWidth + m_grid.Sizes.InterWidth;
				int hh = topW + m_grid.Sizes.InterHeight + T_Size.HScrolHeight + m_grid.Sizes.InterHeight;
				Size sz = new Size(
					this.ClientSize.Width - ww,
					this.ClientSize.Height - hh
					);
				if (m_grid.Size != sz) m_grid.Size = sz;
			}


		}
		public void ChkSize()
		{
			SetMinMax();
			SetLocSize();
		}
		// ********************************************************************
		public T_Input Input
		{
			get { return m_input; }
			set
			{
				m_input = value;
				ChkInput();
			}
		}
		// ********************************************************************
		public void ChkInput()
		{
			if(m_input == null) return;
			if(m_grid!=null)
			{
				m_input.Location = m_grid.Sizes.InputLoc();
				m_input.Size = m_grid.Sizes.InputSize();
			}
			else
			{
				m_input.Location = T_Size.InputLocDef;
				m_input.Size = T_Size.InputSizeDef;
			}
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
				x +ts.FrameWidth+ ts.InterWidth+ts.CellWidth*6+ ts.InterWidth + T_Size.VScrolWidth,
				y + 25 + ts.CaptionHeight2 
					+ ts.CaptionHeight +ts.InterHeight + ts.CellHeight*6+ ts.InterHeight+ T_Size.HScrolHeight
				);
			this.MaximumSize = new Size(
				x + ts.FrameWidth + ts.InterWidth + ts.CellWidth * cc + T_Size.VScrolWidth,
				y + 25 + ts.CaptionHeight2 + ts.CaptionHeight + ts.CellHeight * fc + T_Size.HScrolHeight
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

		// ********************************************************************

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
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if (drgevent != null)
			{
				if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
				{
					drgevent.Effect = DragDropEffects.All;
				}
				else
				{
					drgevent.Effect = DragDropEffects.None;
				}
			}
			//base.OnDragEnter(drgevent);
		}
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if (drgevent != null)
			{
				string[] files = (string[])drgevent.Data.GetData(DataFormats.FileDrop, false);
				//MessageBox.Show("drag");
			}
			//base.OnDragDrop(drgevent);
		}
		// ********************************************************************
		public void Command(string[] args, PIPECALL IsPipe = PIPECALL.StartupExec)
		{
			if ((IsMultExecute == true) && (IsPipe != PIPECALL.StartupExec)) return;
			bool err = true;

			EXEC_MODE em = EXEC_MODE.NONE;
			Args args1 = new Args(args);
			if (args1.OptionCount > 0)
			{
				for (int i = 0; i < args1.OptionCount; i++)
				{
					if (err == false) break;
					Param p = args1.Option(i);
					string op = p.OptionStr.ToLower();
					switch (op)
					{
						case "tocenter":
						case "center":
							this.Invoke((Action)(() => {
								ToCenter();
							}));
							em = EXEC_MODE.NONE;
							break;
						case "foregroundWindow":
						case "foreground":
							this.Invoke((Action)(() => {
								ForegroundWindow();
							}));
							em = EXEC_MODE.NONE;
							break;
						case "exit":
						case "quit":
							if ((args1.ParamsCount == 1) && ((IsPipe == PIPECALL.DoubleExec) || (IsPipe == PIPECALL.PipeExec)))
							{
								em = EXEC_MODE.QUIT;
							}
							break;
						case "export":
							if((m_grid!=null)&&(IsPipe== PIPECALL.PipeExec))
							{
								em = EXEC_MODE.EXPORT;
							}
							break;
						default:
							this.Invoke((Action)(() => {
								if (m_grid != null)
								{
									FuncItem? fi = m_grid.Funcs.FindFunc(op);
									if (fi != null)
									{
										if (fi.Func != null) fi.Func();
									}
								}
							}));
							break;
					}
				}
			}

			switch(em)
			{
				case EXEC_MODE.EXPORT:
						if (m_grid != null){
							//MessageBox.Show(m_grid.ToArdj());
							CallExe.PipeClient("AE_RemapTriaCall", m_grid.ToArdj()).Wait();
						}
					break;
				case EXEC_MODE.QUIT:
					this.Invoke((Action)(() => {
						Application.Exit();
					}));
					break;
				case EXEC_MODE.NONE:
				default:
					if (args1.ParamsCount > 0)
					{
						// パラメータが1個なら読み込み
						if (args1.ParamsCount == 1)
						{
							this.Invoke((Action)(() => {
								if (m_grid != null)
								{
									for (int i = 0; i < args1.ParamsCount; i++)
									{
										if (args1.Params[i].IsPath)
										{
											string p = args1.Params[i].Arg;
											if (File.Exists(p))
											{
												if (m_grid.Open(p) == true)
												{
													break;
												}
											}

										}
									}
								}
							}));
						}
					}
					break;
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
								int idx = -1;
								for(int i = 0; i < apcl.Count; i++)
								{
									if (apcl[i] is T_Form)
									{
										idx = i;
										break;
									}
								}
								if(idx>=0)
								{
									PipeData pd = new PipeData(read);
									((T_Form)apcl[idx]).Command(pd.GetArgs(), pd.GetPIPECALL()); //取得した引数を送る
								}
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