namespace FileUpload.Models
{
    public class ReportModelGeneral
    {
        public int Id { get; set; }
        public int? OrderNumber { get; set; } = null;
        public string ServiceTag { get; set; } = null;
        public string AssetTag { get; set; } = null;
        public string PO_Number { get; set; } = null;
        public string Product_Key { get; set; }
        public string GUID { get; set; } = null;
        public string Onboard_MacAddress { get; set; } = null;
        public string EMBMAC2 { get; set; } = null;
        public string EMBMAC3 { get; set; } = null;
        public string EMBMAC4 { get; set; } = null;
        public string PassThroughMac { get; set; }
        public string WirelessMacAddress { get; set; } = null;
        public string SSDSerialNumber { get; set; } = null;
        public string Model { get; set; } = null;
        public string ModelType { get; set; } = null;
        public string CustName { get; set; } = null;
        public string ChassisDescription { get; set; }
        public string Cpu { get; set; } = null;
        public int? Memory { get; set; } = null;
        public DateTime? ManufacturingDate { get; set; } = null;

        public DateTime? WarranyExpiryDate { get; set; } = null;
        public DateTime? OrderDate { get; set; } = null;
        public string WorkstationID { get; set; } = null;
        public string OS { get; set; } = null;
        public int? CI { get; set; } = null;
        public string FinancialReference { get; set; } = null;
        public long? ExpressServiceCode { get; set; } = null;
        public string ShipContact { get; set; } = null;
        public string ShipAddress1 { get; set; } = null;
        public string ShipAddress2 { get; set; } = null;
        public string ShipAddress3 { get; set; } = null;
        public int? ShipZip { get; set; } = null;
        public string ShipState { get; set; } = null;
        public string ShipCity { get; set; } = null;
        public string ProjectNumber { get; set; } = null;
        public string CustIdentity { get; set; } = null;
        public int? AccountCode { get; set; } = null;
        public string User { get; set; }
        public int? Qty { get; set; } = null;
        public int? QtyInvoiced { get; set; } = null;
        public int? ManufacturerPartNum { get; set; } = null;
        public string Manufacturer { get; set; }
        public float? InvoiceItemExGST { get; set; } = null;
        public int? HD0Size { get; set; } = null;
        public string ShipCompanyName { get; set; } = null;
        public string ShipCountry { get; set; } = null;
        public string ComputerName { get; set; } = null;

        public int? ProcessorSpeed { get; set; } = null;

        public int? VidMemory { get; set; } = null;

        public int? CustomerNumber { get; set; } = null;

        public DateTime? ShipByDate { get; set; } = null;

        public DateTime? ShipDate { get; set; } = null;
        public string OSInstalled { get; set; } = null;

        public string BillCompanyName { get; set; } = null;
    }
}
