using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web.Services;
using System.Linq;
using System.Xml.Linq;

namespace soapbar
{

    public partial class Form1 : Form
    {
        Hashtable services = new Hashtable();

        public Form1()
        {
            InitializeComponent();
        }

        private void addService(string name, Service serv)
        {
            services.Add(name,serv);
            servicesCombo.Items.Add(name);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);

//            addService("Nope", new NoService());
//            addService("Amazon", new AmazonService());
//            addService("Cd Dvd", new CddvdService());
            addService("Bing", new BongService());
            addService("Location", new locationService());
            addService("VisierR", new VizService());
            addService("Dvd Movies", new DvDMoviesService());
            addService("Nasa Extraterrestrial Db", new NEDService());
            addService("Send Sms", new SmsService());
            addService("Gepir Lookup", new GepirService());
            addService("Ip2Geolocation", new Ip2geoService());
            addService("Local Weather", new WeatherService());
            addService("Country Info", new CountryService());
            servicesCombo.Text = (string)servicesCombo.Items[0];
            current().showInput(props);
        }

        private Service current()
        {
            string name = servicesCombo.Text;
            if ( !services.Contains(name) )
                return null;
            return (Service)services[name];
        }

        private void servicesCombo_Click(object sender, EventArgs e)
        {
            Service sel = current();
            if (sel != null)
                sel.showInput(props);
        }
        void servicesCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Service sel = current();
            if (sel != null)
                sel.showInput(props);
        }


        private void queryButton_Click(object sender, EventArgs e)
        {
            Service sel = current();
            if (sel != null)
            {
                sel.doQuery(props);
            }
        }

        void Form1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            props.ExpandAllGridItems();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            props.CollapseAllGridItems();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
