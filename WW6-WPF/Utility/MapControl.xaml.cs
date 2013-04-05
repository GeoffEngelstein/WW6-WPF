using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace WinWam6
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl
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
            var metadata = new FrameworkPropertyMetadata(OnLocationChanged);
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
            var mc = (MapControl) d;
            mc.ShowMapFromAddress((string)e.NewValue);
        }

        public void ShowMapFromAddress(string Address)
        {
            string geocodeURL = @"http://maps.googleapis.com/maps/api/geocode/xml?address=" + Address + "&sensor=false";
            XDocument geoDoc = XDocument.Load(geocodeURL);

            var xElement = geoDoc.Element("GeocodeResponse");
            if (xElement != null)
            {
                var element = xElement.Element("status");
                if (element != null)
                {
                    string responseStatus = element.Value;
                    if (responseStatus == "OK")
                    {
                        var element3 = xElement.Element("result");
                        if (element3 != null)
                        {
                            var element1 = element3.Element("formatted_address");
                            if (element1 != null)
                            {
                                formattedAddress =
                                    element1.Value;
                            }
                        }

                        var xElement1 = xElement.Element("result");
                        if (xElement1 != null)
                        {
                            var element1 = xElement1.Element("geometry");
                            if (element1 != null)
                            {
                                var xElement2 = element1.Element("location");
                                if (xElement2 != null)
                                {
                                    var element2 = xElement2.Element("lat");
                                    if (element2 != null)
                                    {
                                        string slatitude =
                                            element2
                                                .Value;
                                        var xElement3 = xElement2.Element("lng");
                                        if (xElement3 != null)
                                        {
                                            string slongitude =
                                                    xElement3
                                                        .Value;

                                            double d;
                                            if (double.TryParse(slatitude, out d))
                                            {
                                                latitude = d;
                                            }
                                            if (double.TryParse(slongitude, out d))
                                            {
                                                longitude = d;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            ShowMapImage(Address);
        }

        private void ShowMapImage(string MyLoc)
        {
            var bmpImage = new BitmapImage();

            string mapURL = "http://maps.googleapis.com/maps/api/staticmap?size=500x400&markers=size:mid%7Ccolor:red%7C" + MyLoc + "&zoom=" + zoom.ToString(CultureInfo.InvariantCulture) + "&maptype=" + mapType + "&sensor=false";

            bmpImage.BeginInit();
            bmpImage.UriSource = new Uri(mapURL);
            bmpImage.EndInit();

            imgMap.Source = bmpImage;
            location = MyLoc;

        }

                private void ShowMapUsingLatLng()
        {
            var bmpImage = new BitmapImage();
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

            if (latitude < 88)
            {
                if (15 == zoom)
                {
                    latitude += 0.003;
                }
                else
                {
                    double diff;
                    double shift;
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


            if (latitude > -88)
            {
                if (15 == zoom)
                {
                    latitude -= 0.003;
                }
                else
                {
                    double diff;
                    double shift; 
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



            if (longitude > -178)
            {
                if (15 == zoom)
                {
                    longitude -= 0.003;
                }
                else
                {
                    double diff;
                    double shift; 
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



            if (longitude < 178)
            {
                if (15 == zoom)
                {
                    longitude += 0.003;
                }
                else
                {
                    double diff;
                    double shift; 
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
