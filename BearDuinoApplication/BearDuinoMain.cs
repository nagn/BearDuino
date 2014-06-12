using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;

namespace BearDuino
{

    public partial class BearDuinoMain : Form
    {
        public const int ALONE = 0;
        public const int WITH_PERSON = 1;
        public const int LOVE_MODE = 2;
        private int _eyesClosed;
        private int _eyesOpened;
        private int _mouthOpened;
        private int _baudRate;
        private int _bearState;
        private int nearestInches;
        private Random randomBlink;
        private System.Windows.Forms.Timer rangeFinderTimer, blinkTimer;
        private ReadOnlyCollection<InstalledVoice> _voiceList;
        private static readonly object SyncRoot = new Object();
        public Boolean inLove;
        public BearDuinoMain()
        {
            InitializeComponent();

            //get current saved application settings for specific bear head positions
            _eyesClosed = eyesClosed.Value = Properties.Settings.Default.EyesClosed;
            _eyesOpened = eyesOpened.Value = Properties.Settings.Default.EyesOpened;
            _mouthOpened = mouthOpened.Value = Properties.Settings.Default.MouthOpened;
            _baudRate = Properties.Settings.Default.BuadRate;

            //Bear State, whether or not the bear is with someone or not
            _bearState = ALONE;

            //Nearest Object distance
            nearestInches = 0;

            //Blink timer
            blinkTimer = new System.Windows.Forms.Timer();
            blinkTimer.Tick += new EventHandler(updateBlink); // Everytime timer ticks, timer_Tick will be called
            blinkTimer.Interval = (10000);                       // every 10-20 seconds undergo blink routine
            blinkTimer.Start();
            blinkTimer.Start();

            randomBlink = new Random();
            //Periodically update nearestInches value
            rangeFinderTimer = new System.Windows.Forms.Timer();
            rangeFinderTimer.Tick += new EventHandler(updateDistance); // Everytime timer ticks, timer_Tick will be called
            rangeFinderTimer.Interval = (1000) * (1);              // Timer will tick every 10-20 seconds
            rangeFinderTimer.Enabled = true;                       // Enable the timer
            rangeFinderTimer.Start();                              // Start the timer

            eyesClosedValue.Text = _eyesClosed.ToString(CultureInfo.InvariantCulture);
            eyesOpenValue.Text = _eyesOpened.ToString(CultureInfo.InvariantCulture);
            mouthOpenedValue.Text = _mouthOpened.ToString(CultureInfo.InvariantCulture);

            //save settings to bear object
            BearDuino.Bear.SetBearConstants(_eyesClosed, _eyesOpened, _mouthOpened);

            inLove = false;
        }

        private void BearDuinoMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BearDuino.Bear != null)
                BearDuino.Bear.Dispose();

