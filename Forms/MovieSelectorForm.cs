using DevExpress.XtraEditors;
using KeyAuth;
using MovieSelector.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings; 

namespace MovieSelector
{
    public partial class MovieSelectorForm : DevExpress.XtraEditors.XtraForm
    {
        string Fmovies = "https://fmovies2.pro/search.html?keyword=";

        string Flixtor = "https://flixtor.se/show/search/";

        string FlixtorEnd = @"/from/1800/to/2099/rating/0/votes/0/language/all/type/all/genre/all/relevance/page/1";

        string M4ufree = "https://ww1.m4ufree.tv/search/";

        string Movies = global::MovieSelector.Properties.Resources.Movies;

        string version = KeyAuthApp.version;

        string appName = KeyAuthApp.name;

        string MovieFileLocation = @"C:\Users\env3h\source\repos\MovieSelector\Resources\Movies.txt";

        string[] allMovies = File.ReadAllLines(@"C:\Users\env3h\source\repos\MovieSelector\Resources\Movies.txt");

        public static api KeyAuthApp = new api
        (
            name: "Movie Selector",
            ownerid: "XdBoQgG7ja",
            secret: "14a71eaea002f0d5d965c666bc922b202d59cbafc299733cb4636ba8f9fc3a4b",
            version: "0.1.0"
        );

        static string random_string()
        {
            string str = null;

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;

        }

        public MovieSelectorForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyAuthApp.init();

