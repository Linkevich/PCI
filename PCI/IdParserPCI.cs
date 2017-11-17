using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PCI
{
    class IdParserPCI
    {
        public string[] GetDataBase()
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send("google.com");

                if (pingReply.Status == IPStatus.Success)
                {
                    var webClient = new WebClient();
                    string returnString;
                    using (webClient)
                    {
                        returnString = webClient.DownloadString("http://pci-ids.ucw.cz/v2.2/pci.ids");
                    }
                    return returnString.Split('\n');
                }
            }
            catch (PingException)
            {}
            return File.ReadLines("pci.ids").ToArray();
        }


        public List<List<string>> GetDevices()
        {          
            var serialQuery = new SelectQuery("SELECT * FROM Win32_PnPEntity");//plug and play;файл менеджера устройств шиндовс(запрос в базу данных)
            var searcher = new ManagementObjectSearcher(new ManagementScope(), serialQuery);//извлекает коллекцию объектов в соотвествии с заданными запросом
            var dev = new Regex("DEV_.{4}");//ищу совпадение по DEV_ и 4 символа после(айди){device}
            var ven = new Regex("VEN_.{4}");//ищу совпадение по VEN_ и 4 символа после(айди){vendor}
            var buffer = new List<List<string>>();//что мы возвращаем

            var file = GetDataBase();//получаем файл со списком устройств

            foreach (var item in searcher.Get())//проходимся по девайсам и вендорам ,которые в компе есть(коллекцию устройств)
            {
                var deviceId = item["DeviceID"].ToString();//ищем айди устройства по ключу
                if (deviceId.Contains("PCI"))
                    buffer.Add(SearchInFile(dev.Match(deviceId).Value.Substring(4).ToLower(),//получаем айди девайса,приводим буквы к строчному виду(чтобы искать в файле)
                        ven.Match(deviceId).Value.Substring(4).ToLower(),//то же самое с вендором
                        file));//отправляем файл с устройствами 
            }
            return buffer;
        }

        private static List<string> SearchInFile(string dev, string ven, string[] PCIids)
        {
            var result = new List<string>();
            var vendorFound = false;
            var deviceFound = false;
            var vendor = new Regex("^" + ven + "  ");//регулярки
            var device = new Regex("^\\t" + dev + "  ");

            foreach (var item in PCIids)//поэлементно проходимся по файлу
            {
                if (item != null)//если строка не пустая
                {
                    if (vendorFound == false && vendor.Match(item).Success)//ищу вендор и сразу к нему девайс
                    {
                        result.Add("VendorID: " + ven + " (" + item.Substring(6) + ")");
                        vendorFound = true;
                    }
                    else if (vendorFound == true && device.Match(item).Success)
                    {
                        result.Add("DeviceID: " + dev + " (" + item.Substring(7) + ")");
                        deviceFound = true;
                        break;
                    }
                }
            }

            if (!vendorFound)//если не нашел в базе вендор,то выводим то,что нашло в системе, и выводим,что не нашли в базе
            {
                result.Add("Device with VendorID " + ven + " and DeviceID " + dev + "wasn't found in base");
                result.Add("");
            }

            if (!deviceFound)
                result.Add("DeviceID: " + dev + " (name can't be found in base)");

            return result;
        }
    }
}