            //save any updated bear facial positions
            Properties.Settings.Default.EyesClosed = _eyesClosed;
            Properties.Settings.Default.EyesOpened = _eyesOpened;
            Properties.Settings.Default.MouthOpened = _mouthOpened;
            Properties.Settings.Default.Save();
        }

        private void comPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comPorts.SelectedItem == null) return;
            var port = new SerialPort(comPorts.SelectedItem.ToString()) {BaudRate = _baudRate};
            BearDuino.Bear.SetPort(port);
        }

        private void voiceNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (voiceNames.SelectedItem == null) return;
            BearDuino.Bear.SetVoiceName(voiceNames.SelectedItem.ToString());
        }

        private void eyesClosed_Scroll(object sender, EventArgs e)
        {
            _eyesClosed = eyesClosed.Value;
            eyesClosedValue.Text = eyesClosed.Value.ToString(CultureInfo.InvariantCulture);
            BearDuino.Bear.SendPosition(_eyesClosed);
            BearDuino.Bear.SetBearConstants(_eyesClosed, _eyesOpened, _mouthOpened);  
        }

        private void eyesOpened_Scroll(object sender, EventArgs e)
        {
            _eyesOpened = eyesOpened.Value;
            eyesOpenValue.Text = _eyesOpened.ToString(CultureInfo.InvariantCulture);
            BearDuino.Bear.SendPosition(_eyesOpened);
            BearDuino.Bear.SetBearConstants(_eyesClosed, _eyesOpened, _mouthOpened); 
        }

        private void mouthOpened_Scroll(object sender, EventArgs e)
        {
            _mouthOpened = mouthOpened.Value;
            mouthOpenedValue.Text = _mouthOpened.ToString(CultureInfo.InvariantCulture);
            BearDuino.Bear.SendPosition(_mouthOpened);
            BearDuino.Bear.SetBearConstants(_eyesClosed, _eyesOpened, _mouthOpened); 
        }

        private void comPorts_Click(object sender, EventArgs e)
        {
            //get list of active serial com ports
            var ports = SerialPort.GetPortNames();
            comPorts.DataSource = ports;
            comPorts.SelectedIndex = -1;
        }

        private void voiceNames_Click(object sender, EventArgs e)
        {
            //get list of installed SAPI voices
            using (var speech = new SpeechSynthesizer())
            {
                _voiceList = speech.GetInstalledVoices();
                var voices = _voiceList.Select(voice => voice.VoiceInfo.Name).ToList();
                voiceNames.DataSource = voices;
                voiceNames.SelectedIndex = -1;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tedTweet_Click(object sender, EventArgs e)
        {
            var tweetSpeak = new TweetSpeak();
            tweetSpeak.Show();
        }

        private void tts_Click(object sender, EventArgs e)
        {
            var BearDuinoTts = new BearDuinoTts();
            BearDuinoTts.Show();
        }

        private void tedChat_TextChanged(object sender, EventArgs e)
        {

        }

        private void tedChat_Click(object sender, EventArgs e)
        {
            var tedChat = new ChatForm();
            tedChat.Show();
        }

        private void updateDistance(object sender, EventArgs e)
        {
            if (comPorts.SelectedIndex != -1 && checkBox1.Checked)
            {
               nearestInches =  Convert.ToInt32(BearDuino.Bear.GetDistance());
               distanceLabel.Text = nearestInches.ToString();
               if (nearestInches >= 30)
               {
                   if (!(_bearState == ALONE))
                   {
                       _bearState = ALONE;
                       //Close eyes, and disable blinking
                       BearDuino.Bear.CloseEyes(true);
                       inLove = false;
                   }
               }
               else if (nearestInches >= 3)
               {
                   if (!(_bearState == WITH_PERSON)){
                       _bearState = WITH_PERSON;
                       //Open eyes
                       BearDuino.Bear.CloseEyes(false);
                       inLove = false;
                   }
               }
               else
               {
                   if (!(_bearState == LOVE_MODE))
                   {
                       _bearState = LOVE_MODE;
                       //Open eyes
                       BearDuino.Bear.CloseEyes(false);
                       if (!inLove)
                       {
                           var emotionVisualizer = new emotionvisualizer(this);
                           emotionVisualizer.Show();
                           inLove = true;
                       }
                   }

               }
        
            }
            bearState.Text = _bearState.ToString();
        }
        private void updateBlink(object sender, EventArgs e)
        {
            if (comPorts.SelectedIndex != -1 && (_bearState == WITH_PERSON || _bearState == LOVE_MODE) && checkBox1.Checked)
            {
                BearDuino.Bear.Blink(400);
                blinkTimer.Interval = ((randomBlink.Next(1, 10) * 1000) + 10000);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BearDuinoMain_Load(object sender, EventArgs e)
        {

        }

        private void emotion_Click(object sender, EventArgs e)
        {
            inLove = true;
            var emotionVisualizer = new emotionvisualizer(this);
            emotionVisualizer.Show();
        }

        private void bearState_Click(object sender, EventArgs e)
        {

        }
       

    }
}
