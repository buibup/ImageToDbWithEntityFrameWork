using DoctorImages.core;
using DoctorImages.da;
using DoctorImages.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorImages
{
    public partial class Form1 : Form
    {
        ImageModel image = new ImageModel();
        List<ImageModel> imagesDirectoryList = new List<ImageModel>();
        List<ImageModel> imagesDbList = new List<ImageModel>();
        List<ImageModel> imagesDiff = new List<ImageModel>();
        ImageByte core = new ImageByte();
        ImagesDa imgDa = new ImagesDa();
        

        string imagePath = ConfigurationManager.AppSettings["path"];
        public Form1()
        {
            InitializeComponent();


        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadImageFromDirectory();
            
        }

        private void LoadImageFromDirectory()
        {
            listView1.View = View.LargeIcon;

            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(256, 256);
            imgs.ColorDepth = ColorDepth.Depth32Bit;

            String[] paths = { };
            paths = Directory.GetFiles(imagePath, "*jpg");

            try
            {
                foreach(string path in paths)
                {
                    imgs.Images.Add(Path.GetFileName(path),Image.FromFile(path));
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            listView1.LargeImageList = imgs;

            int i = 0;
            foreach(var item in imgs.Images.Keys)
            {
                listView1.Items.Add(item,i);
                i++;
            }
            
        }

        private void ImagesToDb()
        {
            using(var db = new ImageContext())
            {

                try
                {
                    foreach (var item in imagesDiff)
                    {
                        db.Images.Add(item);
                        db.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    //Log any exception here.
                }
            }
        }

        private void ImagesDirectoryToList()
        {
            imagesDirectoryList = core.GetImagesDirectoryToList(imagePath);
        }

        private void ImagesDbToList()
        {
            imagesDbList = imgDa.GetAllImage();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;

            listView1.Columns.Add("Images", 250);

            // get image from database to list
            ImagesDbToList();
            // get images from directory to list
            ImagesDirectoryToList();
            // compare images between directory and database
            imagesDiff = FileData.CompareDiffList(imagesDirectoryList, imagesDbList);
            // insert images to database
            ImagesToDb();

            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
    }
}
