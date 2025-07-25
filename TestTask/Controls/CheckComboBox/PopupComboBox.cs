using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestTask.Controls.CheckComboBox
{
    [ToolboxBitmap(typeof(System.Windows.Forms.ComboBox)),
        ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"),
        Description("Displays an editable text box with a drop-down list of permitted values.")]
    public partial class PopupComboBox : ComboBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopupControl.PopupComboBox" /> class.
        /// </summary>
        public PopupComboBox()
        {
            InitializeComponent();
            base.DropDownHeight = base.DropDownWidth = 1;
            base.IntegralHeight = false;
        }

        /// <summary>
        /// The pop-up wrapper for the dropDownControl. 
        /// Made PROTECTED instead of PRIVATE so descendent classes can set its Resizable property.
        /// Note however the pop-up properties must be set after the dropDownControl is assigned, since this 
        /// popup wrapper is recreated when the dropDownControl is assigned.
        /// </summary>
        protected Popup dropDown = new(new CheckBoxComboBoxListControlContainer());

        private Control dropDownControl;
        /// <summary>
        /// Gets or sets the drop down control.
        /// </summary>
        /// <value>The drop down control.</value>        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control DropDownControl
        {
            get
            {
                return dropDownControl;
            }
            set
            {
                if (dropDownControl == value)
                {
                    return;
                }

                dropDownControl = value;
                dropDown = new Popup(value);
            }
        }

        /// <summary>
        /// Shows the drop down.
        /// </summary>
        public void ShowDropDown() => dropDown?.Show(this);

        /// <summary>
        /// Hides the drop down.
        /// </summary>
        public void HideDropDown() => dropDown?.Hide();

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (NativeMethods.WM_REFLECT + NativeMethods.WM_COMMAND) && NativeMethods.HIWORD(m.WParam) == NativeMethods.CBN_DROPDOWN)
            {
                var localDropDown = dropDown;
                if (localDropDown == null)
                {
                    return;
                }

                // Blocks a redisplay when the user closes the control by clicking 
                // on the combobox.
                BeginInvoke(new MethodInvoker(() =>
                {
                    var timeSpan = DateTime.Now.Subtract(localDropDown.LastClosedTimeStamp);

                    if (timeSpan.TotalMilliseconds > 100)
                    {
                        ShowDropDown();
                    }
                }));
                return;
            }
            base.WndProc(ref m);
        }

        #region " Unused Properties "

        /// <summary>This property is not relevant for this class.</summary>
        /// <returns>This property is not relevant for this class.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new int DropDownWidth
        {
            get => base.DropDownWidth;
            set { base.DropDownWidth = value; }
        }

        /// <summary>This property is not relevant for this class.</summary>
        /// <returns>This property is not relevant for this class.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new int DropDownHeight
        {
            get => base.DropDownHeight;
            set
            {
                dropDown.Height = value;
                base.DropDownHeight = value;
            }
        }

        /// <summary>This property is not relevant for this class.</summary>
        /// <returns>This property is not relevant for this class.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new bool IntegralHeight
        {
            get => base.IntegralHeight;
            set => base.IntegralHeight = value;
        }

        /// <summary>This property is not relevant for this class.</summary>
        /// <returns>This property is not relevant for this class.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new ObjectCollection Items
        {
            get => base.Items;
        }

        /// <summary>This property is not relevant for this class.</summary>
        /// <returns>This property is not relevant for this class.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new int ItemHeight
        {
            get => base.ItemHeight;
            set => base.ItemHeight = value;
        }

        #endregion
    }
}
