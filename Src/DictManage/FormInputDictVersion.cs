using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DictManage
{
    public partial class FormInputDictVersion : Form
    {
        DialogResult _Result = DialogResult.Cancel;

        public FormInputDictVersion()
        {
            InitializeComponent();
        }

        public string Version
        {
            get
            {
                return textBoxVersion.Text;
            }

            set
            {
                textBoxVersion.Text = value;
            }
        }

        new public DialogResult ShowDialog()
        {
            base.ShowDialog();

            return _Result;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            textBoxVersion.Text = textBoxVersion.Text.Trim();
            if (textBoxVersion.Text == "")
            {
                MessageBox.Show("版本号不能为空", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxVersion.Text.Length > 8)
            {
                MessageBox.Show("版本号字符串长度不能大于8", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Result = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
