using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DictManage
{
    public partial class FormEncoder : Form
    {
        bool m_Ok;

        public FormEncoder()
        {
            InitializeComponent();
        }

        public String Encoding
        {
            get
            {
                return comboBoxEncoder.Text;
            }
        }

        new public DialogResult ShowDialog()
        {
            m_Ok = false;
            base.ShowDialog();

            if (m_Ok)
            {
                return DialogResult.OK;
            }
            else
            {
                return DialogResult.Cancel;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            m_Ok = true;
            Close();
        }
    }
}