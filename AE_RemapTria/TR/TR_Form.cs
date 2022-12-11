using BRY;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;

namespace AE_RemapTria
{
#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。

	public partial class TR_Form : Form
	{
		public TR_CellData CellData = new TR_CellData();
		public TR_Size Sizes = new TR_Size();
		public TR_Colors Colors = new TR_Colors();
		public T_Funcs Funcs = new T_Funcs();
		public TR_Grid Grid = new TR_Grid();

		public TR_Menu Menu = new TR_Menu();
		public float MenuFontSize
		{
			get { return Menu.FontSize; }
			set { Menu.FontSize = value;DrawAll(); }
		}
		public int MenuFontIndex
		{
			get { return Menu.FontIndex; }
			set { Menu.FontIndex = value; DrawAll(); }
		}
		public TR_Input Input = new TR_Input();
		private int m_Value = -1;
		public int Value
		{
			get { return m_Value; }
			set
			{
				if(m_Value != value)
				{
					m_Value = value;
					Input.ChkOffScr();
					this.Invalidate();
				}
			}
		}
		// ********************************************************
		public void DrawAll()
		{
			if (Menu != null) Menu.ChkOffScr();
			if (Input != null) Input.ChkOffScr();
			this.Invalidate();
		}
		private T_MyFonts m_Fonts = new T_MyFonts();
		/// <summary>
		/// リソースフォント管理のコンポーネント
		/// </summary>
		[Category("_AE_Remap")]
		public T_MyFonts MyFonts
		{
			get { return m_Fonts; }
		}
		private int m_FontIndex = 5;
		[Category("_AE_Remap")]
		public int FontIndex
		{
			get { return m_FontIndex; }
			set
			{
				m_FontIndex = value;
				if (m_FontIndex < 0) m_FontIndex = 0;
				if (m_Fonts != null)
				{
					this.Font = m_Fonts.MyFont(m_FontIndex, this.Font.Size, this.Font.Style);
					DrawAll();
				}
			}
		}
		[Category("_AE_Remap")]
		public float FontSize
		{
			get { return this.Font.Size; }
			set
			{
				SetFontSizeStyle(value, this.Font.Style);
				DrawAll();
			}
		}
		[Category("_AE_Remap")]
		public FontStyle FontStyle
		{
			get { return this.Font.Style; }
			set
			{
				SetFontSizeStyle(this.Font.Size, value);
				DrawAll();

			}
		}
		public void SetFontSizeStyle(float sz, FontStyle fs)
		{
			this.Font = m_Fonts.MyFont(m_FontIndex, sz, fs);
		}
		public Font MyFont(int idx,float sz, FontStyle fs)
		{
			return m_Fonts.MyFont(idx, sz, fs);
		}
		public StringFormat StringFormat = new StringFormat();
		public  StringAlignment Alignment
		{
			get { return StringFormat.Alignment; }
			set
			{
				StringFormat.Alignment = value;
			}
		}
		public StringAlignment LineAlignment
		{
			get { return StringFormat.LineAlignment; }
			set
			{
				StringFormat.LineAlignment = value;
			}
		}
		// ********************************************************************
		[Category("_AE_Remap")]
		public bool IsMultExecute
		{
			get
			{
				return Grid.IsMultExecute;
			}
			set
			{
				Grid.IsMultExecute = value;
			}
		}       
		// ********************************************************************
		private F_Pipe m_Server = new F_Pipe();
		public void StartServer(string pipename)
		{
			m_Server.Server(pipename);
			m_Server.Reception += M_Server_Reception;
		}
		// ********************************************************************
		public void StopServer()
		{
			m_Server.StopServer();
		}
		// ********************************************************************
		private void M_Server_Reception(object sender, ReceptionArg e)
		{
			this.Invoke((Action)(() => {
				PipeData pd = new PipeData(e.Text);
				Command(pd.Args, PIPECALL.PipeExec);
				ForegroundWindow();
			}));
		}

		//private Bitmap m_Back = Properties.Resources.BackGrad;
		/// <summary>
		/// 多重起動可能フラグ。プロセス間通信が出来なくなる
		/// </summary>

