namespace Blightto_Discord_RPC
{
    using NetDiscordRpc;
    using NetDiscordRpc.RPC;
    using System.IO;
    public partial class Form1 : Form
    {
        private string klasor = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @".Blightto");
        private string dosya = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @".Blightto\userinfo.blight");

        public Form1()
        {
            InitializeComponent();

        }

        //Uygulama açýldýðýnda yapýlmasý gereken iþlemler

        private void Form1_Load(object sender, EventArgs e)
        {
            bool klasorVarMý = Directory.Exists(klasor);
            if (!klasorVarMý) Directory.CreateDirectory(klasor);
            bool dosyaVarMý = File.Exists(dosya);
            if (!dosyaVarMý)
            {
                FileStream fs = File.Create(dosya);
                fs.Close();
            }

            StreamReader inputfile = new StreamReader(dosya);
            textBox_ID.Text = inputfile.ReadLine();
            textBox_Bresim.Text = inputfile.ReadLine();
            textBox_Byazi.Text = inputfile.ReadLine();
            textBox_Kresim.Text = inputfile.ReadLine();
            textBox_Kyazi.Text = inputfile.ReadLine();
            textBox_Yazi1.Text = inputfile.ReadLine();
            textBox_Yazi2.Text = inputfile.ReadLine();
            textBox_Buton1.Text = inputfile.ReadLine();
            textBox_Link1.Text = inputfile.ReadLine();
            textBox_Buton2.Text = inputfile.ReadLine();
            textBox_Link2.Text = inputfile.ReadLine();
            inputfile.Close();
        }

        //Kapanýrken Yapýlacak Þeyler

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter writer = new StreamWriter(dosya);
            writer.WriteLine(textBox_ID.Text);
            writer.WriteLine(textBox_Bresim.Text);
            writer.WriteLine(textBox_Byazi.Text);
            writer.WriteLine(textBox_Kresim.Text);
            writer.WriteLine(textBox_Kyazi.Text);
            writer.WriteLine(textBox_Yazi1.Text);
            writer.WriteLine(textBox_Yazi2.Text);
            writer.WriteLine(textBox_Buton1.Text);
            writer.WriteLine(textBox_Link1.Text);
            writer.WriteLine(textBox_Buton2.Text);
            writer.WriteLine(textBox_Link2.Text);
            writer.Close();
        }

        //Uygulama Fare ile Sürükleme

        private bool mouseDown;
        private Point lastLocation;
        public static DiscordRPC? DiscordRpc;

        private void panel_top_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel_top_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel_top_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        //Kapatma Butonu

        private void panel_close_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        //Presence Ayarlama

        private void panel_launch_MouseClick(object sender, MouseEventArgs e)
        {

            DiscordRpc = new DiscordRPC(textBox_ID.Text);
            
            DiscordRpc.Initialize();

            DiscordRpc.SetPresence(new RichPresence()
            {
                Details = textBox_Yazi1.Text,
                State = textBox_Yazi2.Text,

                Assets = new Assets()
                {
                    LargeImageKey = textBox_Bresim.Text,
                    LargeImageText = textBox_Byazi.Text,
                    SmallImageKey = textBox_Kresim.Text,
                    SmallImageText = textBox_Kyazi.Text
                },

                Buttons = new Button[]
                {
                new() { Label = textBox_Buton1.Text, Url = textBox_Link1.Text },
                new() { Label = textBox_Buton2.Text, Url = textBox_Link2.Text }
                }
            });

            DiscordRpc.Invoke();

        }

        //Presence Kaldýrma

        private void panel_stop_MouseClick(object sender, MouseEventArgs e)
        {
            DiscordRpc.ClearPresence();
        }

    }
}