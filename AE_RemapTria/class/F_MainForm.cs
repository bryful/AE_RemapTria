using BRY;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.IO.Pipes;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace SkeltonWinForm
{


	public partial class MainForm : Form
	{
		private string m_FileName = "";
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
		// ********************************************************************
		public MainForm()
		{
			InitializeComponent();
		}

		// ********************************************************************
		private void Form1_Load(object sender, EventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) return;
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
			Command(Environment.GetCommandLineArgs().Skip(1).ToArray(), PIPECALL.StartupExec);
		}
		// ********************************************************************
		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			PrefFile pf = new PrefFile(this);
			pf.StoreForm();
			pf.Save();
		}
		// ********************************************************************
		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
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
				string s = textBox1.Text;
				File.WriteAllText(p, s);
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
						textBox1.Text = str;
						textBox1.Select(0, 0);
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
		public void ForegroundWindow()
		{
			F_W.SetForegroundWindow(this.Handle);
		}
		// ********************************************************************
		public void Command(string[] args, PIPECALL IsPipe = PIPECALL.StartupExec)
		{
			bool QuitFlag = false;			

			F_Args _args = new F_Args(args);
			if(_args.OptionCount>0)
			{
				for (int i = 0; i < _args.OptionCount; i++)
				{
					F_ArgItem? item = _args.Option(i);
					if (item == null) continue;
					switch(item.Option.ToLower())
					{
						case "tocenter":
						case "center":
							this.Invoke((Action)(() => {
								ToCenter();
							})); 
							break;
						case "foregroundWindow":
						case "foreground":
							ForegroundWindow();
							break;
						case "load":
						case "ld":
							int idx = item.Index + 1;
						if (idx < _args.Count)
						{
							if (_args[idx].IsOption == false)
							{
								Import(_args[idx].Name);
							}
						}
						break;
						case "save":
						case "sv":
							int idx2 = item.Index + 1;
							if (idx2 < _args.Count)
							{
								if (_args[idx2].IsOption == false)
								{
									Export(_args[idx2].Name);
								}
							}
							break;
						case "exit":
						case "quit":
							if ((_args.Count == 1) && ((IsPipe == PIPECALL.DoubleExec) || (IsPipe == PIPECALL.PipeExec)))
							{
								QuitFlag = true;
							}
							break;
						case "callback":
							string s = textBox1.Text;
							F_Pipe.Client(Program.MyCallBackId, s).Wait();
							break;

					}

				}

			}
			else
			{
				if (_args.Count > 0)
				{
					if (_args.Count == 1)
					{
						Import(_args[0].Name);
					}
					else
					{
						textBox1.Text = _args.ArgsString();
						textBox1.Select(0, 0);
						//this.Invoke((Action)(() => {
						//}));
					}
				}
			}
			if(QuitFlag) Application.Exit();
		}
		// *******************************************************************************



	}
}