using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace TestTask.Controls.CheckComboBox
{
    /// <summary>
    /// https://github.com/sgissinger/CheckBoxComboBox/blob/master/CheckBoxComboBox/CheckBoxComboBox.cs
    /// </summary>
    public partial class CheckBoxComboBox : PopupComboBox
    {
        #region CONSTRUCTOR
        /// <summary>
        /// TODO: Documentation Constructor
        /// </summary>
        public CheckBoxComboBox()
            : base()
        {
            InitializeComponent();

            _checkBoxProperties = new CheckBoxProperties();
            _checkBoxProperties.PropertyChanged += new EventHandler(CheckBoxProperties_PropertyChanged);

            // Dumps the ListControl in a(nother) Container to ensure the ScrollBar on the ListControl does not
            // Paint over the Size grip. Setting the Padding or Margin on the Popup or host control does
            // not work as I expected. I don't think it can work that way.
            var ContainerControl = new CheckBoxComboBoxListControlContainer();
            _checkBoxComboBoxListControl = new CheckBoxComboBoxListControl(this);
            _checkBoxComboBoxListControl.Items.CheckBoxCheckedChanged += new EventHandler(Items_CheckBoxCheckedChanged);
            ContainerControl.Controls.Add(_checkBoxComboBoxListControl);

            // This padding spaces neatly on the left-hand side and allows space for the size grip at the bottom.
            ContainerControl.Padding = new Padding(4, 0, 0, 14);

            // The ListControl FILLS the ListContainer.
            _checkBoxComboBoxListControl.Dock = DockStyle.Fill;

            // The DropDownControl used by the base class. Will be wrapped in a popup by the base class.
            DropDownControl = ContainerControl;

            // Must be set after the DropDownControl is set, since the popup is recreated.
            // NOTE: I made the dropDown protected so that it can be accessible here. It was private.
            dropDown.Resizable = true;
        }
        #endregion

        #region MEMBERS
        /// <summary>
        /// The checkbox list control. The public CheckBoxItems property provides a direct reference to its Items.
        /// </summary>
        internal CheckBoxComboBoxListControl _checkBoxComboBoxListControl;
        /// <summary>
        /// In DataBinding operations, this property will be used as the DisplayMember in the CheckBoxComboBoxListBox.
        /// The normal/existing "DisplayMember" property is used by the TextBox of the ComboBox to display 
        /// a concatenated Text of the items selected. This concatenation and its formatting however is controlled 
        /// by the Binded object, since it owns that property.
        /// </summary>
        private string _displayMemberSingleItem = null;
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private string _textSeparator = ", ";
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        internal bool _mustAddHiddenItem = false;
        #endregion

        #region PRIVATE OPERATIONS
        /// <summary>
        /// Builds a CSV string of the items selected.
        /// </summary>
        internal string GetCSVText(bool skipFirstItem)
        {
            var listText = string.Empty;

            var startIndex = DropDownStyle == ComboBoxStyle.DropDownList
                                                        && DataSource == null
                                                        && skipFirstItem ? 1 : 0;

            for (var index = startIndex; index <= _checkBoxComboBoxListControl.Items.Count - 1; index++)
            {
                var item = _checkBoxComboBoxListControl.Items[index];

                if (item.Checked)
                {
                    listText += string.IsNullOrEmpty(listText) ? item.Text : string.Format("{0}{1}", TextSeparator, item.Text);
                }
            }

            return listText;
        }

        /// <summary>
        /// TODO: Documentation WndProc
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            // 323 : Item Added
            // 331 : Clearing
            if (DropDownStyle == ComboBoxStyle.DropDownList
                && DataSource == null
                && m.Msg == 331)
            {
                _mustAddHiddenItem = true;
            }

            base.WndProc(ref m);
        }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// A direct reference to the Items of CheckBoxComboBoxListControl.
        /// You can use it to Get or Set the Checked status of items manually if you want.
        /// But do not manipulate the List itself directly, e.g. Adding and Removing, 
        /// since the list is synchronised when shown with the ComboBox.Items. So for changing 
        /// the list contents, use Items instead.
        /// </summary>
        [Browsable(false)]
        public CheckBoxComboBoxItemList CheckBoxItems
        {
            get
            {
                // Added to ensure the CheckBoxItems are ALWAYS available for modification via code.
                if (_checkBoxComboBoxListControl.Items.Count != Items.Count)
                {
                    _checkBoxComboBoxListControl.SynchroniseControlsWithComboBoxItems();
                }

                return _checkBoxComboBoxListControl.Items;
            }
        }
        /// <summary>
        /// The DataSource of the combobox. Refreshes the CheckBox wrappers when this is set.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new object DataSource
        {
            get => base.DataSource;
            set
            {
                base.DataSource = value;

                // This ensures that at least the checkboxitems are available to be initialised.
                if (!string.IsNullOrEmpty(ValueMember))
                {
                    _checkBoxComboBoxListControl.SynchroniseControlsWithComboBoxItems();
                }
            }
        }
        /// <summary>
        /// The ValueMember of the combobox. Refreshes the CheckBox wrappers when this is set.
        /// </summary>
        /// <summary>
        /// The ValueMember of the combobox. Refreshes the CheckBox wrappers when this is set.
        /// </summary>          
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string ValueMember
        {
            get => base.ValueMember;
            set
            {
                base.ValueMember = value;

                // This ensures that at least the checkboxitems are available to be initialised.
                if (!string.IsNullOrEmpty(ValueMember))
                {
                    _checkBoxComboBoxListControl.SynchroniseControlsWithComboBoxItems();
                }
            }
        }
        /// <summary>
        /// In DataBinding operations, this property will be used as the DisplayMember in the CheckBoxComboBoxListBox.
        /// The normal/existing "DisplayMember" property is used by the TextBox of the ComboBox to display 
        /// a concatenated Text of the items selected. This concatenation however is controlled by the Binded 
        /// object, since it owns that property.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DisplayMemberSingleItem
        {
            get
            {
                return string.IsNullOrEmpty(_displayMemberSingleItem)
                    ? DisplayMember
                    : _displayMemberSingleItem;
            }
            set => _displayMemberSingleItem = value;
        }
        /// <summary>
        /// TODO: Documentation Property
        /// </summary>
        [Browsable(true)]
        [DefaultValue(", ")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TextSeparator
        {
            get => _textSeparator;
            set { _textSeparator = value; }
        }
        /// <summary>
        /// Made this property Browsable again, since the Base Popup hides it. This class uses it again.
        /// Gets an object representing the collection of the items contained in this 
        /// System.Windows.Forms.ComboBox.
        /// </summary>
        /// <returns>A System.Windows.Forms.ComboBox.ObjectCollection representing the items in 
        /// the System.Windows.Forms.ComboBox.
        /// </returns>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new ObjectCollection Items => base.Items;
        #endregion

        #region EVENTS & EVENT HANDLERS
        /// <summary>
        /// TODO: Documentation Event
        /// </summary>
        public event EventHandler CheckBoxCheckedChanged;

        /// <summary>
        /// TODO: Documentation Items_CheckBoxCheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Items_CheckBoxCheckedChanged(object sender, EventArgs e) => OnCheckBoxCheckedChanged(sender, e);

        /// <summary>
        /// TODO: Documentation OnCheckBoxCheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            var listText = GetCSVText(true);

            // The DropDownList style seems to require that the text
            // part of the "textbox" should match a single item.
            if (DropDownStyle != ComboBoxStyle.DropDownList)
            {
                Text = listText;
            }

            // This refreshes the Text of the first item (which is not visible)
            else if (DataSource == null)
            {
                Items[0] = listText;

                // Keep the hidden item and first checkbox item in 
                // sync in order to ensure the Synchronise process can match the items.
                CheckBoxItems[0].ComboBoxItem = listText;
            }

            CheckBoxCheckedChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Will add an invisible item when the style is DropDownList,
        /// to help maintain the correct text in main TextBox.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDropDownStyleChanged(EventArgs e)
        {
            base.OnDropDownStyleChanged(e);

            if (DropDownStyle == ComboBoxStyle.DropDownList
                && DataSource == null
                && !DesignMode)
            {
                _mustAddHiddenItem = true;
            }
        }

        /// <summary>
        /// When the ComboBox is resized, the width of the dropdown 
        /// is also resized to match the width of the ComboBox. I think it looks better.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            dropDown.Size = new Size(Width, DropDownControl.Height);

            base.OnResize(e);
        }
        #endregion

        #region PUBLIC OPERATIONS
        /// <summary>
        /// A function to clear/reset the list.
        /// (Ubiklou : http://www.codeproject.com/KB/combobox/extending_combobox.aspx?msg=2526813#xx2526813xx)
        /// </summary>
        public void Clear()
        {
            Items.Clear();

            if (DropDownStyle == ComboBoxStyle.DropDownList && DataSource == null)
            {
                _mustAddHiddenItem = true;
            }
        }
        /// <summary>
        /// Uncheck all items.
        /// </summary>
        public void ClearSelection()
        {
            foreach (var item in CheckBoxItems)
            {
                if (item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
        #endregion

        #region CHECKBOX PROPERTIES (DEFAULTS)
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private CheckBoxProperties _checkBoxProperties;
        /// <summary>
        /// The properties that will be assigned to the checkboxes as default values.
        /// </summary>
        [Description("The properties that will be assigned to the checkboxes as default values.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CheckBoxProperties CheckBoxProperties
        {
            get => _checkBoxProperties;
            set
            {
                _checkBoxProperties = value;
                CheckBoxProperties_PropertyChanged(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// TODO: Documentation _CheckBoxProperties_PropertyChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxProperties_PropertyChanged(object sender, EventArgs e)
        {
            foreach (var item in CheckBoxItems)
            {
                item.ApplyProperties(CheckBoxProperties);
            }
        }
        #endregion
    }

    /// <summary>
    /// A container control for the ListControl to ensure the ScrollBar on the ListControl does not
    /// Paint over the Size grip. Setting the Padding or Margin on the Popup or host control does
    /// not work as I expected.
    /// </summary>
    [ToolboxItem(false)]
    public class CheckBoxComboBoxListControlContainer : UserControl
    {
        #region CONSTRUCTOR

        public CheckBoxComboBoxListControlContainer()
            : base()
        {
            BackColor = SystemColors.Window;
            BorderStyle = BorderStyle.FixedSingle;
            AutoScaleMode = AutoScaleMode.Inherit;
            ResizeRedraw = true;

            // If you don't set this, then resize operations cause an error in the base class.
            MinimumSize = new Size(1, 1);
            MaximumSize = new Size(500, 500);
        }
        #endregion

        #region RESIZE OVERRIDE REQUIRED BY THE POPUP CONTROL

        /// <summary>
        /// Prescribed by the Popup class to ensure Resize operations work correctly.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if ((Parent as Popup).ProcessResizing(ref m))
            {
                return;
            }

            base.WndProc(ref m);
        }
        #endregion
    }

    /// <summary>
    /// This ListControl that pops up to the User. It contains the CheckBoxComboBoxItems. 
    /// The items are docked DockStyle.Top in this control.
    /// </summary>
    [ToolboxItem(false)]
    public class CheckBoxComboBoxListControl : ScrollableControl
    {
        #region CONSTRUCTOR

        public CheckBoxComboBoxListControl(CheckBoxComboBox owner)
            : base()
        {
            DoubleBuffered = true;

            _checkBoxComboBox = owner;
            _items = new CheckBoxComboBoxItemList(_checkBoxComboBox);
            BackColor = SystemColors.Window;
            // AutoScaleMode = AutoScaleMode.Inherit;
            AutoScroll = true;
            ResizeRedraw = true;
            // if you don't set this, a Resize operation causes an error in the base class.
            MinimumSize = new Size(1, 1);
            MaximumSize = new Size(500, 500);
        }

        #endregion

        #region PRIVATE PROPERTIES

        /// <summary>
        /// Simply a reference to the CheckBoxComboBox.
        /// </summary>
        private readonly CheckBoxComboBox _checkBoxComboBox;
        /// <summary>
        /// A Typed list of ComboBoxCheckBoxItems.
        /// </summary>
        private readonly CheckBoxComboBoxItemList _items;

        #endregion

        public CheckBoxComboBoxItemList Items { get => _items; }

        #region RESIZE OVERRIDE REQUIRED BY THE POPUP CONTROL

        /// <summary>
        /// Prescribed by the Popup control to enable Resize operations.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if ((Parent.Parent as Popup).ProcessResizing(ref m))
            {
                return;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region PROTECTED MEMBERS

        protected override void OnVisibleChanged(EventArgs e)
        {
            // Synchronises the CheckBox list with the items in the ComboBox.
            SynchroniseControlsWithComboBoxItems();
            base.OnVisibleChanged(e);
        }
        /// <summary>
        /// Maintains the controls displayed in the list by keeping them in sync with the actual 
        /// items in the combobox. (e.g. removing and adding as well as ordering)
        /// </summary>
        public void SynchroniseControlsWithComboBoxItems()
        {
            SuspendLayout();

            if (_checkBoxComboBox._mustAddHiddenItem && _checkBoxComboBox.DataSource == null)
            {
                _checkBoxComboBox.Items.Insert(0, _checkBoxComboBox.GetCSVText(false)); // INVISIBLE ITEM
                _checkBoxComboBox.SelectedIndex = 0;
                _checkBoxComboBox._mustAddHiddenItem = false;
            }

            Controls.Clear();

            #region Disposes all items that are no longer in the combo box list

            for (var index = _items.Count - 1; index >= 0; index--)
            {
                var Item = _items[index];
                if (!_checkBoxComboBox.Items.Contains(Item.ComboBoxItem))
                {
                    _items.Remove(Item);
                    Item.Dispose();
                }
            }
            #endregion

            #region Recreate the list in the same order of the combo box items

            bool HasHiddenItem = _checkBoxComboBox.DropDownStyle == ComboBoxStyle.DropDownList
                                    && _checkBoxComboBox.DataSource == null
                                    && !DesignMode;

            CheckBoxComboBoxItemList NewList = new CheckBoxComboBoxItemList(_checkBoxComboBox);

            for (int Index0 = 0; Index0 <= _checkBoxComboBox.Items.Count - 1; Index0++)
            {
                Object obj = _checkBoxComboBox.Items[Index0];
                CheckBoxComboBoxItem item = null;

                // The hidden item could match any other item when only
                // one other item was selected.
                if (Index0 == 0 && HasHiddenItem && _items.Count > 0)
                {
                    item = _items[0];
                }
                else
                {
                    int StartIndex = HasHiddenItem
                        ? 1 // Skip the hidden item, it could match 
                        : 0;
                    for (int Index1 = StartIndex; Index1 <= _items.Count - 1; Index1++)
                    {
                        if (_items[Index1].ComboBoxItem == obj)
                        {
                            item = _items[Index1];
                            break;
                        }
                    }
                }

                if (item == null)
                {
                    item = new CheckBoxComboBoxItem(_checkBoxComboBox, obj);
                    item.ApplyProperties(_checkBoxComboBox.CheckBoxProperties);
                }

                NewList.Add(item);
                item.Dock = DockStyle.Top;
            }
            _items.Clear();
            _items.AddRange(NewList);

            #endregion
            #region Add the items to the controls in reversed order to maintain correct docking order

            if (NewList.Count > 0)
            {
                // This reverse helps to maintain correct docking order.
                NewList.Reverse();
                // If you get an error here that "Cannot convert to the desired 
                // type, it probably means the controls are not binding correctly.
                // The Checked property is binded to the ValueMember property. 
                // It must be a bool for example.
                Controls.AddRange(NewList.ToArray());
            }

            #endregion

            // Keep the first item invisible
            if (_checkBoxComboBox.DropDownStyle == ComboBoxStyle.DropDownList
                && _checkBoxComboBox.DataSource == null
                && !DesignMode)
            {
                _checkBoxComboBox.CheckBoxItems[0].Visible = false;
            }

            ResumeLayout();
        }

        #endregion
    }

    /// <summary>
    /// The CheckBox items displayed in the Popup of the ComboBox.
    /// </summary>
    [ToolboxItem(false)]
    public class CheckBoxComboBoxItem : CheckBox
    {
        #region CONSTRUCTOR

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner">A reference to the CheckBoxComboBox.</param>
        /// <param name="comboBoxItem">A reference to the item in the ComboBox.Items that this object is extending.</param>
        public CheckBoxComboBoxItem(CheckBoxComboBox owner, object comboBoxItem)
            : base()
        {
            DoubleBuffered = true;
            _CheckBoxComboBox = owner;
            ComboBoxItem = comboBoxItem;

            if (_CheckBoxComboBox.DataSource != null)
            {
                AddBindings();
            }
            else
            {
                Text = comboBoxItem.ToString();
            }
        }
        #endregion

        #region PRIVATE PROPERTIES

        /// <summary>
        /// A reference to the CheckBoxComboBox.
        /// </summary>
        private readonly CheckBoxComboBox _CheckBoxComboBox;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// A reference to the Item in ComboBox.Items that this object is extending.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object ComboBoxItem { get; internal set; }

        #endregion

        #region BINDING HELPER

        /// <summary>
        /// When using Data Binding operations via the DataSource property of the ComboBox. This
        /// adds the required Bindings for the CheckBoxes.
        /// </summary>
        public void AddBindings()
        {
            // Note, the text uses "DisplayMemberSingleItem", not "DisplayMember" (unless its not assigned)
            DataBindings.Add("Text", ComboBoxItem, _CheckBoxComboBox.DisplayMemberSingleItem);

            // The ValueMember must be a bool type property usable by the CheckBox.Checked.
            DataBindings.Add("Checked", ComboBoxItem, _CheckBoxComboBox.ValueMember, false,

                // This helps to maintain proper selection state in the Binded object,
                // even when the controls are added and removed.
                DataSourceUpdateMode.OnPropertyChanged, false, null, null);

            // Helps to maintain the Checked status of this
            // checkbox before the control is visible
            if (ComboBoxItem is INotifyPropertyChanged changed)
            {
                changed.PropertyChanged += new PropertyChangedEventHandler(CheckBoxComboBoxItem_PropertyChanged);
            }
        }

        #endregion

        #region PROTECTED MEMBERS

        protected override void OnCheckedChanged(EventArgs e)
        {
            // Found that when this event is raised, the bool value of the binded item is not yet updated.
            if (_CheckBoxComboBox.DataSource != null)
            {
                PropertyInfo PI = ComboBoxItem.GetType().GetProperty(_CheckBoxComboBox.ValueMember);
                PI.SetValue(ComboBoxItem, Checked, null);
            }
            base.OnCheckedChanged(e);
            // Forces a refresh of the Text displayed in the main TextBox of the ComboBox,
            // since that Text will most probably represent a concatenation of selected values.
            // Also see DisplayMemberSingleItem on the CheckBoxComboBox for more information.
            if (_CheckBoxComboBox.DataSource != null)
            {
                string OldDisplayMember = _CheckBoxComboBox.DisplayMember;
                _CheckBoxComboBox.DisplayMember = null;
                _CheckBoxComboBox.DisplayMember = OldDisplayMember;
            }
        }

        #endregion

        #region HELPER MEMBERS

        internal void ApplyProperties(CheckBoxProperties properties)
        {
            Appearance = properties.Appearance;
            AutoCheck = properties.AutoCheck;
            AutoEllipsis = properties.AutoEllipsis;
            AutoSize = properties.AutoSize;
            CheckAlign = properties.CheckAlign;
            FlatAppearance.BorderColor = properties.FlatAppearanceBorderColor;
            FlatAppearance.BorderSize = properties.FlatAppearanceBorderSize;
            FlatAppearance.CheckedBackColor = properties.FlatAppearanceCheckedBackColor;
            FlatAppearance.MouseDownBackColor = properties.FlatAppearanceMouseDownBackColor;
            FlatAppearance.MouseOverBackColor = properties.FlatAppearanceMouseOverBackColor;
            FlatStyle = properties.FlatStyle;
            ForeColor = properties.ForeColor;
            RightToLeft = properties.RightToLeft;
            TextAlign = properties.TextAlign;
            ThreeState = properties.ThreeState;
        }

        #endregion

        #region EVENT HANDLERS - ComboBoxItem (DataSource)

        /// <summary>
        /// Added this handler because the control doesn't seem 
        /// to initialize correctly until shown for the first
        /// time, which also means the summary text value
        /// of the combo is out of sync initially.
        /// </summary>
        private void CheckBoxComboBoxItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == _CheckBoxComboBox.ValueMember)
            {
                Checked = (bool)ComboBoxItem
                                    .GetType()
                                    .GetProperty(_CheckBoxComboBox.ValueMember)
                                    .GetValue(ComboBoxItem, null);
            }
        }

        #endregion
    }

    /// <summary>
    /// A Typed List of the CheckBox items.
    /// Simply a wrapper for the CheckBoxComboBox.Items. A list of CheckBoxComboBoxItem objects.
    /// This List is automatically synchronised with the Items of the ComboBox and extended to
    /// handle the additional boolean value. That said, do not Add or Remove using this List, 
    /// it will be lost or regenerated from the ComboBox.Items.
    /// </summary>
    [ToolboxItem(false)]
    public class CheckBoxComboBoxItemList(CheckBoxComboBox checkBoxComboBox) : List<CheckBoxComboBoxItem>
    {

        #region CONSTRUCTORS

        #endregion

        #region PRIVATE FIELDS

        private readonly CheckBoxComboBox _CheckBoxComboBox = checkBoxComboBox;

        #endregion

        #region EVENTS, This could be moved to the list control if needed

        public event EventHandler CheckBoxCheckedChanged;

        protected void OnCheckBoxCheckedChanged(object sender, EventArgs e) => CheckBoxCheckedChanged?.Invoke(sender, e);

        private void item_CheckedChanged(object sender, EventArgs e) => OnCheckBoxCheckedChanged(sender, e);

        #endregion

        #region LIST MEMBERS & OBSOLETE INDICATORS

        [Obsolete("Do not add items to this list directly. Use the ComboBox items instead.", false)]
        public new void Add(CheckBoxComboBoxItem item)
        {
            item.CheckedChanged += new EventHandler(item_CheckedChanged);
            base.Add(item);
        }

        public new void AddRange(IEnumerable<CheckBoxComboBoxItem> collection)
        {
            foreach (var Item in collection)
            {
                Item.CheckedChanged += new EventHandler(item_CheckedChanged);
            }

            base.AddRange(collection);
        }

        public new void Clear()
        {
            foreach (var Item in this)
            {
                Item.CheckedChanged -= item_CheckedChanged;
            }

            base.Clear();
        }

        [Obsolete("Do not remove items from this list directly. Use the ComboBox items instead.", false)]
        public new bool Remove(CheckBoxComboBoxItem item)
        {
            item.CheckedChanged -= item_CheckedChanged;
            return base.Remove(item);
        }

        #endregion

        #region DEFAULT PROPERTIES

        /// <summary>
        /// Returns the item with the specified displayName or Text.
        /// </summary>
        public CheckBoxComboBoxItem this[string displayName]
        {
            get
            {   // An invisible item exists in this scenario to help with the Text displayed in the TextBox of the Combo
                // 1 Ubiklou : 2008-04-28 : Ignore first item.
                // (http://www.codeproject.com/KB/combobox/extending_combobox.aspx?fid=476622&df=90&mpp=25&noise=3&sort=Position&view=Quick&select=2526813&fr=1#xx2526813xx)
                var StartIndex = _CheckBoxComboBox.DropDownStyle
                                    == ComboBoxStyle.DropDownList
                                    && _CheckBoxComboBox.DataSource
                                    == null ? 1 : 0;

                for (var Index = StartIndex; Index <= Count - 1; Index++)
                {
                    var Item = this[Index];

                    string Text;

                    if (string.IsNullOrEmpty(Item.Text)
                        && Item.DataBindings != null
                        && Item.DataBindings["Text"] != null)
                    {
                        PropertyInfo PropertyInfo = Item.ComboBoxItem.GetType()
                                                                     .GetProperty(Item.DataBindings["Text"].BindingMemberInfo.BindingMember);
                        Text = (string)PropertyInfo.GetValue(Item.ComboBoxItem, null);
                    }
                    else
                    {
                        Text = Item.Text;
                    }

                    if (Text.CompareTo(displayName) == 0)
                    {
                        return Item;
                    }
                }
                throw new ArgumentOutOfRangeException(string.Format("\"{0}\" does not exist in this combo box.", displayName));
            }
        }

        #endregion
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CheckBoxProperties
    {
        public CheckBoxProperties()
        {
        }

        #region PRIVATE PROPERTIES

        private Appearance _appearance = Appearance.Normal;
        private bool _autoSize = false;
        private bool _autoCheck = true;
        private bool _autoEllipsis = false;
        private ContentAlignment _CheckAlign = ContentAlignment.MiddleLeft;
        private Color _flatAppearanceBorderColor = Color.Empty;
        private int _flatAppearanceBorderSize = 1;
        private Color _flatAppearanceCheckedBackColor = Color.Empty;
        private Color _flatAppearanceMouseDownBackColor = Color.Empty;
        private Color _flatAppearanceMouseOverBackColor = Color.Empty;
        private FlatStyle _flatStyle = FlatStyle.Standard;
        private Color _foreColor = SystemColors.ControlText;
        private RightToLeft _rightToLeft = RightToLeft.No;
        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
        private bool _threeState = false;

        #endregion

        #region PUBLIC PROPERTIES

        [DefaultValue(Appearance.Normal)]
        public Appearance Appearance
        {
            get => _appearance;
            set
            {
                _appearance = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(true)]
        public bool AutoCheck
        {
            get => _autoCheck;
            set
            {
                _autoCheck = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(false)]
        public bool AutoEllipsis
        {
            get => _autoEllipsis;
            set { _autoEllipsis = value; OnPropertyChanged(); }
        }
        [DefaultValue(false)]
        public bool AutoSize
        {
            get => _autoSize;
            set { _autoSize = true; OnPropertyChanged(); }
        }
        [DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment CheckAlign
        {
            get => _CheckAlign;
            set
            {
                _CheckAlign = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(typeof(Color), "")]
        public Color FlatAppearanceBorderColor
        {
            get => _flatAppearanceBorderColor;
            set
            {
                _flatAppearanceBorderColor = value; OnPropertyChanged();
            }
        }
        [DefaultValue(1)]
        public int FlatAppearanceBorderSize
        {
            get => _flatAppearanceBorderSize;
            set
            {
                _flatAppearanceBorderSize = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(typeof(Color), "")]
        public Color FlatAppearanceCheckedBackColor
        {
            get => _flatAppearanceCheckedBackColor;
            set
            {
                _flatAppearanceCheckedBackColor = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(typeof(Color), "")]
        public Color FlatAppearanceMouseDownBackColor
        {
            get => _flatAppearanceMouseDownBackColor;
            set
            {
                _flatAppearanceMouseDownBackColor = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(typeof(Color), "")]
        public Color FlatAppearanceMouseOverBackColor
        {
            get => _flatAppearanceMouseOverBackColor;
            set
            {
                _flatAppearanceMouseOverBackColor = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(FlatStyle.Standard)]
        public FlatStyle FlatStyle
        {
            get => _flatStyle;
            set
            {
                _flatStyle = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(typeof(SystemColors), "ControlText")]
        public Color ForeColor
        {
            get => _foreColor;
            set
            {
                _foreColor = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(RightToLeft.No)]
        public RightToLeft RightToLeft
        {
            get => _rightToLeft;
            set
            {
                _rightToLeft = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set
            {
                _textAlign = value;
                OnPropertyChanged();
            }
        }
        [DefaultValue(false)]
        public bool ThreeState
        {
            get => _threeState;
            set
            {
                _threeState = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region EVENTS AND EVENT CALLERS

        /// <summary>
        /// Called when any property changes.
        /// </summary>
        public event EventHandler PropertyChanged;

        protected void OnPropertyChanged() => PropertyChanged?.Invoke(this, EventArgs.Empty);

        #endregion
    }
}
