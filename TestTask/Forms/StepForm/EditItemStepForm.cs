﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestTask.BindingItem.UserBinding.StepBinding;
using TestTask.Core.Models.Modes;
using TestTask.Core.Models.Steeps;

namespace TestTask.Forms.StepForm
{
    public class EditItemStepForm : StepFormBase
    {
        private Step _oldStep;
        private Step _editStep;

        public EditItemStepForm(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            Text = "Edit Step";
        }

        public void Initialize(List<Mode> modes, Step oldItem)
        {
            _modes = new SelectMode(modes);
            _oldStep = oldItem;
        }

        protected override void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!IsDataFilled(out var message))
            {
                _messageBox.ShowInfo(message);
                return;
            }

            _editStep = GetStepModel().ToStep(_oldStep.Id);
            if (_oldStep.Equals(_editStep))
            {
                _messageBox.ShowInfo("The step has not been modified.");
                DialogResult = DialogResult.Cancel;
            }

            DialogResult = DialogResult.OK;
        }

        protected override void AddStepForm_Load(object sender, EventArgs e)
        {
            selectModeBindingSource.DataSource = _modes.Items;
            _modes.SetValueMode(_oldStep.ModeId);
            SetDefaultValueData();
        }

        protected override void SetDefaultValueData()
        {
            cmbModeValue.SelectedItem = _modes.Mode;
            tbTimer.Text = _oldStep.Timer.ToString();
            tbDestination.Text = _oldStep.Destination.ToString();
            tbSpeed.Text = _oldStep.Speed.ToString();
            tbType.Text = _oldStep.Type;
            tbVolume.Text = _oldStep.Volume.ToString();
        }

        public Step GetEditStep() => _editStep;
    }
}
