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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WinWam6
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        private string formattedAddress = "";
        private double latitude;
        private double longitude;
        private int zoom = 15;
        private string location = "";
        private string mapType = "roadmap";

        public MapControl()
        {
            InitializeComponent();
        }

        public string FormattedAddress
        {
            get { return formattedAddress; }
        }

        public double Latitude
        {
            get { return latitude; }
        }

        public double Longitude
        {
            get { return longitude; }
        }

        public void ShowMapFromAddress(string Address)
        {
            string geocodeURL = @"http://maps.googleapis.com/maps/api/geocode/xml?address=" + Address + "&sensor=false";
            XDocument geoDoc = XDocument.Load(geocodeURL);
            string ss = geoDoc.ToString();
            string responseStatus = geoDoc.Element("GeocodeResponse").Element("status").Value;
            if (responseStatus == "OK")
            {
                formattedAddress =
                    (string) geoDoc.Element("GeocodeResponse").Element("result").Element("formatted_address").Value;
                location = formattedAddress;
                string slatitude =
                    (string)
                    geoDoc.Element("GeocodeResponse")
                          .Element("result")
                          .Element("geometry")
                          .Element("location")
                          .Element("lat")
                          .Value;
                string slongitude =
                    (string)
                    geoDoc.Element("GeocodeResponse")
                          .Element("result")
                          .Element("geometry")
                          .Element("location")
                          .Element("lng")
                          .Value;

                double d = 0;
                if (double.TryParse(slatitude, out d))
                {
                    latitude = d;
                }
                if (double.TryParse(slongitude, out d))
                {
                    longitude = d;
                }

                string locationType =
                    (string)
                    geoDoc.Element("GeocodeResponse")
                          .Element("result")
                          .Element("geometry")
                          .Element("location_type")
                          .Value;
            }
            ShowMapImage(Address);
        }

        public void ShowMapImage(string MyLoc)
        {
            BitmapImage bmpImage = new BitmapImage();

            string mapURL = "http://maps.googleapis.com/maps/api/staticmap?size=500x400&markers=size:mid%7Ccolor:red%7C" + MyLoc + "&zoom=" + zoom.ToString() + "&maptype=" + mapType + "&sensor=false";

            bmpImage.BeginInit();
            bmpImage.UriSource = new Uri(mapURL);
            bmpImage.EndInit();

            imgMap.Source = bmpImage;
            location = MyLoc;

        }

                private void ShowMapUsingLatLng()
        {
            BitmapImage bmpImage = new BitmapImage();
                    string mapURL = "http://maps.googleapis.com/maps/api/staticmap?center=" + latitude + "," + longitude + "&" +
                                    "size=500x400&markers=size:mid%7Ccolor:red%7C" + location + "&zoom=" + zoom +
                                    "&maptype=" + mapType + "&sensor=false";
            bmpImage.BeginInit();
            bmpImage.UriSource = new Uri(mapURL);
            bmpImage.EndInit();
            imgMap.Source = bmpImage;
        }


        private void ZoomIn()
        {
            if (zoom < 21)
            {
                zoom += 1;
                ShowMapUsingLatLng();

                if (!ZoomOutButton.IsEnabled) ZoomOutButton.IsEnabled = true;
            }
            else
            {
                ZoomInButton.IsEnabled = true;
            }
        }

        private void ZoomOut()
        {
            if (zoom > 0)
            {
                zoom -= 1;
                ShowMapUsingLatLng();

                if (!ZoomInButton.IsEnabled) ZoomInButton.IsEnabled = true;
            }
            else
            {
                ZoomOutButton.IsEnabled = true;
            }
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            ZoomOut();
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            ZoomIn();
        }

        private void MoveLeftButton_Click(object sender, RoutedEventArgs e)
        {
            int i = 1;
            i += 1;
        }
    }
}
