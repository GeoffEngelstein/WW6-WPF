using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;

namespace WinWam6
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BusinessDetail : UserControl
    {
        public CBusiness curBus {get; set;}
        private CBusiness lBus;

        public BusinessDetail()
        {
            lBus = new CBusiness("000050");
            curBus = lBus;
            DataContext = this;
            InitializeComponent();
        }



        private void fBusinessDetail_Loaded(object sender, RoutedEventArgs e)
        {
            lstBusID.Items.Clear();
            DbDataReader dr = WWD.GetReader("select bus_id, bus_name from business order by bus_id");
            while (dr.Read())
            {
                lstBusID.Items.Add(dr[0].ToString()); //+ " " + dr[1].ToString());
            }
            dr.Dispose();

        }

        private void lstBusID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = lstBusID.SelectedItem.ToString();

            lBus = new CBusiness(s);
            curBus = lBus;
            DataContext = null;     //Refresh all the bindings to match the new object
            DataContext = this;
            

            string location = curBus.PhysicalAddress.LocationString;
            string geocodeURL  = @"http://maps.googleapis.com/maps/api/geocode/xml?address=" + location + "&sensor=false";
            XDocument geoDoc = XDocument.Load(geocodeURL);
            string ss = geoDoc.ToString();
            string responseStatus = geoDoc.Element("GeocodeResponse").Element("status").Value;
            if (responseStatus == "OK")
            {
                string formattedAddress = (string)geoDoc.Element("GeocodeResponse").Element("result").Element("formatted_address").Value;
                string latitude = (string)geoDoc.Element("GeocodeResponse").Element("result").Element("geometry").Element("location").Element("lat").Value;
                string longitude = (string)geoDoc.Element("GeocodeResponse").Element("result").Element("geometry").Element("location").Element("lng").Value;

                double d = 0;
                if (double.TryParse(latitude, out d))
                {
                    curBus.Latitude = d;
                }
                if (double.TryParse(longitude, out d))
                {
                    curBus.Longitude = d;
                }

                string locationType = (string)geoDoc.Element("GeocodeResponse").Element("result").Element("geometry").Element("location_type").Value;
            }
            ShowMapImage(curBus.PhysicalAddress.LocationString);

        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (curBus.IsDirty)
            {
                curBus.Save();
            }
        }

        private void cmdMapPhys_Click(object sender, RoutedEventArgs e)
        {
            //Display the Map
            
        }

        public void ShowMapImage(string MyLoc)
        {
            BitmapImage bmpImage = new BitmapImage();
            string MapType = "roadmap";
            int zoom = 15;
            string mapURL = "http://maps.googleapis.com/maps/api/staticmap?size=500x400&markers=size:mid%7Ccolor:red%7C" + MyLoc + "&zoom=" + zoom.ToString() + "&maptype=" + MapType + "&sensor=false";

            bmpImage.BeginInit();
            bmpImage.UriSource = new Uri(mapURL);
            bmpImage.EndInit();

            imgMap.Source = bmpImage;

        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
