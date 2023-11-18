﻿using TestTask.BindingItem.Pages;

namespace TestTask.Control.PageTabControls
{
    partial class CategoryListView
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
            this.components = new System.ComponentModel.Container();
            this.tlpFilter = new System.Windows.Forms.TableLayoutPanel();
            this.labelSortName = new MaterialSkin.Controls.MaterialLabel();
            this.buttonClearFilter = new MaterialSkin.Controls.MaterialButton();
            this.labelSearchName = new MaterialSkin.Controls.MaterialLabel();
            this.tbSearchStrName = new System.Windows.Forms.TextBox();
            this.buttonUseFilter = new MaterialSkin.Controls.MaterialButton();
            this.cmbSortName = new System.Windows.Forms.ComboBox();
            this.itemsBindingSourceSortName = new System.Windows.Forms.BindingSource(this.components);
            this.sortPageCategoriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listView = new TestTask.Control.ListViewControl();
            this.tlpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemsBindingSourceSortName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortPageCategoriesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpFilter
            // 
            this.tlpFilter.ColumnCount = 6;
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFilter.Controls.Add(this.labelSortName, 2, 0);
            this.tlpFilter.Controls.Add(this.buttonClearFilter, 5, 0);
            this.tlpFilter.Controls.Add(this.labelSearchName, 0, 0);
            this.tlpFilter.Controls.Add(this.tbSearchStrName, 1, 0);
            this.tlpFilter.Controls.Add(this.buttonUseFilter, 4, 0);
            this.tlpFilter.Controls.Add(this.cmbSortName, 3, 0);
            this.tlpFilter.Location = new System.Drawing.Point(1, -1);
            this.tlpFilter.Margin = new System.Windows.Forms.Padding(1);
            this.tlpFilter.Name = "tlpFilter";
            this.tlpFilter.RowCount = 1;
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.Size = new System.Drawing.Size(749, 32);
            this.tlpFilter.TabIndex = 7;
            // 
            // labelSortName
            // 
            this.labelSortName.AutoSize = true;
            this.labelSortName.Depth = 0;
            this.labelSortName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelSortName.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelSortName.FontType = MaterialSkin.MaterialSkinManager.fontType.Body2;
            this.labelSortName.Location = new System.Drawing.Point(264, 12);
            this.labelSortName.Margin = new System.Windows.Forms.Padding(3);
            this.labelSortName.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelSortName.Name = "labelSortName";
            this.labelSortName.Size = new System.Drawing.Size(106, 17);
            this.labelSortName.TabIndex = 6;
            this.labelSortName.Text = "Sort name by";
            this.labelSortName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonClearFilter
            // 
            this.buttonClearFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonClearFilter.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.buttonClearFilter.Depth = 0;
            this.buttonClearFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonClearFilter.HighEmphasis = true;
            this.buttonClearFilter.Icon = null;
            this.buttonClearFilter.Location = new System.Drawing.Point(638, 6);
            this.buttonClearFilter.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.buttonClearFilter.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonClearFilter.Name = "buttonClearFilter";
            this.buttonClearFilter.NoAccentTextColor = System.Drawing.Color.Empty;
            this.buttonClearFilter.Size = new System.Drawing.Size(107, 20);
            this.buttonClearFilter.TabIndex = 1;
            this.buttonClearFilter.Text = "Clear Filter";
            this.buttonClearFilter.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.buttonClearFilter.UseAccentColor = false;
            this.buttonClearFilter.UseVisualStyleBackColor = true;
            this.buttonClearFilter.Click += new System.EventHandler(this.ButtonClearFilter_Click);
            // 
            // labelSearchName
            // 
            this.labelSearchName.AutoSize = true;
            this.labelSearchName.Depth = 0;
            this.labelSearchName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelSearchName.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelSearchName.FontType = MaterialSkin.MaterialSkinManager.fontType.Body2;
            this.labelSearchName.Location = new System.Drawing.Point(3, 12);
            this.labelSearchName.Margin = new System.Windows.Forms.Padding(3);
            this.labelSearchName.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelSearchName.Name = "labelSearchName";
            this.labelSearchName.Size = new System.Drawing.Size(106, 17);
            this.labelSearchName.TabIndex = 2;
            this.labelSearchName.Text = "Search name";
            this.labelSearchName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbSearchStrName
            // 
            this.tbSearchStrName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbSearchStrName.Location = new System.Drawing.Point(115, 9);
            this.tbSearchStrName.Name = "tbSearchStrName";
            this.tbSearchStrName.Size = new System.Drawing.Size(143, 20);
            this.tbSearchStrName.TabIndex = 3;
            // 
            // buttonUseFilter
            // 
            this.buttonUseFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonUseFilter.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.buttonUseFilter.Depth = 0;
            this.buttonUseFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonUseFilter.HighEmphasis = true;
            this.buttonUseFilter.Icon = null;
            this.buttonUseFilter.Location = new System.Drawing.Point(526, 6);
            this.buttonUseFilter.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.buttonUseFilter.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonUseFilter.Name = "buttonUseFilter";
            this.buttonUseFilter.NoAccentTextColor = System.Drawing.Color.Empty;
            this.buttonUseFilter.Size = new System.Drawing.Size(104, 20);
            this.buttonUseFilter.TabIndex = 4;
            this.buttonUseFilter.Text = "Use Filter";
            this.buttonUseFilter.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.buttonUseFilter.UseAccentColor = false;
            this.buttonUseFilter.UseVisualStyleBackColor = true;
            this.buttonUseFilter.Click += new System.EventHandler(this.ButtonUseFilter_Click);
            // 
            // cmbSortName
            // 
            this.cmbSortName.DataSource = this.itemsBindingSourceSortName;
            this.cmbSortName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmbSortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortName.FormattingEnabled = true;
            this.cmbSortName.Location = new System.Drawing.Point(376, 8);
            this.cmbSortName.Name = "cmbSortName";
            this.cmbSortName.Size = new System.Drawing.Size(143, 21);
            this.cmbSortName.TabIndex = 5;
            this.cmbSortName.SelectedIndexChanged += new System.EventHandler(this.CmbSortName_Changed);
            // 
            // itemsBindingSourceSortName
            // 
            this.itemsBindingSourceSortName.DataMember = "Items";
            this.itemsBindingSourceSortName.DataSource = this.sortPageCategoriesBindingSource;
            // 
            // sortPageCategoriesBindingSource
            // 
            this.sortPageCategoriesBindingSource.DataSource = typeof(TestTask.BindingItem.Pages.TypeSortField);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.AutoSize = true;
            this.listView.Depth = 0;
            this.listView.Location = new System.Drawing.Point(0, 34);
            this.listView.MouseState = MaterialSkin.MouseState.HOVER;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(751, 297);
            this.listView.TabIndex = 8;
            this.listView.SizeChanged += new System.EventHandler(this.ListView_SizeChanged);
            // 
            // CategoryListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Controls.Add(this.tlpFilter);
            this.Name = "CategoryListView";
            this.Size = new System.Drawing.Size(750, 330);
            this.tlpFilter.ResumeLayout(false);
            this.tlpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemsBindingSourceSortName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortPageCategoriesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpFilter;
        private MaterialSkin.Controls.MaterialButton buttonClearFilter;
        private MaterialSkin.Controls.MaterialLabel labelSearchName;
        private System.Windows.Forms.TextBox tbSearchStrName;
        private ListViewControl listView;
        private System.Windows.Forms.ComboBox cmbSortName;
        private MaterialSkin.Controls.MaterialLabel labelSortName;
        private System.Windows.Forms.BindingSource itemsBindingSourceSortName;
        private System.Windows.Forms.BindingSource sortPageCategoriesBindingSource;
        private MaterialSkin.Controls.MaterialButton buttonUseFilter;
    }
}
