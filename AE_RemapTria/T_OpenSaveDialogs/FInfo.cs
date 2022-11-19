using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRY;
namespace AE_RemapTria
{
    public enum FInfoType
    {
        Drive,
        Dir,
        File
    }

    public class FInfo
    {
        public int Index = -1;
        private DriveInfo? m_drive = null;
        private DirectoryInfo? m_dir = null;
        private FileInfo? m_file = null;
        private FInfoType m_IsType = FInfoType.Drive;
        public FInfoType FInfoType
        {
            get { return m_IsType; }
        }
        private char m_DriveLetter = 'A';
        public char DriveLetter { get { return m_DriveLetter; } }
        // ********************************************************
        public bool Exists
        {
            get
            {
                bool ret = false;
                switch (m_IsType)
                {
                    case FInfoType.Drive:
                    case FInfoType.Dir:
                        if (m_dir != null && m_drive != null)
                        {
                            ret = m_dir.Exists;
                        }
                        break;
                    case FInfoType.File:
                        if (m_dir != null && m_drive != null && m_file != null)
                        {
                            ret = m_file.Exists;
                        }
                        break;
                }
                return ret;
            }
        }
        public DirectoryInfo? Directory
        {
            get { return m_dir; }
            set
            {
                if (value == null) return;
                if (m_IsType == FInfoType.Dir || m_IsType == FInfoType.Drive)
                {
                    if (m_dir == null || m_dir.Name != value.Name)
                    {
                        SetDir(value);
                    }
                }
            }
        }
        public string FullName
        {
            get
            {
                string ret = "";
                if (m_IsType == FInfoType.File)
                {
                    if (m_file != null)
                    {
                        ret = m_file.FullName;
                    }
                }
                else
                {
                    if (m_dir != null)
                    {
                        ret = m_dir.FullName;
                    }
                }
                return ret;
            }
            set
            {
                switch (m_IsType)
                {
                    case FInfoType.Drive:
					case FInfoType.Dir:
                        SetDir(value);
                        break;
                    case FInfoType.File:
                        SetFile(value);
                        break;
				}

			}

        }
        public string Name
        {
            get
            {
                string ret = "";
				switch (m_IsType)
				{
					case FInfoType.Drive:
						ret = m_DriveLetter + "";
						break;
					case FInfoType.Dir:
						if (m_dir != null)
							ret = Path.GetFileName(m_dir.FullName);
						break;
					case FInfoType.File:
						if (m_file != null)
							ret = Path.GetFileName(m_file.FullName);
						break;
				}
                return ret;
			}
		}
        private string m_Caption = "";        
        public string Caption
        {
            get
            {
                string ret = m_Caption;
                if (ret == "")
                {
                    ret = Name;
                }
                return ret;
            }
            set
            {
                m_Caption = value;
            }
        }
        public bool IsDir
        {
            get { return (m_IsType != FInfoType.File); }
        }
		public bool IsFile
		{
			get { return (m_IsType == FInfoType.File); }
		}
        public bool Hidden
        {
            get
            {
                bool ret = false;
                switch (m_IsType)
                {
                    case FInfoType.Drive:
                        break;
                    case FInfoType.Dir:
                        if (m_dir != null)
                        {
                            ret = (m_dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                        }
                        break;
					case FInfoType.File:
                        if (m_file != null)
                        {
                            ret = (m_file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                        }
						break;

				}
                return ret;
			}
        }


        // ********************************************************
        public FInfo(DriveInfo di, int idx = -1)
        {
            m_IsType = FInfoType.Drive;
            Index = idx;
            SetDrive(di);
        }
        public FInfo(DirectoryInfo di, int idx = -1)
        {
            m_IsType = FInfoType.Dir;
            Index = idx;
            SetDir(di);
        }
        public FInfo(FileInfo fi, int idx = -1)
        {
            m_IsType = FInfoType.File;
            Index = idx;
            SetFile(fi);
        }
        public void SetDrive(DriveInfo di)
        {
            SetDrive(di.Name);
        }
        public void SetDrive(string p)
        {
            try
            {
                m_drive = new DriveInfo(p);
                m_DriveLetter = m_drive.Name.Substring(0, 1).ToUpper()[0];
                m_dir = new DirectoryInfo(m_drive.Name);
            }
            catch
            {
                m_drive = null;
            }
        }
        public void SetDir(DirectoryInfo di)
        {
            SetDir(di.FullName);
        }
        public void SetDir(string p)
        {
            try
            {
                m_dir = new DirectoryInfo(p);
                m_drive = new DriveInfo(m_dir.FullName);
                m_DriveLetter = m_drive.Name.Substring(0, 1).ToUpper()[0];
            }
            catch
            {
                m_dir = null;
            }
        }
        public void SetFile(FileInfo fi)
        {
            SetFile(fi.FullName);
        }
        public void SetFile(string p)
        {
            try
            {
                m_file = new FileInfo(p);
                m_dir = new DirectoryInfo(Path.GetDirectoryName(m_file.FullName)); ;
                m_drive = new DriveInfo(m_file.FullName);
                m_DriveLetter = m_drive.Name.Substring(0, 1).ToUpper()[0];
            }
            catch
            {
                m_file = null;
            }
        }
        public bool IsExt(string[] exts)
        {
            bool ret = false;
			if ((exts.Length == 0)||(exts==null)) return true;
			string ext = T_Def.GetExt(Name).ToLower();
            if(exts.Length>0)
            {
                for(int i = 0; i < exts.Length; i++)
                {
                    if(ext==exts[i])
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;

        }
		public Image IconImage()
		{
			return F_W.FileAssociatedImage(FullName, false);
		}

	}
}
