using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Contaminant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private float Lamda = 0;
        public double[][] Grid;
        public double time;
        float del_t;
        public float del_x;
        public bool choise;
        public MainWindow()
        {
            InitializeComponent();
            del_t = 100;
            del_x = 100;
            choise = false;
            Grid = new double[500][];
            for(int i = 0; i < 500; i++)
            {
                Grid[i] = new double[500];
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void texbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text.Equals("Concentration"))
                txtBox.Text = string.Empty;
        }

        private void TextBox2_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text.Equals("Velocity"))
                txtBox.Text = string.Empty;
        }

        private void TextBox3_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text.Equals("Length of pipe"))
                txtBox.Text = string.Empty;
        }

        private void TextBox4_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text.Equals("Initial pollution"))
                txtBox.Text = string.Empty;
        }

        private void sel_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || textBox4.Text.Equals("Initial pollution") || textBox5.Text.Equals("Velocity") || textBox3.Text.Equals("Amount of pollution"))
            {
                MessageBox.Show("Error! One or more fields are missing!");
            }
            else
            {
                if(float.Parse(textBox4.Text.ToString()) > 100 || float.Parse(textBox3.Text.ToString()) > 100)
                {
       
                    MessageBox.Show("Error! The pollution must be less than 100 milliliters!");
                }
                else
                {
                    if (float.Parse(textBox5.Text.ToString()) > 100)
                    {
                        MessageBox.Show("Error! The velocity must be less than 100 meters in second!");
                    }
                    else
                    {
                        float init_pol = float.Parse(textBox4.Text.ToString());  //initial pollurion
                        float vel = float.Parse(textBox5.Text.ToString()); //velocity
                        float amount_pol = float.Parse(textBox3.Text.ToString()); //amount of pollution
                        time = sldr.Value;
                        del_t = (float)(100.00 / 500.00);
                        if (vel * del_t > 0)
                        {
                            del_x = vel * del_t;
                        }
                        else del_x = del_t;
                        Lamda = (float)(vel * (del_t / del_x));
                        if (Lamda > 1)
                        {
                            MessageBox.Show("Unexpected Error!");
                        }
                        else
                        {
                            Grid_X(init_pol, del_x);
                            Grid_T(amount_pol, del_t,init_pol);
                            Fill_Grid();
                        }
                        choise = true;
                        this.Close();
                    }
                }
            }
        }


        public void Grid_X(double poll,double delta_x)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    for(int i = 0; i < 500; i++)
                    {
                        Grid[0][i] = poll;
                    }
                    break;
                case 1:
                    
                    for(int i = 0; i < 500; i++)
                    {
                        Grid[0][i] = i*delta_x + 1.0 + poll;
                    }
                    break;
                case 2:
                    for (int i = 0; i < 500; i++)
                    {
                        if (Math.Cos(System.Convert.ToDouble(i * delta_x)) < 0)
                        {
                            Grid[0][i] = 0 + poll;
                        }
                        Grid[0][i] = Math.Cos(System.Convert.ToDouble(i*delta_x)) + poll;
                    }
                    break;
                case 3:
                    for (int i = 0; i < 500; i++)
                    {
                        if (Math.Sin(System.Convert.ToDouble(i * delta_x)) < 0)
                        {
                            Grid[0][i] = 0 + poll;
                        }
                        else
                            Grid[0][i] = Math.Sin(System.Convert.ToDouble(i*delta_x)) + poll;
                    }
                    break;
                case 4:
                    for (int i = 0; i < 500; i++)
                    {
                        Grid[0][i] = Math.Sqrt(i*delta_x) + poll;
                    }
                    break;
            }
        }



        public void Grid_T(double fact_poll, float delta_t,float init_poll)
        {
            int t = 0;
            double gr_0_0 = Grid[0][0];
            for (int i = 1; i < 500; i++)
            {
                Grid[i][0] = init_poll;
            }
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    t = 0;
                    for (int i = 1; i < 500; i++)
                    {
                        t = (int)(Math.Ceiling(i / delta_t));
                        if (t < 500)
                            Grid[t][0] = Grid[t][0] + fact_poll;
                        else break;
                    }
                    break;
                case 1:
                    //t+2
                    t = 0;
                    for (int i = 0; i < 500; i++)
                    {
                        t = (int)(Math.Ceiling((i + 2) / delta_t));
                        if (t<500)
                            Grid[i][0] = Grid[t][0] + fact_poll;
                        else break;
                    }
                    break;
                case 2:
                    //t^2
                    t = 0;
                    for (int i = 0; i < 500; i++)
                    {
                        t = (int)(Math.Ceiling((i*i)/delta_t));
                        if (t < 500)
                            Grid[t][0] = Grid[t][0] + fact_poll;
                        else break;
                    }
                    break;
            }

        }

        public void Fill_Grid()
        {
            for(int i = 1; i < 500; i++)
            {
                for(int j = 1; j < 500; j++)
                {
                    Grid[i][j] = (1-Lamda)*Grid[i-1][j]+Lamda*Grid[i-1][j-1];
                }
            }

        }


    }

}