		private enum MDPos
		{
			None,
			Header,
			BottomRight,
		}

		[Category("_AE_Remap")]
		public string FileName 
		{
			get 
			{
				return Grid.FileName;
			}
			set
			{
				Grid.FileName = value;
			}
		}
		//public static bool _execution = true;
		NavBar m_navBar = new NavBar();

		private bool m_IsMouseBR = false;
		private MDPos m_mdPos = MDPos.None;
		private Point m_MD = new Point(0, 0);
		private Size m_MDS = new Size(0, 0);

		private Bitmap[] kagi = new Bitmap[5];
		// ********************************************************************
		// ********************************************************************
		public TR_Form()
		{
			this.AutoScaleMode = AutoScaleMode.None;
			this.Font = m_Fonts.MyFont(m_FontIndex, 9, this.Font.Style);

			StringFormat.Alignment = StringAlignment.Near;
			StringFormat.LineAlignment = StringAlignment.Center;

			Grid.SetTRForm(this);
			Menu.SetTRForm(this);
			Input.SetTRForm(this);



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
				ControlStyles.AllPaintingInWmPaint ,
				//ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();

			this.KeyPreview = true;
			this.AllowDrop = true;

		}
		public void Quit()
		{
			Application.Exit();
		}
		// ************************************************************************
		public bool InputAddKey(int v)
		{
			bool ret = false;
			if ((v >= 0) || (v <= 9))
			{
				if (m_Value < 0) m_Value = 0;
				m_Value = m_Value * 10 + v;
				ret = true;
				Input.ChkOffScr();
				this.Invalidate();
			}
			else if (v < 0)
			{
				ret = InputClear();
				Input.ChkOffScr();
				this.Invalidate();
			}
			return ret;
		}
		// ************************************************************************
		public bool InputClear()
		{
			bool ret = false;
			if (m_Value >= 0)
			{
				m_Value = -1;
				ret = true;
				Input.ChkOffScr();
				this.Invalidate();
			}
			return ret;
		}
		// ************************************************************************
		public bool InputBS()
		{
			bool ret = false;
			if (m_Value >= 10)
			{
				m_Value = m_Value / 10;
				ret = true;
				Input.ChkOffScr();
				this.Invalidate();
			}
			else if ((m_Value >= 0) && (m_Value < 10))
			{
				m_Value = -1;
				Input.ChkOffScr();
				this.Invalidate();
				ret = true;
			}
			return ret;
		}       
		// ********************************************************************
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				ToCenter();
				m_navBar.IsFront = true;
				m_navBar.TopMost = true;
			}
			else
			{
				bool ok = false;
				PrefFile pf = new PrefFile(this);
				this.Text = pf.AppName;
				if (pf.Load() == true)
				{
					pf.RestoreForm();
				}
				else
				{
					ToCenter();
				}
				string bp = Path.Combine(pf.FileDirectory, "backup.ardj.json");
				if (File.Exists(bp))
				{
					Grid.OpenBackup(bp);
				}
				string kp = Path.Combine(pf.FileDirectory, T_Grid.KeyBaindName);
				if(Grid.Funcs.Load(kp))
				{
					Grid.KeyBaindFile = kp;
				}
				if(m_navBar!=null)
				{
					bool b = pf.GetValueBool("IsFront", out ok);
					if(ok)
					{
						m_navBar.IsFront = b;
					}
				}
			}
			SetLocSize();

