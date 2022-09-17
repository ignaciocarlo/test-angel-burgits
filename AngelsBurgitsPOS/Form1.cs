namespace AngelsBurgitsPOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public double costItem()
        {
            double sum = 0;
            int i = 0;

            for (i = 0; i < (dataGridView1.Rows.Count); i++)
            {
                sum = sum + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }
            return sum;
        }

        private void AddCost()
        {
            Double tax, q;
            tax = 3.9;
            if (dataGridView1.Rows.Count > 0)
            {
                lblTax.Text = String.Format("{0:c}", (((costItem() * tax) / 100)));
                lblSubTotal.Text = String.Format("{0:c}", (costItem()));
                q = ((costItem() * tax) / 100);
                lblAmount.Text = String.Format("{0,c}", (costItem() + q));
                lblBarCode.Text = Convert.ToString(q + costItem());
            }
        }

        private void Change()
        {
            Double tax, q, c;
            tax = 3.9;
            if (dataGridView1.Rows.Count > 0)
            {
                q = ((costItem() * tax) / 100) + costItem();
                c = Convert.ToInt32(lblCost.Text);
                lblChange.Text = String.Format("{0,c}", c - q);

            }
        }

        Bitmap bitmap;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                lblBarCode.Text = "";
                lblCost.Text = "0";
                lblChange.Text = "";
                lblSubTotal.Text = "";
                lblTax.Text = "";
                lblAmount.Text = "";
                dataGridView1.Rows.Clear();
                cbMOP.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void NumbersOnly(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (lblCost.Text == "0")
            {
                lblCost.Text = "";
                lblCost.Text = b.Text;
            }
            else if (b.Text == ".")
            {
                if (!lblCost.Text.Contains("."))
                {
                    lblCost.Text = lblCost.Text + b.Text;
                }
            }
            else
                lblCost.Text = lblCost.Text + b.Text;
        }
        private void btnC_Click(object sender, EventArgs e)
        {
            lblCost.Text = "0";
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (cbMOP.Text == "Cash")
            {
                Change();
            }
            else
            {
                lblChange.Text = "";
                lblCost.Text = "";
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
            AddCost();
            if (cbMOP.Text == "Cash")
            {
                Change();
            }
            else
            {
                lblChange.Text = "";
                lblCost.Text = "";
            }
        }

        private void btnRegularBurgits_Click(object sender, EventArgs e)
        {
            Double costItem = 55;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "55"))
                {
                    row.Cells[1].Value = Double.Parse((String)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((String)row.Cells[1].Value) * costItem;
                }
            }
            dataGridView1.Rows.Add("Regular Angel Burgits", "1", costItem);
            AddCost();

        }
    }
}

