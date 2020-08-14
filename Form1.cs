using System;
using System.IO;
using System.Windows.Forms;


namespace GettingStartedWithCSharp
{

    public partial class Calculator : Form
    {
        /*static string path = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).FullName;
        static string relPath = @"\Output\Output.txt";
        string finalPath = path + relPath;
        */


        double value = 0;
        string operation = "";
        string memoryClick = "";
        bool isMemoryStored = false;
        
        double memory = 0;
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
            value = Double.Parse(result.Text);
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
                    result.Text = (value + Double.Parse(result.Text)).ToString();
                    break;
                case "-":
                    result.Text = (value - Double.Parse(result.Text)).ToString();
                    break;
                case "*":
                    result.Text = (value * Double.Parse(result.Text)).ToString();
                    break;
                case "/":
                    result.Text = (value / Double.Parse(result.Text)).ToString();
                    break;
                case "sqrt":
                    result.Text = (Math.Sqrt(value)).ToString();
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
                switch (memoryClick)
                {
                    case "MC":
                        result.Text = "Error! Memory is Empty";
                        break;
                    case "MR":
                        result.Text = "Error! Memory is Empty";
                        break;
                    case "MS":
                        memory = Double.Parse(result.Text);
                        isMemoryStored = true;
                        break;
                    case "M+":
                        result.Text = "Error! Memory is Empty";
                        break;
                    case "M-":
                        result.Text = "Error! Memory is Empty";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (memoryClick)
                {
                    case "MC":
                        isMemoryStored = false;
                        break;
                    case "MR":
                        result.Text = memory.ToString();
                        break;
                    case "MS":
                        memory = Double.Parse(result.Text);
                        isMemoryStored = true;
                        break;
                    case "M+":
                        memory = (Double.Parse(result.Text) + memory);
                        result.Text = memory.ToString();
                        break;
                    case "M-":
                        memory = (Double.Parse(result.Text) - memory);
                        result.Text = memory.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