			//
			//ChkGrid();
			Command(Environment.GetCommandLineArgs().Skip(1).ToArray(), PIPECALL.StartupExec);

		}
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			// 二重起動されたものは保存しない
			if (IsMultExecute == false)
			{
				PrefFile pf = new PrefFile(this);
				Grid.SaveBackup(Path.Combine(pf.FileDirectory, "backup.ardj.json"));
				string kp = Path.Combine(pf.FileDirectory, T_Grid.KeyBaindName);
				Grid.Funcs.Save(kp);
				if (m_navBar!=null)
				{
					pf.SetValue("IsFront", m_navBar.IsFront);
				}
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
			//base.OnPaint(e);
			Graphics g = e.Graphics;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			Pen p = new Pen(Color.White);
			SolidBrush sb = new SolidBrush(Color.Black);
			try
			{
				//g.DrawImage(Properties.Resources.Back, new Rectangle(0, 0, this.Width, this.Height));
				T_G.GradBG(g, this.ClientRectangle);
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

				Rectangle r0 = new Rectangle(0, 0, this.Width, 20);
				sb.Color = Grid.Colors.TopBar;
				g.FillRectangle(sb, r0);

				p.Color = Grid.Colors.LineDark;
				Rectangle r = new Rectangle(0,0,this.Width-1,this.Height-1);
				g.DrawRectangle(p, r);

				g.DrawImage(Menu.Offscr(), Menu.Location);
				g.DrawImage(Input.Offscr(), Input.Location);
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
		// ********************************************************************


		// ********************************************************************
		/*
		[Category("_AE_Remap")]
		public T_Grid Grid
		{
			get { return m_grid; }
			set
			{
				m_grid = value;
				ChkGrid();
			}
		}
		*/
		/*
		private void ChkGrid()
		{
			if (m_grid != null)
			{
				SetMinMax();
				SetLocSize();
				//m_grid.SetForm(this);
				m_grid.Sizes.ChangeGridSize += Sizes_ChangeGridSize;
			}

		}
		*/
		private void Sizes_ChangeGridSize(object? sender, EventArgs e)
		{
			SetMinMax();
		}
		// ********************************************************************
		private void SetLocSize()
		{
			Menu.SetLocSize();
			Input.SetLocSize();

			int leftW = Grid.Sizes.FrameWidth + Grid.Sizes.InterWidth;
			int topW = Grid.Sizes.MenuHeight+ Grid.Sizes.InterHeight 
				+ Grid.Sizes.CaptionHeight + Grid.Sizes.CaptionHeight2 + Grid.Sizes.InterHeight;
			Point p = new Point(leftW, topW);
			if (Grid.Location != p) Grid.Location = p;

			int ww = leftW + Grid.Sizes.InterWidth + T_Size.VScrolWidth + Grid.Sizes.InterWidth;
			int hh = topW + Grid.Sizes.InterHeight + T_Size.HScrolHeight + Grid.Sizes.InterHeight;
			Size sz = new Size(
				this.ClientSize.Width - ww,
				this.ClientSize.Height - hh
				);
			if (Grid.Size != sz) Grid.Size = sz;


		}
		public void ChkSize()
		{
			SetMinMax();
			SetLocSize();
		}
		// ********************************************************************

		// ********************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
		// ********************************************************************
		private void SetMinMax()
		{

			TR_Size ts = Grid.Sizes;
			int cc = CellData.CellCount;
			int fc = CellData.FrameCount;

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
				x + ts.FrameWidth + ts.InterWidth*3 + ts.CellWidth * cc + T_Size.VScrolWidth,
				y + 25 + ts.CaptionHeight2 + ts.CaptionHeight + ts.CellHeight * fc + T_Size.HScrolHeight+T_Size.InterHeightDef
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
			FuncItem fi = Funcs.FindKeys(keyData);
			if (fi != null)
			{
				if (fi.Func()) this.Invalidate();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int v = e.Delta * SystemInformation.MouseWheelScrollLines / 15;
			Grid.Sizes.DispY -= v;
			base.OnMouseWheel(e);
		}
		// ********************************************************************
		public void ForegroundWindow()
		{
			F_W.SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
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
			F_Args args1 = new F_Args(args);
			if (args1.OptionCount > 0)
			{
				for (int i = 0; i < args1.OptionCount; i++)
				{
					if (err == false) break;
					F_ArgItem p = args1.Option(i);
					if(p== null) continue;
					string tag = p.Option.ToLower();
					switch (tag)
					{
						case "tocenter":
						case "center":
							ToCenter();
							em = EXEC_MODE.NONE;
							break;
						case "active":
						case "foreground":
						case "foregroundwindow":
							ForegroundWindow();
							em = EXEC_MODE.NONE;
							break;
						case "exit":
						case "quit":
							if ((args1.Count == 1) && ((IsPipe == PIPECALL.DoubleExec) || (IsPipe == PIPECALL.PipeExec)))
							{
								em = EXEC_MODE.QUIT;
							}
							break;
						case "export":
							if((IsPipe== PIPECALL.PipeExec))
							{
								em = EXEC_MODE.EXPORT;
							}
							break;
						case "import_layer":
							if ((IsPipe == PIPECALL.PipeExec))
							{
								em = EXEC_MODE.IMPORT_LAYER;
							}
							break;
						default:
							FuncItem? fi = Grid.Funcs.FindFunc(tag);
							if (fi != null)
							{
								if (fi.Func != null) fi.Func();
							}
							break;
					}
				}
			}

			switch(em)
			{
				case EXEC_MODE.EXPORT:
					F_Pipe.Client(Program.MyCallBackId, Grid.ToArdj()).Wait();
					break;
				case EXEC_MODE.IMPORT_LAYER:
					if ((args.Length > 1))
					{
						Grid.Import_layer(args[1]);
						//MessageBox.Show(args[1]);
					}
					break;
				case EXEC_MODE.QUIT:
					Application.Exit();
					break;
				case EXEC_MODE.NONE:
				default:
					if (args1.Count > 0)
					{
						// パラメータが1個なら読み込み
						for (int i = 0; i < args1.Count; i++)
						{
							if (args1[i].IsOption==false)
							{
								string p = args1[i].Name;
								if (File.Exists(p))
								{
									if (Grid.Open(p) == true)
									{
										break;
									}
								}

							}
						}
					}
					break;
			}
		}
		// *******************************************************************************
		/*
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
									if (apcl[i] is TR_Form)
									{
										idx = i;
										break;
									}
								}
								if(idx >= 0)
								{
                                    BRY.PipeData pd = new BRY.PipeData(read);
									((TR_Form)apcl[idx]).Command(pd.GetArgs(), pd.GetPIPECALL()); //取得した引数を送る
								}
							}

							if (!_execution)
								break; //起動停止？
						}
						ssSv = null;
					}
				}
			});
		}*/
		// ************************************************************************
		/// <summary>
		/// 子コントロールにMouseイベントハンドラを設定(再帰)
		/// </summary>
		public void SetEventHandler(Control objControl)
		{
			// イベントの設定
			// (親フォームにはすでにデザイナでマウスのイベントハンドラが割り当ててあるので除外)
			//if (objControl != this)
			//{
			objControl.MouseDown += (sender, e) => this.OnMouseDown(e);
			objControl.MouseMove += (sender, e) => this.OnMouseMove(e);
			objControl.MouseUp += (sender, e) => this.OnMouseUp(e);
			//}
			/*
			// さらに子コントロールを検出する
			if (objControl.Controls.Count > 0)
			{
				foreach (Control objChildControl in objControl.Controls)
				{
					SetEventHandler(objChildControl);
				}
			}
			*/
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			// senderは常にFormだが、eの座標は各コントロールを基準とした座標が入る為、
			// スクリーン座標からクライアント座標を計算すること
			Point pntScreen = Control.MousePosition;
			Point pntForm = this.PointToClient(pntScreen);

			this.Text = "X=" + pntForm.X + " Y=" + pntForm.Y;
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			Point pntScreen = Control.MousePosition;
			Point pntForm = this.PointToClient(pntScreen);

			this.Text = "X=" + pntForm.X + " Y=" + pntForm.Y;
		}

		private void Form1_MouseUp(object sender, MouseEventArgs e)
		{
			Point pntScreen = Control.MousePosition;
			Point pntForm = this.PointToClient(pntScreen);

			this.Text = "X=" + pntForm.X + " Y=" + pntForm.Y;
		}
		/*
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (m_grid!=null)
			{
				if ((m_grid.IsModif)&&(m_grid.IsMultExecute==false))
				{
					m_grid.SaveAs();
					e.Cancel = false;
				}
			}
			//base.OnFormClosing(e);
		}
		*/
	}

}
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。

