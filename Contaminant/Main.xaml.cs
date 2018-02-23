using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Contaminant
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private bool dragStarted = false;
        double y1 = 1.0;
        double x1 = 1.0;
        double y2 = 1.0;
        double x2 = 1.0;
        double y3 = 1.0;
        double x3 = 1.0;
        double y4 = 1.0;
        double x4 = 1.0;
        double[][] grid;
        public double time;
        public float del_x;
        public Main()
        {
            InitializeComponent();
            sldr.Visibility = System.Windows.Visibility.Hidden;
            grid = new double[500][];
            for (int i = 0; i < 500; i++)
            {
                grid[i] = new double[500];
            }
            del_x = 100;
        }

        private void Picture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Owner = Window.GetWindow(this);
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
            for (int i=0;i<500;i++)
            {
                for (int j = 0; j<500;j++)
                {
                    grid[i][j] = window.Grid[i][j];
                }
            }
            if (window.choise == true)
            {
                time = window.time;
                del_x = window.del_x;
                sldr.Visibility = System.Windows.Visibility.Visible;
                sldr.Maximum = time;
                sldr.Value = 0;
                ChangeImage(0);
            }
        }

        private void X_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Y_MouseDown(object sender, MouseButtonEventArgs e)
        {
            About window = new About();
            window.Owner = Window.GetWindow(this);
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        private void Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            ChangeImage(((Slider)sender).Value);
            this.dragStarted = false;
        }

        private void Slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.dragStarted = true;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!dragStarted)
                ChangeImage(e.NewValue);
        }

        private void ChangeImage(double second)
        {
            if (t1 == null) t1 = new TextBlock();
            if (t2 == null) t2 = new TextBlock();
            if (t3 == null) t3 = new TextBlock();
            if (t4 == null) t4 = new TextBlock();
            float index = Get_index(25);
            float val = (float)grid[(int)(second/0.2)][(int)index];
            if (ImageScale == null) ImageScale = new ScaleTransform();
            ImageScale.ScaleX = x1 + (val * 0.01);
            ImageScale.ScaleY = y1 + (val * 0.01);
            t1.Text = " " + string.Format("{0:F2}", val);


            index = Get_index(50);
            val = (float)grid[(int)(second / 0.2)][(int)index];
            if (im1 == null) im1 = new ScaleTransform();
            im1.ScaleX = x2 + (val * 0.01);
            im1.ScaleY = y2 + (val * 0.01);
            t2.Text = " " + string.Format("{0:F2}", val);


            index = Get_index(75);
            val = (float)grid[(int)(second / 0.2)][(int)index];
            if (im2 == null) im2 = new ScaleTransform();
            im2.ScaleX = x3 + (val * 0.01);
            im2.ScaleY = y3 + (val * 0.01);
            t3.Text = " " + string.Format("{0:F2}", val);

            index = Get_index(100);
            val = (float)grid[(int)(second / 0.2)][(int)index];
            if (im3 == null) im3 = new ScaleTransform();
            im3.ScaleX = x4 + (val * 0.01);
            im3.ScaleY = y4 + (val * 0.01);
            t4.Text = " " + string.Format("{0:F2}", val);
     
        }

        private float Get_index(int num)
        {
            float index = float.Parse(string.Format("{0:F3}", num / del_x));
            if(index < 500)
            {
                if (index % 10 > 5)
                {
                    index = (float)Math.Ceiling(index);
                }
                else
                {
                    index = (float)Math.Floor(index);
                }
            }
           else index = 499;

            return index;
        }

    }
}
