namespace ExcelImporter.Models
{
    public class FileUploadViewModel
    {
        public int RowNumber { get; set; }
        public string PickupStoreNumber { get; set; }
        public string PickupStoreName { get; set; }
        public string PickupLat { get; set; }
        public string PickupLong { get; set; }
        public string PickupFormattedAddress { get; set; }
        public string PickupContactNameFirstName { get; set; }
        public string PickupContactNameLastName { get; set; }
        public string PickupContactEmail { get; set; }
        public string PickupContactMobileNumber { get; set; }
        public string PickupEnableSMSNotification { get; set; }
        public string PickupTime { get; set; }
        public string PickupTolerance { get; set; }
        public string PickupServiceTime { get; set; }
        public string DeliveryStoreNumber { get; set; }
        public string DeliveryStoreName { get; set; }
        public string DeliveryLat { get; set; }
        public string DeliveryLong { get; set; }
        public string DeliveryFormattedAddress { get; set; }
        public string DeliveryContactFirstName { get; set; }
        public string DeliveryContactLastName { get; set; }
        public string DeliveryContactEmail { get; set; }
        public string DeliveryContactMobileNumber { get; set; }
        public string DeliveryEnableSMSNotification { get; set; }
        public string DeliveryTime { get; set; }
        public string DeliveryTolerance { get; set; }
        public string DeliveryServiceTime { get; set; }
        public string OrderDetails { get; set; }
        public string AssignedDriver { get; set; }
        public string CustomerReference { get; set; }
        public string Payer { get; set; }
        public string Vehicle { get; set; }
        public string Weight { get; set; }
        public string Price { get; set; }
    }
}
