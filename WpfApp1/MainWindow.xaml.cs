using System;
using System.Data.SqlTypes;
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
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        //public void Authenticate()
        //{
        //    string time = DateTime.UtcNow.ToString("o");
	       // string secret = "";
        //    string method = "POST";
        //    string requestPath = "/orders";
        //    Body body = new Body();

        //        body.price = "1.0";
        //        body.size = "1.0";
        //        body.side = "BUY";
        //        body.product_id = "BTC-USD";

        //    string newBody = JsonConvert.SerializeObject(body);

        //    string message = time + method + requestPath + newBody;
        //    string encodedMessage = Base64Encode(message);
        //    Byte[] encodedMessageAsByte = System.Text.Encoding.UTF8.GetBytes(encodedMessage);
        //    Byte[] data = Convert.FromBase64String(secret);
        //    string decodedString = Encoding.UTF8.GetString(data);
            
        //    HMACSHA256 secretHmacsha256 = new HMACSHA256();
        //    label3.Content = System.Text.Encoding.UTF8.GetString( secretHmacsha256.ComputeHash(encodedMessageAsByte));


        //     You're trying to figure out string vs. byte. You made it to step four but can't figure out how to encode hmacsha256
        //}

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

            //Authenticate();


            streamRead.Close();
            streamResponse.Close();
            myHttpWebResponse.Close();
            Console.ReadLine();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Create a new 'HttpWebRequest' object to the mentioned URL.
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.gdax.com/products/BTC-USD/book?level=2");
            myHttpWebRequest.UserAgent = ".NET Framework Test Client";
            myHttpWebRequest.Method = "POST";
            myHttpWebRequest.Timeout = 12000;
            // Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            // Display the contents of the page to the console.
            Stream streamResponse = myHttpWebResponse.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            string myResult = streamRead.ReadToEnd();
            var coinData = JsonConvert.DeserializeObject<Coin>(myResult);
        }

       
    }
}
