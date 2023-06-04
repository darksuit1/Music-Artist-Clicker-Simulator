using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music_Artist
{
    public partial class Form2 : Form
    {
        private string[] labels = { "Universal Music Group", "Sony Music", "Warner Records", "Effective Records", "RNB Club", "AWGE Records", "Polyroom Entertaiment" };
        public Form2()
        {
            InitializeComponent();
            label1.Text = "Выберите музыкальный лейбл:";
            listBox1.Items.AddRange(labels);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void gotolabel_Click(object sender, EventArgs e)
        {
            if (new Random().Next(1, 5) == 4) // шанс 25%
            {
                MessageBox.Show("Поздравляю! Вы вступили в музыкальный лейбл " + listBox1.SelectedItem.ToString() + ".", "Успех!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("К сожалению, вы не были приняты в музыкальный лейбл.", "Неудача!");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
