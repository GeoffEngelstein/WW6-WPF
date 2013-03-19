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
using System.Windows.Threading;
using System.Threading;
using System.Net;
using System.Drawing;
using System.IO;
using System.Xml.Linq;
using Microsoft.Win32;

namespace WinWam6
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MapDisplay : UserControl
    {
        public MapDisplay()
        {
            InitializeComponent();
        }

#region fields
    private XDocument geoDoc;
    private string location;
    private int zoom;
    private SaveFileDialog saveDialog = new SaveFileDialog();
    private string mapType;
    private double lat;
    private double lng;
#endregion

    private void GetGeocodeData(){
        string geocodeURL  = @"http://maps.googleapis.com/maps/api/geocode/xml?address=" + location + "&sensor=false";
        try
        {
            geoDoc = XDocument.Load(geocodeURL);
        }
        catch (WebException)
            {
                Thread HideBar = new Thread(new ThreadStart(this.HideProgressBar));
                HideBar.Start();
                return;
            }
        
        Thread InstanceCaller = new Thread(new ThreadStart(this.ShowGeocodeData));
        InstanceCaller.Start();
        
        }

    private void ShowGeocodeData()
            {
                string responseStatus = (string)geoDoc.Element("status").Value;    
            if (responseStatus == "OK") {
                string formattedAddress = (string)geoDoc.Element("result").Element("formatted_address").Value;
                string latitude = (string)geoDoc.Element("result").Element("geometry").Element("location").Element("lat").Value;
                string longitude = (string)geoDoc.Element("result").Element("geometry").Element("location").Element("lng").Value;

                string locationType = (string)geoDoc.Element("result").Element("geometry").Element("location").Element("location_type").Value;
                AddressTxtBlck.Text = formattedAddress;
                LatitudeTxtBlck.Text = latitude;
                LongitudeTxtBlck.Text = longitude;

                switch (locationType)
                {
                    case "APPROXIMATE":
                        AccuracyTxtBlck.Text = "Approximate";
                        break;
                    case "ROOFTOP":
                        AccuracyTxtBlck.Text = "Precise";
                        break;
                    default:
                        AccuracyTxtBlck.Text = "Approximate";
                        break;
                }
            
                lat = double.Parse(latitude);
                lng = double.Parse(longitude);

                if (SaveButton.IsEnabled == false) {
                    SaveButton.IsEnabled = true;
                    RoadmapToggleButton.IsEnabled = true;
                    TerrainToggleButton.IsEnabled = true;
                }

        else {
                if (responseStatus == "ZERO_RESULTS") { 
                    MessageBox.Show("Unable to show results for: " + "\n\r" + location, "Unknown Location", MessageBoxButton.OK, MessageBoxImage.Information);
            DisplayXXXs();
            AddressTxtBox.SelectAll();
                }
            }
        }
        
        ShowMapButton.IsEnabled = true;
        ZoomInButton.IsEnabled = true;
        ZoomOutButton.IsEnabled = true;
        MapProgressBar.Visibility = System.Windows.Visibility.Hidden;
            
        }

            private void HideProgressBar()
            {
                MapProgressBar.Visibility = System.Windows.Visibility.Hidden;
                ShowMapButton.IsEnabled = true;
            }

        private void DisplayXXXs()
        {
                   AddressTxtBlck.Text = "XXXXXXXXX, XXXXX, XXXXXX";
        LatitudeTxtBlck.Text = "XXXXXXXXXX";
        LongitudeTxtBlck.Text = "XXXXXXXXXX";
        AccuracyTxtBlck.Text = "XXXXXXXXX";
        }

        public void ShowMapImage(string MyLoc)
        {
        BitmapImage bmpImage = new BitmapImage();
        string MapType = "roadmap";
        string mapURL = "http://maps.googleapis.com/maps/api/staticmap?size=500x400&markers=size:mid%7Ccolor:red%7C" + MyLoc + "&zoom=" + zoom + "&maptype=" + MapType + "&sensor=false";

        bmpImage.BeginInit();
        bmpImage.UriSource = new Uri(mapURL);
        bmpImage.EndInit();

        MapImage.Source = bmpImage;

        }
    }
}
