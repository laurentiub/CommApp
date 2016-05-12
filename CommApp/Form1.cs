using CommApp.Logic; //namespace pentru clasa Communication
using System;
using System.Windows.Forms;

namespace CommApp
{


    public partial class Form1 : Form, IView
    {
        private Communication communication;
        public Form1()
        {
            InitializeComponent();
        }

        public string GetIpServer()
        {
            return txtIpServer.Text;
        }

        public void ShowMessage(string newMessage)
        {
            //MessageBox.Show(newMessage);
            Invoke(new Action(() => lstMessages.Items.Add(newMessage)));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            communication = new Communication(this);
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            communication.StartServer();
        }

        public string GetPortServer()
        {
            return txtPortServer.Text;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            communication.Connect();
        }

        public string GetIpClient()
        {
            return txtIpClient.Text;
        }

        public string GetPortClient()
        {
            return txtPortClient.Text;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            communication.SendMessage();
        }
    }
}
