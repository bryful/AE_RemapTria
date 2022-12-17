using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BRY;
namespace AE_RemapTria
{
 
    public partial class TR_ColorPlate : TR_DialogControl
    {
        private Color m_DotColor = Color.Transparent;
        [Category("_AE_Remap")]
        public Color DotColor
		{
            get { return m_DotColor; }
            set { m_DotColor = value; this.Invalidate(); }
        }
        private Size m_DotSize = new Size(10,10);
		[Category("_AE_Remap")]
		public Size DotSize
		{
			get { return m_DotSize; }
			set { m_DotSize = value; this.Invalidate(); }
		}
		private Point m_DotLoc = new Point(10, 10);
		[Category("_AE_Remap")]
		public Point DotLoc
		{
			get { return m_DotLoc; }
			set { m_DotLoc = value; this.Invalidate(); }
		}
		public TR_ColorPlate()
        {
            this.Size = new Size(40, 40);
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            SolidBrush sb = new SolidBrush(Color.Transparent);
            try
            {
                sb.Color = BackColor;
                g.FillRectangle(sb, this.ClientRectangle);

				sb.Color = m_FrameColor;
				DrawPadding(g, sb);

				g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, new Rectangle(m_DotLoc, m_DotSize));
				g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

			}
			finally
            {
                sb.Dispose();
            }
        }
    }
}
