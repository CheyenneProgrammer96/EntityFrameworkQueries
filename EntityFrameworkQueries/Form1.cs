using EntityFrameworkQueries.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EntityFrameworkQueries
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectAllVendors_Click(object sender, EventArgs e)
        {
            using ApContext dbContext = new ApContext();


            // LINQ (Langauge Integrated Query) method syntax
            List<Vendor> vendorList = dbContext.Vendors.ToList();

            // LINQ query syntax
            List<Vendor> vendorList2 = (from v in dbContext.Vendors
                                       select v).ToList();
        }

        private void btnAllCaliVendors_Click(object sender, EventArgs e)
        {
            using ApContext dbContext = new();

            // LINQ (Langauge Integrated Query) method syntax
            List<Vendor> vendorList = dbContext.Vendors
                                        .Where(v => v.VendorState == "CA")
                                        .OrderBy(v => v.VendorName)
                                        .ToList();

            // LINQ query syntax
            List<Vendor> vendorList2 = (from v in dbContext.Vendors
                                       where v.VendorState == "CA"
                                       orderby v.VendorName
                                       select v).ToList();
        }

        private void btnSelectSpecificColumns_Click(object sender, EventArgs e)
        {
            ApContext dbContext = new();
            // Anonymus type
            List<VendorLocation> results = (from v in dbContext.Vendors
                          select new VendorLocation
                          {
                             VendorName  = v.VendorName,
                             VendorState = v.VendorState,
                             VendorCity = v.VendorCity
                          }).ToList();

            StringBuilder displayString = new StringBuilder(); 
            foreach ( VendorLocation vendor in results ) 
            {
                displayString.AppendLine($"{vendor.VendorName} is in {vendor.VendorCity}");
            }

            MessageBox.Show(displayString.ToString());
        }

        private void btnMiscQueries_Click(object sender, EventArgs e)
        {
            ApContext dbContext = new();

            // Check if a Vendor exists in Washington
            bool doesExist = (from v in dbContext.Vendors
                              where v.VendorState == "WA"
                              select v).Any();

            // Get number of Invoices
            int invoiceCount = (from invoice in dbContext.Invoices
                               select invoice).Count();

            // Query a single Vendor
            Vendor? singleVendor = (from v in dbContext.Vendors
                          where v.VendorName == "Joe's Burger Shack"
                          select v).SingleOrDefault();

            if (singleVendor != null)
            {
                // Do something with the Vendor object
            }
        }
    }
    class VendorLocation
    {
        public string VendorName { get; set; }

        public string VendorState { get; set; }

        public string VendorCity { get; set; }
    }
}