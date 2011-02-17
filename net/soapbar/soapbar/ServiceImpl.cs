using System.Windows.Forms;
using System.Web.Services;
using System.Linq;
using System.Xml;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System;


namespace soapbar
{
    public class NoService : soapbar.Service
    {
        struct Input
        {
            string url;
            public string Url
            {
                get { return url; }
                set { url = value; }
            }
        };
        struct Output
        {
            string message;
            public string Message
            {
                get { return message; }
                set { message = value; }
            }
        };
        Input input = new Input();
        Output output = new Output();
        
        public void showInput(PropertyGrid grid)
        {
            grid.SelectedObject = input;
        }
        public bool doQuery(PropertyGrid grid)
        {
            grid.SelectedObject = output;
            return false;
        }
    };



    public class Ip2geoService : soapbar.Service
    {
        struct Input
        {
            string ip;
            public string Ip
            {
                get { return ip; }
                set { ip = value; }
            }
        };
        private Input input = new Input();
        private IP2Geo geo = new IP2Geo();
        private IPInformation res = null;

        public void showInput(PropertyGrid grid)
        {
            grid.SelectedObject = input;
        }
        public bool doQuery(PropertyGrid grid)
        {
            input = (Input)grid.SelectedObject; ;
            this.res = geo.ResolveIP(input.Ip, "0");
            grid.SelectedObject = this.res;
            return (this.res != null);
        }
    };

    public class CountryService : soapbar.Service
    {
        struct Input
        {
            string countryIso;
            public string CountryIso
            {
                get { return countryIso; }
                set { countryIso = value; }
            }
        };
        private Input input = new Input();
        private CountryInfoService info = new CountryInfoService();
        private tCountryInfo res = null;

        public void showInput(PropertyGrid grid)
        {
            grid.SelectedObject = input;
        }
        public bool doQuery(PropertyGrid grid)
        {
            input = (Input)grid.SelectedObject; ;
            this.res = info.FullCountryInfo(input.CountryIso);
            grid.SelectedObject = this.res;
            return (this.res != null);
        }
    };

    public class SmsService : soapbar.Service
    {
        struct Input
        {
            string email;
            public string Email
            {
                get { return email; }
                set { email = value; }
            }
            string mobileNumber;
            public string MobileNumber
            {
                get { return mobileNumber; }
                set { mobileNumber = value; }
            }
            string countryCode;
            public string CountryCode
            {
                get { return countryCode; }
                set { countryCode = value; }
            }
            string message;
            public string Message
            {
                get { return message; }
                set { message = value; }
            }
            string ok;
            public string Ok
            {
                get { return ok; }
                set { ok = value; }
            }
        };
        private Input input = new Input();
        private SendSMSWorld sms = new SendSMSWorld();

        public void showInput(PropertyGrid grid)
        {
            input.Ok = "";
            grid.SelectedObject = input;
        }
        public bool doQuery(PropertyGrid grid)
        {
            input = (Input)grid.SelectedObject; ;
            input.Ok = "";
            input.Ok = sms.sendSMS(input.Email, input.CountryCode, input.MobileNumber, input.Message);
            grid.SelectedObject = this.input;
            return (input.Ok != null);
        }
    };

    public class GepirService : soapbar.Service
    {
        private GetPartyByGTIN gp = new GetPartyByGTIN();
        private router info = new router();
        private gepirParty res = null;

        public void showInput(PropertyGrid grid)
        {
            gp.requestedLanguages = new string[] { "en" };
            gp.version = 2;
            grid.SelectedObject = gp;
        }
        public bool doQuery(PropertyGrid grid)
        {
            // input = (Input)grid.SelectedObject; ;
            //gp.requestedGln = new string[] { input.Gln };
            //gp.requestedLanguages = new string[] { "en" };
            //gp.version = 2;
            gp = (GetPartyByGTIN)grid.SelectedObject; ;
            this.res = info.GetPartyByGTIN(gp);
            grid.SelectedObject = this.res;
            return (this.res != null);
        }
    };

    //public class AmazonService : soapbar.Service
    //{
    //    private GetPartyByGLN gp = new GetPartyByGLN();
    //    private EasyAmazonService amz = new EasyAmazonService();
    //    private gepirParty res = null;

    //    public void showInput(PropertyGrid grid)
    //    {
    //        grid.SelectedObject = gp;
    //    }
    //    public bool doQuery(PropertyGrid grid)
    //    {
    //        // input = (Input)grid.SelectedObject; ;
    //        //gp.requestedGln = new string[] { input.Gln };
    //        //gp.requestedLanguages = new string[] { "en" };
    //        //gp.version = 2;
    //        gp = (GetPartyByGLN)grid.SelectedObject; ;
    //        this.res = amz.(gp);
    //        grid.SelectedObject = this.res;
    //        return (this.res != null);
    //    }
    //};

    public class DvDMoviesService : soapbar.Service
    {
        struct Input
        {
            string upc;
            public string Upc
            {
                get { return upc; }
                set { upc = value; }
            }
        };
        private Input input = new Input();
        private MovieInformation amz = new MovieInformation();
        private Movie res = null;

        public void showInput(PropertyGrid grid)
        {
            grid.SelectedObject = input;
        }
        public bool doQuery(PropertyGrid grid)
        {
            input = (Input)grid.SelectedObject; ;
            this.res = amz.GetByUPC(input.Upc);
            grid.SelectedObject = this.res;
            return (this.res != null);
        }
    };

