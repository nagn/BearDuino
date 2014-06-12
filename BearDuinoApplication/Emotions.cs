using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using BearDuino;
using TweetClean;
using System.Threading;
namespace BearDuino
{
    public partial class emotionvisualizer : Form
    {
        private BearDuinoMain bear;
        private List<Dictionary<string, string>> feelings;
        private System.Windows.Forms.Timer emotionTimer;
        private System.Windows.Forms.Timer speakTimer;
        private String currentText;
        public emotionvisualizer(BearDuinoMain aBear)
        {
            InitializeComponent();
            WebClient client = new WebClient();
            emotionPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            feelings = new List<Dictionary<string, string>>();

            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2009&feeling=lovely&extraimages=100")));
            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2010&feeling=lovely&extraimages=100")));
            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2011&feeling=lovely&extraimages=100")));


            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2009&feeling=loved&extraimages=100")));
            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2010&feeling=loved&extraimages=100")));
            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2011&feeling=loved&extraimages=100")));

            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2009&feeling=loving&extraimages=100")));
            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2010&feeling=loving&extraimages=100")));
            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2011&feeling=loving&extraimages=100")));

            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2009&feeling=beloved&extraimages=67")));
            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2010&feeling=beloved&extraimages=67")));
            appendEmotions(client.OpenRead(createQuery("&limit=100&postyear=2011&feeling=beloved&extraimages=67")));

            // Create a timer with a two second interval.
            emotionTimer = new System.Windows.Forms.Timer();
            // Hook up the Elapsed event for the timer. 
            emotionTimer.Tick += new EventHandler(NewEmotionEvent);
            emotionTimer.Start();
            bear = aBear;

            speakTimer = new System.Windows.Forms.Timer();
            speakTimer.Tick += new EventHandler(SpeakEmotionEvent);
        }
        private String getImage(String postDate, String imageID, String imageSize)
        {
            String imgPath = "http://wefeelfine.org/data/images/";
	        imgPath += postDate.Replace('-','/');
	        imgPath += "/" + imageID;
	        if (imageSize == "thumb"){
                imgPath += "_thumb.jpg";
            }else{

                imgPath += "_full.jpg";
            }
            return imgPath;

        }
        private void emotionvisualizer_Load(object sender, EventArgs e)
        {

        }

        private void emotionPictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawStringRectangleF(e);
        }

