using MaterialSkin;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestTask.Controls
{
    public partial class BaseUserControl : UserControl, IMaterialControl
    {

        public BaseUserControl() => InitializeComponent();

        //IMaterialControl item
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Depth { get; set; }

        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MouseState MouseState { get; set; }
    }
}
