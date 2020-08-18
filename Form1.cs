using System;
using System.IO;
using System.Windows.Forms;


namespace GettingStartedWithCSharp
{

    public partial class Calculator : Form
    {
        decimal value = 0;
        string operation = "";
        string memoryClick = "";
        bool isMemoryStored = false;

        decimal memory;
        bool operation_pressed = false;
        bool result_obtained = false;

        public Calculator()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (result.Text == "0" || (operation_pressed))
                result.Clear();
            if (result_obtained)
            {
                result.Text = "";
            }
            result_obtained = false;
            operation_pressed = false;
            Button b = (Button)sender;
            result.Text += b.Text;

        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            result.Text = "0";
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            operation = b.Text;
            try
            {
                value = (decimal)(Double.Parse(result.Text));
            }
            catch (System.FormatException) {
                MessageBox.Show("Introduceti o valoare valida");
                result.Text = "0";
                equation.Text = "0";
            }
            operation_pressed = true;
            equation.Text = value + " " + operation;
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            operation_pressed = false;
            equation.Text = "";
            switch (operation)
            {
                case "+":
                    result.Text = (value + (decimal)Double.Parse(result.Text)).ToString("0.000");
                    break;
                case "-":
                    result.Text = (value - (decimal)Double.Parse(result.Text)).ToString("0.000");
                    break;
                case "*":
                    result.Text = (value * (decimal)Double.Parse(result.Text)).ToString("0.000");
                    break;
                case "/":
                    try
                    {
                        result.Text = (value / (decimal)Double.Parse(result.Text)).ToString("0.000");
                    }
                    catch (DivideByZeroException)
                    {
                        MessageBox.Show("Impartirea la 0 nu este o operatie valida");
                        result.Text = "operatie nevalida";
                    }
                    break;
                case "sqrt":
                    if (value < 0)
                    {
                        try { throw new Exception("Radacina patrata a numerelor negative nu este posibila"); }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Radacina patrata a numerelor negative nu este posibila");
                        }
                        result.Text = "operatie nevalida";
                    }
                    else
                    {
                        result.Text = (Math.Sqrt((double)value)).ToString("0.000");
                    }
                    break;
                default:
                    break;
            }
            result_obtained = true;
            Istoric.Text += (result.Text + ", ");

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            result.Clear();
            Istoric.Clear();
            value = 0;
        }

        private void SalveazaIstoricul_click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text File|*";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = sfd.FileName;
                using (var sw = new StreamWriter(File.Create(path)))
                {
                    sw.Write(Istoric.Text.Remove((Istoric.Text.Length - 2), 1));
                    sw.Dispose();
                }
            }
        }

        private void Memory_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            memoryClick = b.Text;

            if (!isMemoryStored)
            {
                MC.Enabled = false;
                MR.Enabled = false;
                M.Enabled = false;
            }

            switch (memoryClick)
            {
                case "MC":
                    string mesaj = "Do you want to clear the memory?";
                    string titlu = "Memory Clear";
                    MessageBoxButtons butoane = MessageBoxButtons.YesNo;
                    DialogResult rezultat = MessageBox.Show(mesaj, titlu, butoane);
                    if (rezultat == DialogResult.Yes)
                    {
                        isMemoryStored = false;
                        MC.Enabled = false;
                        MR.Enabled = false;
                        M.Enabled = false;
                    }
                    break;
                case "MR":
                    result.Text = memory.ToString();
                    break;
                case "MS":
                    memory = (decimal)Double.Parse(result.Text);
                    isMemoryStored = true;
                    MC.Enabled = true;
                    MR.Enabled = true;
                    M.Enabled = true;
                    break;
                case "M+":
                    memory += (decimal)(Double.Parse(result.Text));
                    break;
                case "M-":
                    memory -= (decimal)Double.Parse(result.Text);
                    break;
                case "M":
                    MemoryShow.SetToolTip(M, memory.ToString());
                    break;
                default:
                    break;
            }
        }

    }
}
