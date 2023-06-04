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
        private string[] labels = { "Universal Music Group", "Sony Music", "Warner Records", "Effective Records", "RNB Club", "AWGE Records", "Polyroom Entertaiment" }; // Создаем список лейблов
        public Form2()
        {
            InitializeComponent();
            label1.Text = "Выберите музыкальный лейбл:";
            listBox1.Items.AddRange(labels); // Загружаем лейблы из списка в листбокс
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void gotolabel_Click(object sender, EventArgs e)
        {
            if (new Random().Next(1, 5) == 4) // Создаем вступление в лейбл с шансом 25%
            {
                MessageBox.Show("Поздравляю! Вы вступили в музыкальный лейбл " + listBox1.SelectedItem.ToString() + ".", "Успех!");
                this.DialogResult = DialogResult.OK; // Если игрок вступает в лейбл то диалог закрывается
                this.Close();
            }
            else
            {
                MessageBox.Show("К сожалению, вы не были приняты в музыкальный лейбл.", "Неудача!"); // Если игрок не вступает в лейбл то ему дается шанс найти вступить в лейбл еще раз
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
