using BRY;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO.Compression;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;

namespace AE_RemapTria
{

    public partial class TR_Form : Form
	{
		public TR_CellData CellData = new TR_CellData();
		public TR_Size Sizes = new TR_Size();
		public TR_Colors Colors = new TR_Colors();
		public T_Funcs Funcs = new T_Funcs();
		public TR_Grid Grid = new TR_Grid();

		public TR_Selection Selection = new TR_Selection();

		public TR_Menu Menu = new TR_Menu();
		public TR_MenuPlate [] MenuPlates = new TR_MenuPlate[0];

		public TR_Frame Frame = new TR_Frame();
		public TR_Caption Caption = new TR_Caption();
		public TR_VScrol VScrol = new TR_VScrol();
		public TR_HScrol HScrol = new TR_HScrol();

		private Bitmap tria = Properties.Resources.τρία;
		// ---------------------------------------------------------
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

		private string m_InputStr = "";
		public string InputStr
		{
			get { return this.m_InputStr; }
		}
		public int InputValue
		{
			get
			{
				int ret = -1;
				if( int.TryParse(m_InputStr,out ret)==false)
				{
					ret = -1;
				}
				return ret;
			}
		}
		public bool AddValueStr(int v)
		{
			bool ret = false;
			if ((v < 0) || (v > 9)) return ret;
			if ((m_InputStr == "") && (v == 0)) return ret;
			if (m_InputStr.Length >= 3) return ret;
			m_InputStr += v.ToString();
			ret = true;
			return ret;
		}

		// ********************************************************
		public void DrawAll()
		{
			if (Menu != null) Menu.ChkOffScr();
			if (Frame != null) Frame.ChkOffScr();
			if (Caption != null) Caption.ChkOffScr();
			if (Grid != null) Grid.ChkOffScr();
			if (VScrol != null) VScrol.ChkOffScr();
			if (HScrol != null) HScrol.ChkOffScr();
			this.Invalidate();
		}
		private TR_MyFonts m_Fonts = new TR_MyFonts();
		public TR_MyFonts MyFonts
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
		private bool m_IsJapanOS = true;
		public bool IsJapanOS { get { return m_IsJapanOS; } }
		public bool IsMultExecute = false;
		private string m_FileName = "AE_Remapτρ?α";
		public string FileName
		{
			get { return m_FileName; }
			set
			{
				m_FileName = value;
				if (m_FileName != "")
				{
					string t = T_Def.GetName(m_FileName);
					if (IsModif)
					{
						t += "*";
					}
					this.Text = t;
				}
				else
				{
					this.Text = "AE_Remapτρ?α";
				}
			}
		}
		public string KeyBaindFile = "";
		public const string KeyBaindName = "AE_Remap_key.json";
		public bool IsModif = false;

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


		
		//public static bool _execution = true;
		TR_NavBar m_navBar = new TR_NavBar();

		private bool m_IsMouseBR = false;
		private MDPos m_mdPos = MDPos.None;
		private Point m_MD = new Point(0, 0);
		private Size m_MDS = new Size(0, 0);

		//private Bitmap[] kagi = new Bitmap[5];
		// ********************************************************************
		// ********************************************************************
		public TR_Form()
		{
			m_IsJapanOS = System.Globalization.CultureInfo.CurrentUICulture.Name == "ja-JP";

			this.AutoScaleMode = AutoScaleMode.None;
			this.Font = m_Fonts.MyFont(m_FontIndex, 9, this.Font.Style);

			StringFormat.Alignment = StringAlignment.Near;
			StringFormat.LineAlignment = StringAlignment.Center;

			MakeFuncs();
			CellData.SetTRForm(this);
			Selection.SetCellData(CellData);
			Grid.SetTRForm(this);
			Sizes.SetTRForm(this);

			Frame.SetTRForm(this);
			Caption.SetTRForm(this);
			VScrol.SetTRForm(this);
			HScrol.SetTRForm(this);
			Menu.SetTRForm(this);


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
					OpenBackup(bp);
				}
				string kp = Path.Combine(pf.FileDirectory, TR_Form.KeyBaindName);
				if(Funcs.Load(kp))
				{
					KeyBaindFile = kp;
				}
				if(m_navBar!=null)
				{
					bool b = pf.GetValueBool("IsFront", out ok);
					if(ok)
					{
						m_navBar.IsFront = b;
					}
				}
				string cp = Path.Combine(pf.FileDirectory, "Colors.json");
				if(Colors.load(cp)) { }

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
				SaveBackup(Path.Combine(pf.FileDirectory, "backup.ardj.json"));
				Colors.Save(Path.Combine(pf.FileDirectory, "Colors.json"));
				string kp = Path.Combine(pf.FileDirectory, KeyBaindName);
				Funcs.Save(kp);
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
			m_navBar.SetTRForm(this);
			m_navBar.Show();

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
			if(IsInputMode)
			{
				SetIsInputMode(false);
			}
			if (m_mdPos != MDPos.None) return;

