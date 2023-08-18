using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPU_Notepad
{
    public partial class Form1 : Form
    {
        Font saveFont;
        Color saveColor;
        int number = 0;

        public Form1()
        {
            InitializeComponent();
            statusLabel.Text = $"Количество символов: 0";
            //загружаем коллекцию системных шрифтов
            var fontsCollection = new InstalledFontCollection();
            var font = fontsCollection.Families;
            foreach ( var f in font )
            {
                fontList.Items.Add( f.Name );
            }
        }
        //загрузка текста из файла
        private void loadFile()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Все файлы(*.*)|*.*|Текстовый файл(*.txt)|*.txt";
            open.FilterIndex = 2;
            if(open.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = File.OpenText(open.FileName))
                {
                    richTextBox1.Text = reader.ReadToEnd();
                }
            }
        }

        //сохранение текста в файл
        private void saveFile()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Все файлы(*.*)|*.*|Текстовый файл(*.txt)|*.txt";
            save.FilterIndex = 2;
            if(save.ShowDialog() == DialogResult.OK)
            {
                using(StreamWriter writer = new StreamWriter(save.FileName))
                {
                    writer.WriteLine(richTextBox1.Text);
                }
            }
        }
        //изменение счетчика символов в строке состояния
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            statusLabel.Text = $"Количество символов: {richTextBox1.Text.Length}";
        }
        //выравнивание по левому краю
        private void leftAlign()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }
        //выравнивание по правому краю
        private void rightAlign()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }
        //выравнивание по центру
        private void centerAlign()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }
        //выравнивание по ширине
        private void justifyAlign() 
        { 
            richTextBox1.SelectionIndent = 0; 
            richTextBox1.SelectionRightIndent = 0;
            richTextBox1.SelectionHangingIndent = 0; 
        }
        //жирное выделение
        private void boldText()
        {
            if(richTextBox1.SelectionFont.Bold != true)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold);
            }
            else
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Regular);
            }
        }
        //курсивное выделение
        private void italicText()
        {
            if (richTextBox1.SelectionFont.Italic != true)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Italic);
            }
            else
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Regular);
            }
        }
        //подчеркнутое выделение
        private void underlinedText()
        {
            if (richTextBox1.SelectionFont.Underline != true)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Underline);
            }
            else
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Regular);
            }
        }
        //уменьшение шрифта
        private void dicreaseText()
        {
            if(richTextBox1.SelectionFont.Size > 1)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.Size - 1, richTextBox1.SelectionFont.Style);
            }
        }
        //увеличение шрифта
        private void increaseText()
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.Size + 1, richTextBox1.SelectionFont.Style);
        }
        //выбор шрифта из списка
        private void fontList_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font((string)fontList.SelectedItem, richTextBox1.SelectionFont.Size, richTextBox1.SelectionFont.Style);
        }
        //формат по образцу. сохраняем шрифт и цвет текста
        private void formatPaint()
        {
            if(richTextBox1.SelectedText != "")
            {
                saveFont = richTextBox1.SelectionFont;
                saveColor = richTextBox1.SelectionColor;
            }
        }
        //меняет регистр выделенного текста. возвращает измененную строку, которую нужно передать обратно в выделенный текст
        private string upperText()
        {
            return richTextBox1.SelectedText.ToUpper();
        }

        //смена стиля и цвета шрифта при выделении
        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if(formatPaintBtn.Checked == true || formatPaintMenuItem.Checked == true)
            {
                richTextBox1.SelectionFont = saveFont;
                richTextBox1.SelectionColor = saveColor;
            }
        }
        //палитра цветов
        private void textColorBtn_Click(object sender, EventArgs e)
        {
            var colorCollection = new ColorDialog();
            if(colorCollection.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorCollection.Color;
            }
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            loadFile();
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            loadFile();
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cutBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }        

        private void copyBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void alignLeftBtn_Click(object sender, EventArgs e)
        {
            leftAlign();
        }

        private void alignCenterBtn_Click(object sender, EventArgs e)
        {
            centerAlign();
        }

        private void alignRightBtn_Click(object sender, EventArgs e)
        {
            rightAlign();
        }

        private void alignJustifyBtn_Click(object sender, EventArgs e)
        {
            justifyAlign();
        }

        private void boldBtn_Click(object sender, EventArgs e)
        {
            boldText();
        }

        private void italicBtn_Click(object sender, EventArgs e)
        {
            italicText();
        }

        private void underlinedBtn_Click(object sender, EventArgs e)
        {
            underlinedText();
        }

        private void upperBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = upperText();
        }

        private void dicreaseBtn_Click(object sender, EventArgs e)
        {
            dicreaseText();
        }

        private void increaseBtn_Click(object sender, EventArgs e)
        {
            increaseText();
        }

        private void formatPaintBtn_Click(object sender, EventArgs e)
        {
            formatPaint();
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (richTextBox1.SelectedText != "")
            {

                if (e.KeyValue == (Char)Keys.Enter)
                {
                    string text = richTextBox1.SelectedText;
                    number++;
                    richTextBox1.SelectedText = Convert.ToString(number) + ") " + text + "\n";
                }
                else if (e.KeyValue == (Char)Keys.Back)
                {
                    number-=2;
                }
            
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //if (e.KeyValue == (Char)Keys.Enter)
            //{
            //    string text = richTextBox1.SelectedText;
            //    i++;
            //    richTextBox1.SelectedText = Convert.ToString(i) + ") " + text;
            //}
        }

    }
}
