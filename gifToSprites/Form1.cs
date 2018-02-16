using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using GifToSpritesClasses;

namespace gifToSprites
{
    public partial class Form1 : Form
    {
        private Image gifImg;
        private IGifConverter converter;
        private string fileName;

        public Form1() {

            InitializeComponent();

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.FileName = null;
            openFileDialog1.DefaultExt = ".gif";
            openFileDialog1.Filter = "Animation files |*.gif";
            openFileDialog1.Multiselect = false;
        }

        private void openFileButton_Click(object sender, EventArgs e) {

            if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                fileName = openFileDialog1.FileName;
                gifImg = Image.FromFile(fileName);
                pictureBox1.Image = gifImg;
                sliceButton.Enabled = true;
            }
        }



        private void sliceButton_Click(object sender, EventArgs e)
        {
            string nameWithoutExtension = PathHelper.GetFullPathWithoutExtension(fileName);

            try {
                if (radioButton1.Checked) {

                    converter = new GifToSpritesheet(gifImg);
                    ImageFileSaver.Save(converter.Convert()[0], nameWithoutExtension);
                } else {

                    converter = new GifToSprites(gifImg);
                    ImageFileSaver.SaveMultiple(converter.Convert(), nameWithoutExtension);
                }
            } catch(InvalidImageFormatException ex) {
                MessageBox.Show("Selected image either corrupt or is not of .gif format");
            }

        }
    }
}
