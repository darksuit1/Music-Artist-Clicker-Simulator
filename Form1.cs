﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Music_Artist
{
    public partial class Form1 : Form
    {
        private int buttonCount = 0;
        private int score = 0;
        private int tracksReleased = 0;
        private string currentLabel = "нет лейбла";
        private int remainingTime = 60;
        private int dollars = 0;

        public class Instrument // Создаем класс Музыкальных инструментов
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public bool Bought { get; set; }
            public int Points { get; set; }

            public Instrument(string name, int price, bool bought, int points)
            {
                Name = name;
                Price = price;
                Bought = bought;
                Points = points;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private List<Instrument> instruments = new List<Instrument>() // Создаем лист инструментов
        {
            new Instrument("Гитара", 1, false, 4), // Это сами инструменты, последнее число отвечает за то сколько очков будет давать за выпущенный трек
            new Instrument("Барабаны", 2, false, 1),
            new Instrument("Клавишные", 3, false, 2)
        };

        public Form1()
        {
            InitializeComponent();
            usinglabel.Text = $"Лейбл: {currentLabel}";
            listBox1.Items.AddRange(instruments.ToArray()); // Добавляем инструменты в листбокс(Магазин)
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            releasetrack.Enabled = false;
        }

        private void clicker_Click(object sender, EventArgs e)
        {
            guna2ProgressBar1.Value = buttonCount++; // Каждое нажатие кнопки заполняет прогрессбар на 1 процент
            label1.Text = $"Очки: {score}"; // Выводит кол-во очков
            if (buttonCount == 50) // После 50 кликов становится доступна кнопка для выпуска треков
            {
                releasetrack.Enabled = true;
            }
        }

        private void releasetrack_Click(object sender, EventArgs e) // Кнопка которая выпускает трек
        {
            tracksReleased++;


            int bonus = 0;
            foreach (Instrument instrument in instruments)
            {
                if (instrument.Bought)
                {
                    bonus += instrument.Points; // Если у игрока есть инструменты в инвентаре то ему за музыкальные инструменты начисляются очки при выпуске треков
                }
            }

            if (currentLabel != "нет лейбла") // Если нет лейбла то игроку не будет начисляться +10 очков
            {
                score += 20 +bonus;
                MessageBox.Show($"Трек выпущен! Добавлено 20 очков (может включать бонусы).");
            }
            else
            {
                score += 10 + bonus;
                MessageBox.Show($"Трек выпущен! Добавлено 10 очков.(может включать бонусы)"); // тут мы по стандарту начисляем 10 очков игроку за каждый выпущенный трек
            }
            guna2ProgressBar1.Value = 0;
            buttonCount = 0;
            releasetrack.Enabled = false;
            label1.Text = $"Очки: {score}"; // Тут выводим количество очков
            label2.Text = $"Выпущено треков: {tracksReleased}"; // Тут выводим количество выпущенных треков
        }

        private void timer1_Tick(object sender, EventArgs e) // Тут мы запускаем таймер если игрок вступил в лейбл
        {
            remainingTime--;
            rmtime.Text = $"Оставшееся время: {remainingTime} сек";
            if (remainingTime == 0) // останавливаем таймер если время в музыкальном лейбле истекло
            {
                timer1.Stop();
                currentLabel = "нет лейбла";
                usinglabel.Text = $"Лейбл: {currentLabel}";
                MessageBox.Show("Время в музыкальном лейбле истекло. Вы снова свободны!",
                    "Конец вступления");
            }
        }

        private void findlabel_Click(object sender, EventArgs e) // Кнопка открытия формы для поиска лейбла
        {
            Form2 form2 = new Form2();
            var result = form2.ShowDialog();
            if (result == DialogResult.OK)
            {
                currentLabel = form2.listBox1.SelectedItem.ToString();
                usinglabel.Text = $"Лейбл: {currentLabel}";
                remainingTime = 60;
                timer1.Start();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e) 
        {
            if (int.TryParse(guna2TextBox1.Text, out int dollars)) // Конвертируем очки в деньги
            {
                int points = dollars * 100;
                if (score >= points)
                {
                    score -= points;
                    label1.Text = $"Очки: {score}";
                    this.dollars += dollars;
                    double converted = (double)this.dollars;
                    money.Text = $"Доллары: {converted.ToString("C2")}";
                }
                else
                {
                    MessageBox.Show($"Недостаточно очков! Необходимо {points} очков для конвертации {dollars} долларов.", // если у игрока меньше 100 очков то ему не начислится доллар (100 очков = 1$)
                        "Неудача");
                }
            }
            else
            {
                MessageBox.Show("Введите правильное количество долларов.", "Ошибка!"); // Если игрок ввел недопустимое количество
            }
        }

        private void money_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e) // Кнопка для покупки музыкальных инструментов
        {
            if (listBox1.SelectedItem != null)
            {
                Instrument instrument = listBox1.SelectedItem as Instrument;
                if (!instrument.Bought)
                {
                    if (dollars >= instrument.Price)
                    {
                        dollars -= instrument.Price; // Забираем у игрока стоимость инструмента
                        money.Text = $"Доллары: {dollars.ToString("C2")}"; // обновляем баланс
                        instrument.Bought = true;
                        listBox1.Items[listBox1.SelectedIndex] = instrument;
                        listBox2.Items.Add(instrument.Name); // Добавляем купленный инструмент в инвентарь
                        MessageBox.Show($"Поздравляем! Вы купили инструмент: {instrument.Name}.", "Покупка завершена"); 
                    }
                    else
                    {
                        MessageBox.Show($"Недостаточно долларов! Необходимо {instrument.Price} долларов для покупки инструмента: {instrument.Name}.", // Если у игрока нету денег то ему не даст купить инструмент
                            "Неудача");
                    }
                }
                else
                {
                    MessageBox.Show($"Инструмент: {instrument.Name} уже куплен!", // Если инструмент уже куплен
                        "Информация");
                }
            }
            else
            {
                MessageBox.Show($"Выберите инструмент для покупки!",
                    "Ошибка");
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e) // Кнопка удаления инструмента из инвентаря
        {
            if (listBox2.SelectedItem != null)
            {
                string instrumentName = listBox2.SelectedItem.ToString();
                Instrument instrument = instruments.Find(i => i.Name == instrumentName);
                if (instrument != null)
                {
                    instrument.Bought = false; // Делаем инструмент неприобретенным
                    listBox1.Items[listBox1.SelectedIndex] = instrument;
                    listBox2.Items.Remove(instrumentName); // Удаляем из инвентаря
                    MessageBox.Show($"Инструмент: {instrumentName} удален из вашего инвентаря.", "Успех");
                }
            }
            else
            {
                MessageBox.Show($"Выберите инструмент для удаления!",
                    "Ошибка");
            }
        }

        private void button1_Click(object sender, EventArgs e) // Кнопка для быстрого начисления баллов чтобы проверить игру если лень зарабатывать очки. По умолчанию кнопка скрыта
        {
            score += 100;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Закрываем игру
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
