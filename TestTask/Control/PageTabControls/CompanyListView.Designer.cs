﻿namespace TestTask.Control.PageTabControls
{
    partial class CompanyListView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpFilter = new System.Windows.Forms.TableLayoutPanel();
            labelSortField = new System.Windows.Forms.Label();
            labelTypeSort = new System.Windows.Forms.Label();
            labelSearchName = new System.Windows.Forms.Label();
            buttonClearFilter = new MaterialSkin.Controls.MaterialButton();
            buttonUseFilter = new MaterialSkin.Controls.MaterialButton();
            cmbSortField = new System.Windows.Forms.ComboBox();
            sortCompaniesBindingSource = new System.Windows.Forms.BindingSource(components);
            cmbTypeSort = new System.Windows.Forms.ComboBox();
            itemsBindingSourceTypeField = new System.Windows.Forms.BindingSource(components);
            typeSortFieldBindingSource = new System.Windows.Forms.BindingSource(components);
            tbSearchStrName = new System.Windows.Forms.TextBox();
            listView = new ListViewControl();
            tlpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sortCompaniesBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemsBindingSourceTypeField).BeginInit();
            ((System.ComponentModel.ISupportInitialize)typeSortFieldBindingSource).BeginInit();
            SuspendLayout();
            // 
            // tlpFilter
            // 
            tlpFilter.ColumnCount = 8;
            tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            tlpFilter.Controls.Add(labelSortField, 4, 0);
            tlpFilter.Controls.Add(labelTypeSort, 2, 0);
            tlpFilter.Controls.Add(labelSearchName, 0, 0);
            tlpFilter.Controls.Add(buttonClearFilter, 7, 0);
            tlpFilter.Controls.Add(buttonUseFilter, 6, 0);
            tlpFilter.Controls.Add(cmbSortField, 5, 0);
            tlpFilter.Controls.Add(cmbTypeSort, 3, 0);
            tlpFilter.Controls.Add(tbSearchStrName, 1, 0);
            tlpFilter.Location = new System.Drawing.Point(1, -1);
            tlpFilter.Margin = new System.Windows.Forms.Padding(1);
            tlpFilter.Name = "tlpFilter";
            tlpFilter.RowCount = 1;
            tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpFilter.Size = new System.Drawing.Size(874, 37);
            tlpFilter.TabIndex = 9;
            // 
            // labelSortField
            // 
            labelSortField.AutoSize = true;
            labelSortField.Dock = System.Windows.Forms.DockStyle.Bottom;
            labelSortField.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelSortField.Location = new System.Drawing.Point(420, 13);
            labelSortField.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            labelSortField.Name = "labelSortField";
            labelSortField.Size = new System.Drawing.Size(81, 19);
            labelSortField.TabIndex = 12;
            labelSortField.Text = "Column:";
            labelSortField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTypeSort
            // 
            labelTypeSort.AutoSize = true;
            labelTypeSort.Dock = System.Windows.Forms.DockStyle.Bottom;
            labelTypeSort.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelTypeSort.Location = new System.Drawing.Point(211, 13);
            labelTypeSort.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            labelTypeSort.Name = "labelTypeSort";
            labelTypeSort.Size = new System.Drawing.Size(72, 19);
            labelTypeSort.TabIndex = 11;
            labelTypeSort.Text = "Sort by:";
            labelTypeSort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSearchName
            // 
            labelSearchName.AutoSize = true;
            labelSearchName.Dock = System.Windows.Forms.DockStyle.Bottom;
            labelSearchName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelSearchName.Location = new System.Drawing.Point(3, 13);
            labelSearchName.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            labelSearchName.Name = "labelSearchName";
            labelSearchName.Size = new System.Drawing.Size(98, 19);
            labelSearchName.TabIndex = 10;
            labelSearchName.Text = "Search ";
            labelSearchName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonClearFilter
            // 
            buttonClearFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonClearFilter.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            buttonClearFilter.Depth = 0;
            buttonClearFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            buttonClearFilter.HighEmphasis = true;
            buttonClearFilter.Icon = null;
            buttonClearFilter.Location = new System.Drawing.Point(761, 7);
            buttonClearFilter.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            buttonClearFilter.MouseState = MaterialSkin.MouseState.HOVER;
            buttonClearFilter.Name = "buttonClearFilter";
            buttonClearFilter.NoAccentTextColor = System.Drawing.Color.Empty;
            buttonClearFilter.Size = new System.Drawing.Size(108, 23);
            buttonClearFilter.TabIndex = 1;
            buttonClearFilter.Text = "Clear Filter";
            buttonClearFilter.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            buttonClearFilter.UseAccentColor = false;
            buttonClearFilter.UseVisualStyleBackColor = true;
            buttonClearFilter.Click += ButtonClearFilter_Click;
            // 
            // buttonUseFilter
            // 
            buttonUseFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonUseFilter.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            buttonUseFilter.Depth = 0;
            buttonUseFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            buttonUseFilter.HighEmphasis = true;
            buttonUseFilter.Icon = null;
            buttonUseFilter.Location = new System.Drawing.Point(648, 7);
            buttonUseFilter.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            buttonUseFilter.MouseState = MaterialSkin.MouseState.HOVER;
            buttonUseFilter.Name = "buttonUseFilter";
            buttonUseFilter.NoAccentTextColor = System.Drawing.Color.Empty;
            buttonUseFilter.Size = new System.Drawing.Size(103, 23);
            buttonUseFilter.TabIndex = 4;
            buttonUseFilter.Text = "Use Filter";
            buttonUseFilter.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            buttonUseFilter.UseAccentColor = false;
            buttonUseFilter.UseVisualStyleBackColor = true;
            buttonUseFilter.Click += ButtonUseFilter_Click;
            // 
            // cmbSortField
            // 
            cmbSortField.DataSource = sortCompaniesBindingSource;
            cmbSortField.Dock = System.Windows.Forms.DockStyle.Bottom;
            cmbSortField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSortField.FormattingEnabled = true;
            cmbSortField.Location = new System.Drawing.Point(508, 11);
            cmbSortField.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbSortField.Name = "cmbSortField";
            cmbSortField.Size = new System.Drawing.Size(131, 23);
            cmbSortField.TabIndex = 5;
            cmbSortField.SelectedIndexChanged += CmbSortField_Changed;
            // 
            // sortCompaniesBindingSource
            // 
            sortCompaniesBindingSource.DataSource = typeof(BindingItem.Pages.Companies.SortCompaniesModel);
            // 
            // cmbTypeSort
            // 
            cmbTypeSort.DataSource = itemsBindingSourceTypeField;
            cmbTypeSort.Dock = System.Windows.Forms.DockStyle.Bottom;
            cmbTypeSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbTypeSort.FormattingEnabled = true;
            cmbTypeSort.Location = new System.Drawing.Point(290, 11);
            cmbTypeSort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbTypeSort.Name = "cmbTypeSort";
            cmbTypeSort.Size = new System.Drawing.Size(123, 23);
            cmbTypeSort.TabIndex = 8;
            cmbTypeSort.SelectedIndexChanged += CmbTypeSort_Changed;
            // 
            // itemsBindingSourceTypeField
            // 
            itemsBindingSourceTypeField.DataMember = "Items";
            itemsBindingSourceTypeField.DataSource = typeSortFieldBindingSource;
            // 
            // typeSortFieldBindingSource
            // 
            typeSortFieldBindingSource.DataSource = typeof(BindingItem.Pages.TypeSortFieldModel);
            // 
            // tbSearchStrName
            // 
            tbSearchStrName.Dock = System.Windows.Forms.DockStyle.Bottom;
            tbSearchStrName.Location = new System.Drawing.Point(108, 11);
            tbSearchStrName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tbSearchStrName.Name = "tbSearchStrName";
            tbSearchStrName.Size = new System.Drawing.Size(96, 23);
            tbSearchStrName.TabIndex = 3;
            // 
            // listView
            // 
            listView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listView.AutoSize = true;
            listView.Depth = 0;
            listView.Location = new System.Drawing.Point(0, 39);
            listView.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            listView.MouseState = MaterialSkin.MouseState.HOVER;
            listView.Name = "listView";
            listView.Size = new System.Drawing.Size(877, 343);
            listView.TabIndex = 10;
            listView.SizeChanged += ListView_SizeChanged;
            // 
            // CompanyListView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(listView);
            Controls.Add(tlpFilter);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "CompanyListView";
            Size = new System.Drawing.Size(875, 381);
            tlpFilter.ResumeLayout(false);
            tlpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sortCompaniesBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemsBindingSourceTypeField).EndInit();
            ((System.ComponentModel.ISupportInitialize)typeSortFieldBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListViewControl listView;
        private System.Windows.Forms.TableLayoutPanel tlpFilter;
        private MaterialSkin.Controls.MaterialButton buttonClearFilter;
        private System.Windows.Forms.TextBox tbSearchStrName;
        private MaterialSkin.Controls.MaterialButton buttonUseFilter;
        private System.Windows.Forms.ComboBox cmbSortField;
        private System.Windows.Forms.BindingSource sortCompaniesBindingSource;
        private System.Windows.Forms.ComboBox cmbTypeSort;
        private System.Windows.Forms.BindingSource itemsBindingSourceTypeField;
        private System.Windows.Forms.BindingSource typeSortFieldBindingSource;
        private System.Windows.Forms.Label labelSearchName;
        private System.Windows.Forms.Label labelSortField;
        private System.Windows.Forms.Label labelTypeSort;
    }
}
