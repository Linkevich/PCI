using System;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace PCI
{
    public partial class PCISearcher : Form
    {
        public PCISearcher()
        {
            InitializeComponent();
        }

        private void PCISearcher_Load(object sender, EventArgs e)
        {
            var provider = new IdParserPCI();
            try
            {
                foreach (var dev in provider.GetDevices())
                    PCIListBox.Items.Add(dev[0].PadRight(100 - dev[0].Length) + "\t\t\t" + dev[1]);
            }
            catch (ManagementException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                PCIListBox.Items.Add(ex.Message);
            }
        }

    }
}