			if (Grid.ChkMouseDown(e))
			{ 
			}else if (Menu.ChkMouseDown(e))
			{

			
			}else if (Frame.ChkMouseDown(e))
			{

			}
			else if (Caption.ChkMouseDown(e))
			{

			}
			else if (VScrol.ChkMouseDown(e))
			{

			}
			else if (HScrol.ChkMouseDown(e))
			{

			}
			else
			{
				int headerY = 25;
				int x1 = this.Width - 25;
				int xmid = this.Width / 2;
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
					m_MD = new Point(e.X, e.Y);
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
			if ((e.X > this.Width - 20) && (e.Y > this.Height - 20))
			{
				if (m_IsMouseBR == false)
				{
					m_IsMouseBR = true;
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
				m_IsMouseBR = false;
			}

			if (Grid.ChkMouseMove(e)) 
			{ 
			}
			else if (Menu.ChkMouseMove(e))
			{

			}
			else if (VScrol.ChkMouseMove(e))
			{

			}
			else if (HScrol.ChkMouseMove(e))
			{

			}
			else if (m_mdPos != MDPos.None)
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
				this.Invalidate();
			}
			//base.OnMouseMove(e);
		}
		// ********************************************************************
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (Grid.ChkMouseMove(e))
			{

			}
			else if(Menu.ChkMouseUp(e))
			{

			}
			else if (Frame.ChkMouseUp(e))
			{

			}
			else if (Caption.ChkMouseUp(e))
			{

			}
			else if (VScrol.ChkMouseUp(e))
			{

			}
			else if (HScrol.ChkMouseUp(e))
			{

			}
			
			m_mdPos = MDPos.None;
			base.OnMouseUp(e);
		}
		// ********************************************************************
		protected override void OnDoubleClick(EventArgs e)
		{
			if(Caption.ChkDoubleClick(e))
			{ 
			}
			else if (m_mdPos == MDPos.Header)
			{
				HeightMax();
				m_mdPos=MDPos.None;	
			}
			base.OnDoubleClick(e);
		}

