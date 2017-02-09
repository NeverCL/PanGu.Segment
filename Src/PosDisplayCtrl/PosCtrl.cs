/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PanGu;

namespace PosDisplayCtrl
{
    public partial class PosCtrl : UserControl
    {
        const int POS_PER_LINE = 4;
        const int POS_TOP = 0;
        const int POS_LEFT = 0;
        const int POS_WIDTH = 120;
        const int POS_HIGHT = 30;

        Hashtable m_PosTable = new Hashtable();
        int m_Pos = 0;

        public int Pos
        {
            get
            {
                RefreshPos();
                return m_Pos;
            }

            set
            {
                m_Pos = value;
                Display();
            }
        }

        static public String GetChsPos(POS pos)
        {
            switch (pos)
            {
                case POS.POS_D_A:	//	形容词 形语素
                    return "形容词 形语素";

                case POS.POS_D_B:	//	区别词 区别语素
                    return "区别词 区别语素";

                case POS.POS_D_C:	//	连词 连语素
                    return "连词 连语素";

                case POS.POS_D_D:	//	副词 副语素
                    return "副词 副语素";

                case POS.POS_D_E:	//	叹词 叹语素
                    return "叹词 叹语素";

                case POS.POS_D_F:	//	方位词 方位语素
                    return "方位词 方位语素";

                case POS.POS_D_I:	//	成语
                    return "成语";

                case POS.POS_D_L:	//	习语
                    return "习语";

                case POS.POS_A_M:	//	数词 数语素
                    return "数词 数语素";

                case POS.POS_D_MQ:   //	数量词
                    return "数量词";

                case POS.POS_D_N:	//	名词 名语素
                    return "名词 名语素";

                case POS.POS_D_O:	//	拟声词
                    return "拟声词";

                case POS.POS_D_P:	//	介词
                    return "介词";

                case POS.POS_A_Q:	//	量词 量语素
                    return "量词 量语素";

                case POS.POS_D_R:	//	代词 代语素
                    return "代词 代语素";

                case POS.POS_D_S:	//	处所词
                    return "处所词";

                case POS.POS_D_T:	//	时间词
                    return "时间词";

                case POS.POS_D_U:	//	助词 助语素
                    return "助词 助语素";

                case POS.POS_D_V:	//	动词 动语素
                    return "动词 动语素";

                case POS.POS_D_W:	//	标点符号
                    return "标点符号";

                case POS.POS_D_X:	//	非语素字
                    return "非语素字";

                case POS.POS_D_Y:	//	语气词 语气语素
                    return "语气词 语气语素";

                case POS.POS_D_Z:	//	状态词
                    return "状态词";

                case POS.POS_A_NR://	人名
                    return "人名";

                case POS.POS_A_NS://	地名
                    return "地名";

                case POS.POS_A_NT://	机构团体
                    return "机构团体";

                case POS.POS_A_NX://	外文字符
                    return "外文字符";

                case POS.POS_A_NZ://	其他专名
                    return "其他专名";

                case POS.POS_D_H:	//	前接成分
                    return "前接成分";

                case POS.POS_D_K:	//	后接成分
                    return "后接成分";

                case POS.POS_UNK://  未知词性
                    return "未知词性";

                default:
                    return "未知词性";

            }
        }


        private void CreatePosCheckBox()
        {
            int pos = 0x40000000;
            this.Width = POS_PER_LINE * POS_WIDTH;
            this.Height = POS_HIGHT * (32 / POS_PER_LINE + 1);

            int j = POS_TOP;
            for (int i = 0; i < 31; i++)
            {
                if (i % POS_PER_LINE == 0)
                {
                    j += POS_HIGHT;
                }

                if (pos == 1)
                {
                    pos = 0;
                }

                POS tPos = (POS)pos;
                CheckBox checkBoxPos = new CheckBox();
                checkBoxPos.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
                m_PosTable[tPos] = checkBoxPos;
                checkBoxPos.Tag = tPos;
                checkBoxPos.Parent = this;
                checkBoxPos.Name = tPos.ToString();
                checkBoxPos.Text = GetChsPos(tPos);

                checkBoxPos.Top = j;
                checkBoxPos.Width = POS_WIDTH;
                checkBoxPos.Left = POS_LEFT + POS_WIDTH * (i % POS_PER_LINE);
                pos >>= 1;
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox checkBox = (CheckBox)sender;

            if (checkBox.Checked)
            {
                checkBox.ForeColor = Color.Red;

                CheckBox posCheckBox = (CheckBox)m_PosTable[(POS)0];

                if ((POS)checkBox.Tag == POS.POS_UNK)
                {
                    foreach (object key in m_PosTable.Keys)
                    {
                        posCheckBox = (CheckBox)m_PosTable[key];

                        if ((POS)posCheckBox.Tag == POS.POS_UNK)
                        {
                            continue;
                        }

                        posCheckBox.Checked = false;
                    }
                }
                else
                {
                    posCheckBox.Checked = false;
                }
            }
            else
            {
                checkBox.ForeColor = Color.Black;
            }

        }

        private void RefreshPos()
        {
            CheckBox posCheckBox;

            posCheckBox = (CheckBox)m_PosTable[(POS)0];
            m_Pos = 0;

            int pos = 0x40000000;

            for (int i = 0; i < 30; i++)
            {
                POS tPos = (POS)pos;
                posCheckBox = (CheckBox)m_PosTable[tPos];

                if (posCheckBox.Checked)
                {
                    m_Pos |= pos;
                }

                pos >>= 1;
            }
        }

        private void Display()
        {
            CheckBox posCheckBox;

            posCheckBox = (CheckBox)m_PosTable[(POS)0];

            if (m_Pos == 0)
            {
                foreach(object key in m_PosTable.Keys)
                {
                    ((CheckBox)m_PosTable[key]).Checked = false;
                }

                posCheckBox = (CheckBox)m_PosTable[(POS)0];
                posCheckBox.Checked = true;
                return;
            }
            else
            {
                posCheckBox.Checked = false;
            }

            int pos = 0x40000000;

            for (int i = 0; i < 30; i++)
            {
                POS tPos = (POS)pos;
                posCheckBox = (CheckBox)m_PosTable[tPos];

                if ((m_Pos & pos) != 0)
                {
                    posCheckBox.Checked = true;
                }
                else
                {
                    posCheckBox.Checked = false;
                }

                pos >>= 1;
            }
        }

        public PosCtrl()
        {
            InitializeComponent();

            CreatePosCheckBox();

            Display();
        }
    }
}