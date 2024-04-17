using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine_Integrated_Karting_Experience
{
    public partial class FrmEventCRUD : Form
    {
        public string[,] dataCRUD = new string[,]
                {
                    { "cock", "penis", "balls" },
                    { "big", "small", "tinyy" },
                    { "123", "3423423", "324" }
                };

        public FrmEventCRUD()
        {
            InitializeComponent();

            //for (int i = 0; i < dataCRUD.GetLength(0); i++)
            //{
            //    dataGridView1.Columns[i].Name = (i).ToString();
            //}

            for (int i = 0; i < dataCRUD.GetLength(0); i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                for (int j = 0; j < dataCRUD.GetLength(1); j++)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = dataCRUD[i, j] });

                }
                dataGridView1.Rows.Add(row);
            }

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        //TODO, add CRUD Page

    }
}
