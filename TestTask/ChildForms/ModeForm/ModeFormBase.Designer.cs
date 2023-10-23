﻿namespace TestTask.ChildForms.ModeForm
{
    partial class ModeFormBase
    {

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbNameMode = new MaterialSkin.Controls.MaterialTextBox2();
            this.modeModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tlpNameMode = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMode = new System.Windows.Forms.TableLayoutPanel();
            this.tbMaxBottle = new MaterialSkin.Controls.MaterialTextBox2();
            this.tbMaxUsedTips = new MaterialSkin.Controls.MaterialTextBox2();
            this.tlbButtonMode = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.btnClear = new MaterialSkin.Controls.MaterialButton();
            this.btnClose = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.modeModelBindingSource)).BeginInit();
            this.tlpNameMode.SuspendLayout();
            this.tlpMode.SuspendLayout();
            this.tlbButtonMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNameMode
            // 
            this.tbNameMode.AnimateReadOnly = false;
            this.tbNameMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tbNameMode.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbNameMode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.modeModelBindingSource, "Name", true));
            this.tbNameMode.Depth = 0;
            this.tbNameMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNameMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbNameMode.HideSelection = true;
            this.tbNameMode.Hint = "Name";
            this.tbNameMode.LeadingIcon = null;
            this.tbNameMode.Location = new System.Drawing.Point(3, 3);
            this.tbNameMode.MaxLength = 32767;
            this.tbNameMode.MouseState = MaterialSkin.MouseState.OUT;
            this.tbNameMode.Name = "tbNameMode";
            this.tbNameMode.PasswordChar = '\0';
            this.tbNameMode.PrefixSuffixText = null;
            this.tbNameMode.ReadOnly = false;
            this.tbNameMode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbNameMode.SelectedText = "";
            this.tbNameMode.SelectionLength = 0;
            this.tbNameMode.SelectionStart = 0;
            this.tbNameMode.ShortcutsEnabled = true;
            this.tbNameMode.Size = new System.Drawing.Size(384, 48);
            this.tbNameMode.TabIndex = 1;
            this.tbNameMode.TabStop = false;
            this.tbNameMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbNameMode.TrailingIcon = null;
            this.tbNameMode.UseSystemPasswordChar = false;
            // 
            // modeModelBindingSource
            // 
            this.modeModelBindingSource.DataSource = typeof(TestTask.BindingItem.UserBinding.ModeModel);
            // 
            // tlpNameMode
            // 
            this.tlpNameMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpNameMode.ColumnCount = 1;
            this.tlpNameMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNameMode.Controls.Add(this.tbNameMode, 0, 0);
            this.tlpNameMode.Location = new System.Drawing.Point(5, 5);
            this.tlpNameMode.MinimumSize = new System.Drawing.Size(390, 50);
            this.tlpNameMode.Name = "tlpNameMode";
            this.tlpNameMode.RowCount = 1;
            this.tlpNameMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNameMode.Size = new System.Drawing.Size(390, 50);
            this.tlpNameMode.TabIndex = 2;
            // 
            // tlpMode
            // 
            this.tlpMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMode.ColumnCount = 2;
            this.tlpMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMode.Controls.Add(this.tbMaxBottle, 0, 0);
            this.tlpMode.Controls.Add(this.tbMaxUsedTips, 1, 0);
            this.tlpMode.Location = new System.Drawing.Point(5, 61);
            this.tlpMode.MinimumSize = new System.Drawing.Size(390, 50);
            this.tlpMode.Name = "tlpMode";
            this.tlpMode.RowCount = 1;
            this.tlpMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMode.Size = new System.Drawing.Size(390, 50);
            this.tlpMode.TabIndex = 3;
            // 
            // tbMaxBottle
            // 
            this.tbMaxBottle.AnimateReadOnly = false;
            this.tbMaxBottle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tbMaxBottle.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbMaxBottle.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.modeModelBindingSource, "MaxBottleNumber", true));
            this.tbMaxBottle.Depth = 0;
            this.tbMaxBottle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMaxBottle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbMaxBottle.HideSelection = true;
            this.tbMaxBottle.Hint = "MaxBottleNumber";
            this.tbMaxBottle.LeadingIcon = null;
            this.tbMaxBottle.Location = new System.Drawing.Point(3, 3);
            this.tbMaxBottle.MaxLength = 32767;
            this.tbMaxBottle.MouseState = MaterialSkin.MouseState.OUT;
            this.tbMaxBottle.Name = "tbMaxBottle";
            this.tbMaxBottle.PasswordChar = '\0';
            this.tbMaxBottle.PrefixSuffixText = null;
            this.tbMaxBottle.ReadOnly = false;
            this.tbMaxBottle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbMaxBottle.SelectedText = "";
            this.tbMaxBottle.SelectionLength = 0;
            this.tbMaxBottle.SelectionStart = 0;
            this.tbMaxBottle.ShortcutsEnabled = true;
            this.tbMaxBottle.Size = new System.Drawing.Size(189, 48);
            this.tbMaxBottle.TabIndex = 0;
            this.tbMaxBottle.TabStop = false;
            this.tbMaxBottle.Text = "0";
            this.tbMaxBottle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tbMaxBottle.TrailingIcon = null;
            this.tbMaxBottle.UseSystemPasswordChar = false;
            this.tbMaxBottle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbMaxBottle_KeyPress);
            // 
            // tbMaxUsedTips
            // 
            this.tbMaxUsedTips.AnimateReadOnly = false;
            this.tbMaxUsedTips.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tbMaxUsedTips.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbMaxUsedTips.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.modeModelBindingSource, "MaxUsedTips", true));
            this.tbMaxUsedTips.Depth = 0;
            this.tbMaxUsedTips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMaxUsedTips.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbMaxUsedTips.HideSelection = true;
            this.tbMaxUsedTips.Hint = "MaxUsedType";
            this.tbMaxUsedTips.LeadingIcon = null;
            this.tbMaxUsedTips.Location = new System.Drawing.Point(198, 3);
            this.tbMaxUsedTips.MaxLength = 32767;
            this.tbMaxUsedTips.MouseState = MaterialSkin.MouseState.OUT;
            this.tbMaxUsedTips.Name = "tbMaxUsedTips";
            this.tbMaxUsedTips.PasswordChar = '\0';
            this.tbMaxUsedTips.PrefixSuffixText = null;
            this.tbMaxUsedTips.ReadOnly = false;
            this.tbMaxUsedTips.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbMaxUsedTips.SelectedText = "";
            this.tbMaxUsedTips.SelectionLength = 0;
            this.tbMaxUsedTips.SelectionStart = 0;
            this.tbMaxUsedTips.ShortcutsEnabled = true;
            this.tbMaxUsedTips.Size = new System.Drawing.Size(189, 48);
            this.tbMaxUsedTips.TabIndex = 1;
            this.tbMaxUsedTips.TabStop = false;
            this.tbMaxUsedTips.Text = "0";
            this.tbMaxUsedTips.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tbMaxUsedTips.TrailingIcon = null;
            this.tbMaxUsedTips.UseSystemPasswordChar = false;
            this.tbMaxUsedTips.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbMaxUsedType_KeyPress);
            // 
            // tlbButtonMode
            // 
            this.tlbButtonMode.ColumnCount = 3;
            this.tlbButtonMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlbButtonMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlbButtonMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlbButtonMode.Controls.Add(this.btnSave, 0, 0);
            this.tlbButtonMode.Controls.Add(this.btnClear, 1, 0);
            this.tlbButtonMode.Controls.Add(this.btnClose, 2, 0);
            this.tlbButtonMode.Location = new System.Drawing.Point(5, 115);
            this.tlbButtonMode.MinimumSize = new System.Drawing.Size(390, 45);
            this.tlbButtonMode.Name = "tlbButtonMode";
            this.tlbButtonMode.RowCount = 1;
            this.tlbButtonMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlbButtonMode.Size = new System.Drawing.Size(390, 45);
            this.tlbButtonMode.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(4, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(122, 33);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSave.UseAccentColor = false;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClear.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnClear.Depth = 0;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.HighEmphasis = true;
            this.btnClear.Icon = null;
            this.btnClear.Location = new System.Drawing.Point(134, 6);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClear.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClear.Name = "btnClear";
            this.btnClear.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnClear.Size = new System.Drawing.Size(122, 33);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Default Value";
            this.btnClear.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnClear.UseAccentColor = false;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnClose.Depth = 0;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.HighEmphasis = true;
            this.btnClose.Icon = null;
            this.btnClose.Location = new System.Drawing.Point(264, 6);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClose.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClose.Name = "btnClose";
            this.btnClose.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnClose.Size = new System.Drawing.Size(122, 33);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnClose.UseAccentColor = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // ModeFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 161);
            this.Controls.Add(this.tlbButtonMode);
            this.Controls.Add(this.tlpMode);
            this.Controls.Add(this.tlpNameMode);
            this.MinimumSize = new System.Drawing.Size(420, 200);
            this.Name = "ModeFormBase";
            this.Text = "Mode Form";
            this.Load += new System.EventHandler(this.AddModeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.modeModelBindingSource)).EndInit();
            this.tlpNameMode.ResumeLayout(false);
            this.tlpMode.ResumeLayout(false);
            this.tlbButtonMode.ResumeLayout(false);
            this.tlbButtonMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected MaterialSkin.Controls.MaterialTextBox2 tbNameMode;
        protected System.Windows.Forms.TableLayoutPanel tlpNameMode;
        protected System.Windows.Forms.TableLayoutPanel tlpMode;
        protected MaterialSkin.Controls.MaterialTextBox2 tbMaxBottle;
        protected MaterialSkin.Controls.MaterialTextBox2 tbMaxUsedTips;
        protected System.Windows.Forms.TableLayoutPanel tlbButtonMode;
        protected MaterialSkin.Controls.MaterialButton btnSave;
        protected MaterialSkin.Controls.MaterialButton btnClear;
        protected MaterialSkin.Controls.MaterialButton btnClose;
        protected System.Windows.Forms.BindingSource modeModelBindingSource;
        private System.ComponentModel.IContainer components;
    }
}