		// ********************************************************************
		public Point[] Kagi(int pos)
		{
			Point[] ret = new Point[3];
			int x = 0;
			int y = 0;
			switch (pos)
			{
				case 0:
					x = 8;
					y = 4 + Menu.MenuHeight + Sizes.InterHeight;
					ret = new Point[]
					{
						new Point(x, y+12),
						new Point(x, y),
						new Point(x+12,y)
					};
					break;
				case 1:
					x = this.Width-8;
					y = 4 + Menu.MenuHeight + Sizes.InterHeight;
					ret = new Point[]
					{
						new Point(x-12, y),
						new Point(x, y),
						new Point(x,y+12)
					};
					break;
				case 2:
					x = this.Width - 8;
					y = this.Height - 8;
					ret = new Point[]
					{
						new Point(x, y-12),
						new Point(x, y),
						new Point(x-12,y)
					};
					break;
				case 4:
					x = this.Width - 8;
					y = this.Height - 8;
					ret = new Point[]
					{
						new Point(x, y-12),
						new Point(x, y),
						new Point(x-12,y),
						new Point(x-12,y-12)
					};
					break;
				case 3:
					x = 8;
					y = this.Height - 8;
					ret = new Point[]
					{
						new Point(x, y-12),
						new Point(x, y),
						new Point(x+12,y)
					};
					break;
			}
			return ret;
		}
		// ********************************************************************
		private bool IsPaintNow = false;
		protected override void OnPaint(PaintEventArgs e)
		{
			if(IsPaintNow) return;
			IsPaintNow = true;
			//base.OnPaint(e);
			Graphics g = e.Graphics;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			Pen p = new Pen(Color.White);
			SolidBrush sb = new SolidBrush(Color.Black);
			try
			{
				//g.DrawImage(Properties.Resources.Back, new Rectangle(0, 0, this.Width, this.Height));
				T_G.GradBG(g, this.ClientRectangle);
				int w0 = 3;
				int w1 = this.Width - 20 - w0;
				//int h0 = 23;
				int h1 = this.Height -20 -3;

				p.Color = Colors.Kagi;
				p.Width = 3;
				g.DrawLines(p, Kagi(0));
				g.DrawLines(p, Kagi(1));
				g.DrawLines(p, Kagi(3));

				if (m_IsMouseBR)
				{
					sb.Color = Colors.KagiBR;
					g.FillPolygon(sb, Kagi(4));
					p.Color = Colors.KagiBRHi;
					g.DrawLines(p, Kagi(2));

				}
				else
				{
					p.Color = Colors.KagiBR;
					g.DrawLines(p, Kagi(2));
				}

				p.Color = Colors.MenuWaku;
				Rectangle r = new Rectangle(0,0,this.Width-1,this.Height-1);
				g.DrawRectangle(p, r);

				g.DrawImage(Menu.Offscr(), Menu.Location);
				g.DrawImage(Frame.Offscr(), Frame.Location);
				g.DrawImage(Caption.Offscr(), Caption.Location);
				g.DrawImage(Grid.Offscr(), Grid.Location);
				g.DrawImage(VScrol.Offscr(), VScrol.Location);
				g.DrawImage(HScrol.Offscr(), HScrol.Location);
				g.DrawImage(tria, 0,
					Menu.MenuHeight);

				if(IsInputMode)
				{
					int x = Grid.Location.X + Selection.Target * Sizes.CellWidth - Sizes.DispX;
					int yy = Selection.Start;
					if(yy<0) yy = 0;
					int y = Grid.Location.Y + yy * Sizes.CellHeight - Sizes.DispY;
					Rectangle rr = new Rectangle(x, y+2, Sizes.CellWidth, Sizes.CellHeight-4);
					sb.Color = Color.FromArgb(5, 5, 10);
					g.FillRectangle(sb, rr);
					StringFormat.Alignment = StringAlignment.Center;
					StringFormat.LineAlignment = StringAlignment.Center;
					sb.Color = Colors.Moji;
					g.DrawString(m_InputStr,
						MyFont(Grid.FontIndex,Grid.FontSize,FontStyle.Regular), 
						sb, rr, StringFormat);
				}
				Rectangle ri = new Rectangle(
					Sizes.FrameWidth2,
					this.Height - 25,
					Sizes.FrameWidth- Sizes.FrameWidth2,
					25
					);
				sb.Color = Colors.Moji;
				StringFormat.Alignment= StringAlignment.Far;
				StringFormat.LineAlignment = StringAlignment.Near;
				g.DrawString(CellData.Info,
					MyFont(FontIndex, 8, FontStyle.Regular),
					sb, 
					ri,
					StringFormat);


			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}
			IsPaintNow = false;

		}
		// ********************************************************************
		private void SetLocSize()
		{
			Grid.SetLocSize();
			Sizes.SizeSetting();
			Menu.SetLocSize();
			Frame.SetLocSize();
			Caption.SetLocSize();
			VScrol.SetLocSize();
			HScrol.SetLocSize();
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
			Point[] points = new Point[]
			{
				new Point(0, 10),
				new Point(10, 10),
				new Point(10, 0),
				new Point(Width-11, 0),
				new Point(Width-11, 10),
				new Point(Width, 10),
				new Point(Width, Height),
				new Point(0, Height),
			};
			GraphicsPath path = new GraphicsPath();
			path.AddPolygon(points);
			this.Region = new Region(path);
			ChkSize();
			Refresh();
		}
		// ********************************************************************
		private void SetMinMax()
		{

			int cc = CellData.CellCount;
			int fc = CellData.FrameCount;

			Size csz = this.ClientSize;
			Size sz = this.Size;
			int x = sz.Width -csz.Width;
			int y = sz.Height - csz.Height;


			this.MinimumSize = new Size(
				x 
				+ Sizes.FrameWidth
				+ Sizes.InterWidth
				+ Sizes.CellWidth*6
				+ Sizes.InterWidth 
				+ TR_Size.VScrolWidth,
				y 
				+ Sizes.MenuHeight 
				+ Sizes.CaptionHeight2 
				+ Sizes.CaptionHeight 
				+ Sizes.InterHeight 
				+ Sizes.CellHeight*6
				+ Sizes.InterHeight
				+ TR_Size.HScrolHeight
				);
			this.MaximumSize = new Size(
				x
				+ Sizes.FrameWidth
				+ Sizes.InterWidth
				+ Sizes.CellWidth * CellData.CellCount
				+ Sizes.InterWidth
				+ TR_Size.VScrolWidth,
				y
				+ Sizes.MenuHeight
				+ Sizes.CaptionHeight2
				+ Sizes.CaptionHeight
				+ Sizes.InterHeight
				+ Sizes.CellHeight * CellData.FrameCountTrue
				+ Sizes.InterHeight
				+ TR_Size.HScrolHeight
				+ Sizes.InterHeight
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
		public void SetIsInputMode(bool b)
		{
			if(IsInputMode != b)
			{
				IsInputMode = b;
				m_InputStr = "";
				this.Invalidate();
			}
		}
		// ********************************************************************
		private int IsNumberKey(Keys k)
		{
			int ret = -1;
			if((k>=Keys.D0)&&(k<=Keys.D9))
			{
				ret = (int)k - (int)Keys.D0;
			}else if ((k >= Keys.NumPad0) && (k <= Keys.NumPad9))
			{
				ret = (int)k - (int)Keys.NumPad0;
			}else if ((k == Keys.Enter)|| (k == Keys.Return))
			{
				ret = 10;
			}else if(k==Keys.Back)
			{
				ret = 11;
			}
			else if (k == Keys.Delete)
			{
				ret = 12;
			}
			return ret;
		}
		// ********************************************************************
		private bool IsInputMode = false;
		protected override bool ProcessDialogKey(Keys keyData)
		{
#if DEBUG
			this.Text = String.Format("{0}", keyData.ToString());
#endif
			int num = IsNumberKey(keyData);
			if (num >= 0)
			{
				if (IsInputMode)
				{
					if ((num >= 0) && (num <= 9))
					{
						AddValueStr(num);
						this.Invalidate(true);
						return true;
					}
					else if (num == 10)
					{
						//enter
						InputEnter();
						return true;
					}
					else if (num == 11)
					{
						//Back
						if(m_InputStr=="")
						{
							SetIsInputMode(false);
						}
						else
						{
							m_InputStr=m_InputStr.Substring(0, m_InputStr.Length-1);
						}
						this.Invalidate();
						return true;
					}
					else if (num == 12)
					{
						//Delete
						if (m_InputStr == "")
						{
							SetIsInputMode(false);
						}
						else
						{
							m_InputStr = "";
						}
						this.Invalidate();
						return true;
					}
				}
				else if ((num >= 1) && (num <= 9))
				{
					SetIsInputMode(true);
					AddValueStr(num);
					this.Invalidate(true);
					return true;
				}
			}
			else
			{
				if (IsInputMode)
				{
					SetIsInputMode(false);
					return true;
				}
			}


			if (IsInputMode == true) return base.ProcessDialogKey(keyData);

			FuncItem? fi = Funcs.FindKeys(keyData);
			if ((fi != null)&&(fi.Func!=null))
			{
				if (fi.Func()) this.Invalidate();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int v = e.Delta * SystemInformation.MouseWheelScrollLines / 15;
			Sizes.DispY -= v;
			DrawAll();
			base.OnMouseWheel(e);
		}
		// ********************************************************************
		public void ForegroundWindow()
		{
			F_W.SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
		}
		// ********************************************************************

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
					F_Pipe.Client(Program.MyCallBackId, ToArdj()).Wait();
					break;
				case EXEC_MODE.IMPORT_LAYER:
					if ((args.Length > 1))
					{
						Import_layer(args[1]);
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
									if (Open(p) == true)
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
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			Menu.IsActive = true;
			Menu.ChkOffScr();
			this.Invalidate();
		}
		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			Menu.IsActive = false;
			Menu.ChkOffScr();
			this.Invalidate();
		}
	}

}