    public class locationService : soapbar.Service
    {
        private Location input = new Location();
        private Locations loc = new Locations();
        private LocationInfo [] res = null;

        public void showInput(PropertyGrid grid)
        {
            grid.SelectedObject = input;
        }
        public bool doQuery(PropertyGrid grid)
        {
            input = (Location)grid.SelectedObject; ;
            this.res = loc.getLocationWithInfo(input,false,"anon");
            grid.SelectedObjects = this.res;
            return (this.res != null);
        }
    };

    public class BongService : soapbar.Service
    {
        private SearchRequest input = new SearchRequest();
        private BingService bing = new BingService();
        private SearchResponse res = null;
       
        public void showInput(PropertyGrid grid)
        {
            input.AppId = "F7F0BEFDE0AF0B076753D283540B26F6613DF3C8";
            grid.SelectedObject = input;
        }
        public bool doQuery(PropertyGrid grid)
        {
            input = (SearchRequest)grid.SelectedObject; ;
            this.res = bing.Search(input);
            grid.SelectedObject = this.res;
            return (this.res != null);
        }
    };

    public class CddvdService : soapbar.Service
    {
        struct Input
        {
            string query;
            public string Query
            {
                get { return query; }
                set { query = value; }
            }
            string field;
            public string Field
            {
                get { return field; }
                set { field = value; }
            }
        };
        private Input input = new Input();
        private FlashCDDBService amz = new FlashCDDBService();
        private SearchResultArray res = null;

        public void showInput(PropertyGrid grid)
        {
            grid.SelectedObject = input;
        }
        public bool doQuery(PropertyGrid grid)
        {
            input = (Input)grid.SelectedObject; ;
            this.res = amz.searchArtistList("anything","anything", input.Query, input.Field);
            grid.SelectedObject = this.res;
            return (this.res != null);
        }
    };

    public class WeatherService : soapbar.Service
    {
        struct WeatherData
        {
            string country;
            public string Country
            {
                get { return country; }
                set { country = value; }
            }
            string town;
            public string Town
            {
                get { return town; }
                set { town = value; }
            }
            string weather;
            public string Weather
            {
                get { return weather; }
                set { weather = value; }
            }
        };
        private GlobalWeather glw = new GlobalWeather();
        private WeatherData data = new WeatherData();

        public void showInput(PropertyGrid grid)
        {
            data.Weather = "";
            grid.SelectedObject = data;
        }
        public bool doQuery(PropertyGrid grid)
        {
            data = (WeatherData)grid.SelectedObject; ;
            data.Weather = glw.GetWeather(data.Town, data.Country);
            grid.SelectedObject = this.data;
            return (this.data.Weather != null);
        }
    };

    public class NEDService : soapbar.Service
    {
        struct NEDData
        {
            double ra;
            public double Ra
            {
                get { return ra; }
                set { ra = value; }
            }
            double dec;
            public double Dec
            {
                get { return dec; }
                set { dec = value; }
            }
            double radius;
            public double Radius
            {
                get { return radius; }
                set { radius = value; }
            }
        };
        private NED ned = new NED();
        private NEDData data = new NEDData();
        private ObjInfo[] obj;

        public void showInput(PropertyGrid grid)
        {
            grid.SelectedObject = data;
        }
        public bool doQuery(PropertyGrid grid)
        {
            data = (NEDData)grid.SelectedObject; ;
            obj = ned.ObjNearPosn(data.Ra, data.Dec, data.Radius);
            grid.SelectedObjects = obj;
            return (this.obj != null);
        }
    };

    public class VizService : soapbar.Service
    {
        struct VizData
        {
            string target;
            public string Target
            {
                get { return target; }
                set { target = value; }
            }
            double radius;
            public double Radius
            {
                get { return radius; }
                set { radius = value; }
            }
            string unit;
            public string Unit
            {
                get { return unit; }
                set { unit = value; }
            }
            string text;
            public string Text
            {
                get { return text; }
                set { text = value; }
            }
        };
        struct OutData
        {
            string result;
            public string Result
            {
                get { return result; }
                set { result = value; }
            }
        };
        private VizieRService viz = new VizieRService();
        private VizData data = new VizData();
        private ObjInfo[] obj;

        public void showInput(PropertyGrid grid)
        {
            data.Target = "M31";
            data.Radius = 1.0;
            data.Unit = "arcmin";
            data.Text = "Dixon";
            grid.SelectedObject = data;
        }
        public bool doQuery(PropertyGrid grid)
        {
            data = (VizData)grid.SelectedObject; ;
            string res = viz.cataloguesData(data.Target, data.Radius, data.Unit, data.Text);
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(res);
                //XmlNodeList root = doc.DocumentElement.ChildNodes;//SelectNodes("VOTABLE");
                //object[] o = new object[root.Count];
                //int i=0;
                //foreach (XmlNode n in root)
                //{
                //    o[i++] = n.InnerXml;
                //}
                //grid.SelectedObjects = o;
                grid.SelectedObject = doc.DocumentElement.FirstChild;
                return true;
            }
            catch (Exception) {}

            OutData x = new OutData();
            x.Result = res;
            grid.SelectedObject = x;
            return (x.Result != null);
        }
    };

}