            if (KeyAuthApp.response.message == "invalidver")
            {
                if (!string.IsNullOrEmpty(KeyAuthApp.app_data.downloadLink))
                {
                    DialogResult dialogResult = XtraMessageBox.Show("Yes to open file in browser\nNo to download file automatically", "Auto update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (dialogResult)
                    {
                        case DialogResult.Yes:
                            Process.Start(KeyAuthApp.app_data.downloadLink);
                            Environment.Exit(0);
                            break;
                        case DialogResult.No:
                            WebClient webClient = new WebClient();
                            string destFile = Application.ExecutablePath;

                            string rand = random_string();

                            destFile = destFile.Replace(".exe", $"-{rand}.exe");
                            webClient.DownloadFile(KeyAuthApp.app_data.downloadLink, destFile);

                            Process.Start(destFile);
                            Process.Start(new ProcessStartInfo()
                            {
                                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                CreateNoWindow = true,
                                FileName = "cmd.exe"
                            });
                            Environment.Exit(0);

                            break;
                        default:
                            XtraMessageBox.Show("Invalid option");
                            Environment.Exit(0);
                            break;
                    }
                }
                XtraMessageBox.Show(appName + " version " + version + " is out of date.\nFurthermore, the download link online isn't set. You will need to manually download it.", appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }

            foreach (var line in allMovies)
            {
                string[] tokens = line.Split('\n');
                movieListBox.Items.Add(tokens[0]);
            }

            versionCaption.Caption = "Version: " + version;

            serverSelectioBox.EditValue = "Fmovies";

            livewatchToggle.Checked = true;
        }

        private void getRandomMovie()
        {
            int lineCount = File.ReadAllLines(MovieFileLocation).Length;
            Random rnd = new Random();
            int randomLineNum = rnd.Next(lineCount);
            int indicator = 0;

            using (var reader = File.OpenText(MovieFileLocation))
            {
                while (reader.ReadLine() != null)
                {
                    if (indicator == randomLineNum)
                    {
                        randomSelectionText.Text = reader.ReadLine();
                        break;
                    }
                    indicator++;
                }
            }
        }

        private void pickRandomMovieBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (randomSelectionText.Text == "")
            {
                getRandomMovie();
            }
            else
            {
                randomSelectionText.Text = "";
                getRandomMovie();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (serverSelectioBox.Text == "Fmovies")
            {
                if (randomSelectionText.Text == "" || randomSelectionText.Text == "You can type your own movie too!")
                {
                    XtraMessageBoxArgs args = new XtraMessageBoxArgs()
                    {
                        Caption = "Movie Selector",
                        Text = $"You can't watch a movie if you do not select one :P.\n\nWould you like to select a random movie?",
                        Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No },
                        Icon = new Icon(@"C:\Users\env3h\Desktop\Programming Projects\Utilities\Picture Icons\ICO\film_projector_cinema_icon-icons.com_66132.ico"),

                        AutoCloseOptions = new AutoCloseOptions()
                        {
                            // Sets the delay before the message box automatically closes.
                            Delay = 10000,
                            //Displays the timer on the default button.
                            ShowTimerOnDefaultButton = true,
                        }
                    };
                    if (XtraMessageBox.Show(args) == DialogResult.Yes)
                    {
                        randomSelectionText.Text = "";
                        getRandomMovie();
                    }
                    else
                    {
                        randomSelectionText.Text = "";

                    }

                }
                else
                {
                    System.Diagnostics.Process.Start(Fmovies + randomSelectionText.Text.Replace(" ", "+").Replace(":", ""));
                }
            }
            if (serverSelectioBox.Text == "M4ufree")
            {
                if (randomSelectionText.Text == "" || randomSelectionText.Text == "You can type your own movie too!")
                {
                    XtraMessageBoxArgs args = new XtraMessageBoxArgs()
                    {
                        Caption = "Movie Selector",
                        Text = $"You can't watch a movie if you do not select one :P.\n\nWould you like to select a random movie?",
                        Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No },
                        Icon = new Icon(@"C:\Users\env3h\Desktop\Programming Projects\Utilities\Picture Icons\ICO\film_projector_cinema_icon-icons.com_66132.ico"),

                        AutoCloseOptions = new AutoCloseOptions()
                        {
                            // Sets the delay before the message box automatically closes.
                            Delay = 10000,
                            //Displays the timer on the default button.
                            ShowTimerOnDefaultButton = true,
                        }
                    };
                    if (XtraMessageBox.Show(args) == DialogResult.Yes)
                    {
                        randomSelectionText.Text = "";
                        getRandomMovie();
                    }
                    else
                    {
                        randomSelectionText.Text = "";

                    }

                }
                else
                {
                    System.Diagnostics.Process.Start(M4ufree + randomSelectionText.Text.Replace(" ", "-").Replace(":", "") + ".html");
                }
            }
            if (serverSelectioBox.Text == "Flixtor")
            {
                if (randomSelectionText.Text == "" || randomSelectionText.Text == "You can type your own movie too!")
                {
                    XtraMessageBoxArgs args = new XtraMessageBoxArgs()
                    {
                        Caption = "Movie Selector",
                        Text = $"You can't watch a movie if you do not select one :P.\n\nWould you like to select a random movie?",
                        Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No },
                        Icon = new Icon(@"C:\Users\env3h\Desktop\Programming Projects\Utilities\Picture Icons\ICO\film_projector_cinema_icon-icons.com_66132.ico"),

                        AutoCloseOptions = new AutoCloseOptions()
                        {
                            // Sets the delay before the message box automatically closes.
                            Delay = 10000,
                            //Displays the timer on the default button.
                            ShowTimerOnDefaultButton = true,
                        }
                    };
                    if (XtraMessageBox.Show(args) == DialogResult.Yes)
                    {
                        randomSelectionText.Text = "";
                        getRandomMovie();
                    }
                    else
                    {
                        randomSelectionText.Text = "";

                    }

                }
                else
                {
                    System.Diagnostics.Process.Start(Flixtor + randomSelectionText.Text.Replace(" ", "%20").Replace(":", "") + FlixtorEnd);
                }
            }
        }

        private void watchSelectedMovie_Click(object sender, EventArgs e)
        {
            
        }

        private void addCustomMovie_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            movieListBox.Items.Add(customMovieText.EditValue);
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                TextWriter writer = new StreamWriter(folderBrowserDialog1.SelectedPath + "Movies.txt");
                writer.WriteLine(customMovieText.EditValue);
                writer.Close();
                XtraMessageBox.Show(customMovieText.EditValue + "has been saved in the list.\n\nIf you run into any problems, you can always add your movie manually!", "Movie Selector", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void comboMovieBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("https://fmovies2.pro/Fmovies");
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("https://flixtor.se/");
        }

        private void toM4uFree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ww1.m4ufree.tv/");
        }

        private void clearComboBox_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            movieListBox.Items.Clear();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Restart();
        }

        private void quitApplication_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Environment.Exit(0);
        }

        private void helpMeBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs()
            {
                Caption = "Movie Selector Help",
                Text = $"Hello!\n" + $"File Tab\n" + $"- Download Movie List: This downloads the 'Movies.txt'.\n" + $"- Restart Application: This restarts the application :p.\n" + $"- Exit: this closes the application.\n\n" + $"Settings Tab\n" + $"- Help: Hello! You are here now!\n" + $"- Manual Update: This lets you manually update the app!\n" + $"- Theme: This allows you to change the theme and even the skin pallet of the tool!\n" +
                $"" ,
                Buttons = new DialogResult[] { DialogResult.OK },
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

        private void groupControl4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void serverSelectioBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serverSelectioBox.Text == "Flixtor")
            {
                XtraMessageBox.Show("Flixtor is the safest and most updated site, with that being said all shows and movies are free to watch until they age 6 months or older.\n\nYou may purchase a VIP pass to get full access to all shows and movies no mater the age!", "Server Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void imOpenSource_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/FrankAustin808/MovieSelector");
        }

        private void manualUpdateMe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.mediafire.com/file/4q607dh8st69135/MovieSelector.exe/file/d");
        }

        private void openMovieListBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void downloadMovieList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // COME BACK LATER
        }

        private void liveWatchBtnTimer_Tick(object sender, EventArgs e)
        {
            watchRandomMovieBtn.Text = "Watch '" + randomSelectionText.Text + "' On " + serverSelectioBox.Text;
        }

        private void movieListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            randomSelectionText.Text = movieListBox.SelectedItem.ToString();
        }

        private void barToggleSwitchItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void livewatchToggle_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (livewatchToggle.Checked)
            {
                liveWatchBtnTimer.Start();
            }
            else
            {
                liveWatchBtnTimer.Stop();
                watchRandomMovieBtn.Text = "Watch";
            }
        }
    }
}
