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

        public static DependencyProperty LocationProperty;

        static MapControl()
        {
            var metadata = new FrameworkPropertyMetadata(new PropertyChangedCallback(OnLocationChanged));
            LocationProperty = DependencyProperty.Register("Location", typeof(string), typeof(MapControl), metadata);
        }

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


        public string Location
        {
            get { return (string) GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value);}
            /*
            get { return location; }
            set
            {
                if (value == location) return;
                location = value;
                ShowMapFromAddress(location);
            }*/
        }

        private static void OnLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MapControl mc = (MapControl) d;
            mc.ShowMapFromAddress((string)e.NewValue);
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

        private void ShowMapImage(string MyLoc)
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

        private void MoveUp()
        {
            // Default zoom is 15, and at this level changing
            // the center point is done by 0.003 degs. 
            // Need to change based on the zoom

            double diff;
            double shift;

            if (latitude < 88)
            {
                if (15 == zoom)
                {
                    latitude += 0.003;
                }
                else
                {
                    if (zoom > 15)
                    {
                        diff = zoom - 15;
                        shift = ((15 - diff)*0.003)/15;
                        latitude += shift;
                    }
                    else
                    {
                        diff = 15 - zoom;
                        shift = ((15 + diff)*0.003)/15;
                        latitude += shift;
                    }
                }
                ShowMapUsingLatLng();
            }
            else
            {
                latitude = 90;
            }
        }

        private void MoveDown()
        {
            // Default zoom is 15, and at this level changing
            // the center point is done by 0.003 degs. 
            // Need to change based on the zoom

            double diff;
            double shift;

            if (latitude > -88)
            {
                if (15 == zoom)
                {
                    latitude -= 0.003;
                }
                else
                {
                    if (zoom > 15)
                    {
                        diff = zoom - 15;
                        shift = ((15 - diff) * 0.003) / 15;
                        latitude -= shift;
                    }
                    else
                    {
                        diff = 15 - zoom;
                        shift = ((15 + diff) * 0.003) / 15;
                        latitude -= shift;
                    }
                }
                ShowMapUsingLatLng();
            }
            else
            {
                latitude = -90;
            }
        }

        private void MoveLeft()
        {
            // Default zoom is 15, and at this level changing
            // the center point is done by 0.003 degs. 
            // Need to change based on the zoom

            double diff;
            double shift;

            if (longitude > -178)
            {
                if (15 == zoom)
                {
                    longitude -= 0.003;
                }
                else
                {
                    if (zoom > 15)
                    {
                        diff = zoom - 15;
                        shift = ((15 - diff) * 0.003) / 15;
                        longitude -= shift;
                    }
                    else
                    {
                        diff = 15 - zoom;
                        shift = ((15 + diff) * 0.003) / 15;
                        longitude -= shift;
                    }
                }
                ShowMapUsingLatLng();
            }
            else
            {
                longitude = -180;
            }
        }

        private void MoveRight()
        {
            // Default zoom is 15, and at this level changing
            // the center point is done by 0.003 degs. 
            // Need to change based on the zoom

            double diff;
            double shift;

            if (longitude < 178)
            {
                if (15 == zoom)
                {
                    longitude += 0.003;
                }
                else
                {
                    if (zoom > 15)
                    {
                        diff = zoom - 15;
                        shift = ((15 - diff) * 0.003) / 15;
                        longitude += shift;
                    }
                    else
                    {
                        diff = 15 - zoom;
                        shift = ((15 + diff) * 0.003) / 15;
                        longitude += shift;
                    }
                }
                ShowMapUsingLatLng();
            }
            else
            {
                longitude = 180;
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
            MoveLeft();
        }

        private void MoveUpButton_Click(object sender, RoutedEventArgs e)
        {
            MoveUp();
        }

        private void MoveDownButton_Click(object sender, RoutedEventArgs e)
        {
            MoveDown();
        }

        private void MoveRightButton_Click(object sender, RoutedEventArgs e)
        {
            MoveRight();
        }


        private void RoadmapToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (mapType != "roadmap")
            {
                mapType = "roadmap";
                ShowMapUsingLatLng();
                TerrainToggleButton.IsChecked = false;
            }
        }

        private void TerrainToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (mapType != "terrain")
            {
                mapType = "terrain";
                ShowMapUsingLatLng();
                RoadmapToggleButton.IsChecked = false;
            }
        }
    }
}
