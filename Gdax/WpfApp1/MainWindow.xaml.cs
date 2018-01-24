using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a new 'HttpWebRequest' object to the mentioned URL.
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.gdax.com/products/BTC-USD/book?level=2");
            myHttpWebRequest.UserAgent = ".NET Framework Test Client";
            // Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            // Display the contents of the page to the console.
            Stream streamResponse = myHttpWebResponse.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            Char[] readBuff = new Char[256];
            //int count = streamRead.Read(readBuff, 0, 256);
            string myResult = streamRead.ReadToEnd();
            var coinData = JsonConvert.DeserializeObject<Coin>(myResult);
            string allBids = "";

            for (int i = 0; i < 50; i++)
            {
                string current = coinData.bids[i][0].ToString();
                string bids = string.Format("Bid: {0}", current);
                bidsList.Items.Add(bids);

            }
            
            for (int i = 0; i < 50; i++)
            {
                string current = coinData.asks[i][0].ToString();
                string asks = string.Format("Asks: {0}", current);
                asksList.Items.Add(asks);
            }

            
            

            streamRead.Close();
            streamResponse.Close();
            myHttpWebResponse.Close();
            Console.ReadLine();
        }

       
    }
}
