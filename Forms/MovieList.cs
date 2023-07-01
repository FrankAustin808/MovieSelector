using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieSelector.Forms
{
    public partial class MovieList : DevExpress.XtraEditors.XtraForm
    {
        string[] allMovies = File.ReadAllLines(@"C:\Users\env3h\source\repos\MovieSelector\Resources\Movies.txt");

        public MovieList()
        {
            InitializeComponent();
        }

        private void MovieList_Load(object sender, EventArgs e)
        {
            foreach (var line in allMovies)
            {
                string[] tokens = line.Split('\n');
                movieListBox.Items.Add(tokens[0]);
            }
        }

        private void movieListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(movieListBox.SelectedItem.ToString());

            XtraMessageBoxArgs args = new XtraMessageBoxArgs()
            {
                Caption = "Movie Selector",
                Text = $"'" + movieListBox.SelectedItem.ToString() + "' has been copied to your clipbard!",
                Buttons = new DialogResult[] { DialogResult.OK},
                Icon = new Icon(@"C:\Users\env3h\Desktop\Programming Projects\Utilities\Picture Icons\ICO\film_projector_cinema_icon-icons.com_66132.ico"),

                AutoCloseOptions = new AutoCloseOptions()
                {
                    // Sets the delay before the message box automatically closes.
                    Delay = 10000,
                    //Displays the timer on the default button.
                    ShowTimerOnDefaultButton = true,
                }
            };
            if (XtraMessageBox.Show(args) == DialogResult.OK)
            {
            }
            else
            {
            }
        }

        private void movieListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
