using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ChatterBotAPI;
using BearDuino;
using TweetClean;

namespace BearDuino
{
    public partial class ChatForm : Form
    {
        private ChatterBotFactory factory;
        private ChatterBot bot1;
        private ChatterBotSession bot1session;

        
        public ChatForm()
        {
            InitializeComponent();
            factory = new ChatterBotFactory();
            //God
            //bot1 = factory.Create(ChatterBotType.PANDORABOTS, "923c98f3de35606b");
            //Chompsky
            //bot1 = factory.Create(ChatterBotType.PANDORABOTS, "b0dafd24ee35a477");
            //Bearbot
            //bot1 = factory.Create(ChatterBotType.PANDORABOTS, "d5a9d6d49e35633f");
            ChatterBot bot1 = factory.Create(ChatterBotType.CLEVERBOT);
            bot1session = bot1.CreateSession();


        }

        private void chatForm_Load(object sender, EventArgs e)
        {

        }

        private void sendBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            sendText();
        }
        private void sendText()
        {
            if (entryBox.Text != "")
            {
                String inputText = entryBox.Text;
                BearDuino.Bear.CloseEyes(true);
                sendButton.Text = "Thinking";
                entryBox.Text = "";
                entryBox.Enabled = false;
                sendButton.Enabled = false;
                entryBox.Update();
                messageLogBox.AppendText("YOU: " + inputText + "\n");
                String outputText = HtmlRemoval.StripTagsCharArray(bot1session.Think(inputText));
                messageLogBox.AppendText("BOT: " + outputText + "\n");
                
                var text = outputText;
                text = text.TedClean();
                BearDuino.Bear.CloseEyes(false);
                Thread.Sleep(300);
                BearDuino.Bear.Speak(text);
                entryBox.Enabled = true;
                sendButton.Enabled = true;
                sendButton.Text = "Send";
                this.ActiveControl = entryBox;

            }
        }
    }
}
