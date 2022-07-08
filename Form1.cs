using Opc.UaFx.Client;

namespace OPCUARead
{
    public partial class Form1 : Form
    {
        OpcClient client = new OpcClient("opc.tcp://localhost:4840/");
        
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 100;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            client.Connect();
            timer1.Start();
            lblStatusMessage.Text = "Connected to OPC Server";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (client != null)
                client.Disconnect();
            lblStatusMessage.Text = "Disconected from OPC Server";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
                client.Disconnect();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OpcRead();
            DateTime sensorDateTime = DateTime.Now;
            txtTimeStamp.Text = sensorDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        void OpcRead()
        {
            //temperature
            string tagName = "ns=2;s=Temperature";
            Opc.UaFx.OpcValue opcTemp = client.ReadNode(tagName);

            double temperature = (double)opcTemp.Value;
            txtOpcValue.Text = temperature.ToString("#.##");

            // name
            tagName = "ns=2;s=Name";
            Opc.UaFx.OpcValue opcName = client.ReadNode(tagName);

            string Name = (string)opcName.Value;
            txtName.Text = Name.ToString();

            // position
            tagName = "ns=2;s=Position";
            Opc.UaFx.OpcValue opcPos = client.ReadNode(tagName);

            sbyte position = (sbyte)opcPos.Value;
            txtPosition.Text = position.ToString();

            // IsActive
            tagName = "ns=2;s=IsActive";
            Opc.UaFx.OpcValue opcIsActive = client.ReadNode(tagName);

            bool stt = (bool)opcIsActive.Value;
            lbStt.Text = stt.ToString();

            // status
            tagName = "ns=2;s=Status";
            Opc.UaFx.OpcValue opcStt = client.ReadNode(tagName);

            byte STT = (byte)opcStt.Value;
            txtStatus.Text = STT.ToString();





        }
    }
}