        private void feelingText_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void NewEmotionEvent(Object source, EventArgs e)
        {
            emotionTimer.Stop();
            //select random from the list
            Random random = new Random();
            int index = random.Next(feelings.Count);
            Dictionary<string, string> feeling = feelings[index];

            
            if (feeling.ContainsKey("imageid")) {
                String imgURL = getImage(feeling["postdate"], feeling["imageid"], "full");
                try
                {
                    emotionPictureBox.Load(imgURL);
                }
                catch (Exception error)
                {

                }
                
            }
            if (!bear.inLove)
            {
                emotionTimer.Stop();
                this.Close();
                return;
            }
            emotionLabel.Text = feeling["sentence"] + " @" + index;
            emotionTypeLabel.Text = feeling["feeling"];
            
            setBackgroundToFeeling(feeling["feeling"]);

            currentText = feeling["sentence"];
            speakTimer.Interval = 200;
            speakTimer.Start();
        }
        private void SpeakEmotionEvent(Object source, EventArgs e)
        {
            String text = currentText.TedClean();
            BearDuino.Bear.CloseEyes(false);
            BearDuino.Bear.Speak(text);

            emotionTimer.Interval = 5000;
            emotionTimer.Start();
            speakTimer.Stop();
        }
        public void emotionLabel_Paint(object sender, PaintEventArgs e)
        {
           
            var control = sender as Control;
            if (control == null)
                return;

            control.Text = string.Empty;    //delete old stuff
            var rectangle = control.ClientRectangle;

            using (Font f = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, FontStyle.Bold))
            {
                SizeF size;
                using (Font f2 = AppropriateFont(e.Graphics, 5, 50, rectangle.Size, emotionLabel.Text, f, out size))
                {
                    PointF p = new PointF((rectangle.Width - size.Width) / 2, (rectangle.Height - size.Height) / 2);
                    e.Graphics.DrawString(emotionLabel.Text, f2, Brushes.Black, p);
                }
            }
        }
        public void DrawStringRectangleF(PaintEventArgs e)
        {

            // Create string to draw.
            String drawString = "Sample Text";
            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            // Create rectangle for drawing.
            float x = 20.0F;
            float y = 50.0F;
            float width = 200.0F;
            float height = 50.0F;
            RectangleF drawRect = new RectangleF(x, y, width, height);
            // Draw rectangle to screen.
            Pen blackPen = new Pen(Color.Black);
            e.Graphics.DrawRectangle(blackPen, x, y, width, height);
            // Draw string to screen.
            //e.Graphics.DrawString(drawString, drawFont, drawBrush, drawRect);
        }
        private void appendEmotions(Stream data)
        {
            XmlTextReader xmlReader = new XmlTextReader(data);
            try
            {
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            if (xmlReader.HasAttributes)
                            {
                                Dictionary<string, string> dict = new Dictionary<string, string>();

                                //Console.WriteLine("Attributes of <" + xmlReader.Name + ">");
                                while (xmlReader.MoveToNextAttribute())
                                {
                                    dict.Add(xmlReader.Name, xmlReader.Value);
                                }
                                // Move the reader back to the element node.
                                if (dict.ContainsKey("imageid"))
                                {
                                    Boolean hasSentence = false;
                                    foreach (Dictionary<string, string> feeling in feelings)
                                    {
                                        if (feeling["sentence"].Equals(dict["sentence"]))
                                        {
                                            hasSentence = true;
                                        }
                                    }
                                    if (!hasSentence)
                                    {
                                        feelings.Add(dict);
                                    }


                                }
                                xmlReader.MoveToElement();
                            }
                            break;
                    }
                }
            }
            catch (XmlException error)
            {
                Console.WriteLine(error.Message);
                Console.WriteLine("Exception object Line, pos: (" + error.LineNumber + "," + error.LinePosition + ")");
                Console.WriteLine("XmlReader Line, pos: (" + xmlReader.LineNumber + "," + xmlReader.LinePosition + ")");
            }
            data.Close();
            xmlReader.Close();

        }
        private void setBackgroundToFeeling(String feeling)
        {
            switch (feeling)
            {
                case "lovely":
                    this.BackColor = System.Drawing.ColorTranslator.FromHtml("#004E6F");
                    break;
                case "loved":
                    this.BackColor = System.Drawing.ColorTranslator.FromHtml("#F30172");
                    break;
                case "loving":
                    this.BackColor = System.Drawing.ColorTranslator.FromHtml("#C80027");
                    break;
                case "beloved":
                    this.BackColor = System.Drawing.ColorTranslator.FromHtml("#40B6B8");
                    break;
                default:
                    break;
                    

            }
        }
        public static Font AppropriateFont(Graphics g, float minFontSize, float maxFontSize, Size layoutSize, string s, Font f, out SizeF extent)
        {
            if (maxFontSize == minFontSize)
                f = new Font(f.FontFamily, minFontSize, f.Style);

            extent = g.MeasureString(s, f);

            if (maxFontSize <= minFontSize)
                return f;

            float hRatio = layoutSize.Height / extent.Height;
            float wRatio = layoutSize.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = f.Size * ratio;

            if (newSize < minFontSize)
                newSize = minFontSize;
            else if (newSize > maxFontSize)
                newSize = maxFontSize;

            f = new Font(f.FontFamily, newSize, f.Style);
            extent = g.MeasureString(s, f);

            return f;
        }
        public void emotionvisualizer_FormClosed(Object sender, FormClosedEventArgs e)
        {
            bear.inLove = false;
        }
        private String createQuery(String returnArgs)
        {
            return "http://api.wefeelfine.org:8080/ShowFeelings?display=xml&returnfields=imageid,feeling,sentence,posttime,postdate,posturl,gender,born,country,state,city,lat,lon,conditions" + returnArgs;
        }
        private void emotionvisualizer_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;

            // Ensure the Form remains square (Height = Width). 
            if (control.Size.Height != control.Size.Width)
            {
                control.Size = new Size(control.Size.Width, control.Size.Width);
            }
        }
    }
}
