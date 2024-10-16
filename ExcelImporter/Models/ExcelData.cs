using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ExcelImporter.Models
{
    public class ExcelData
    {
        [JsonIgnore]
        public int RowNumber { get; set; }

        [Display(Name = "Pickup store #")]
        public int PickupStoreNumber { get; set; }

        [Display(Name = "Pickup store Name")]
        public string PickupStoreName { get; set; }

        [Display(Name = "Pickup lat")]
        public double PickupLat { get; set; }

        [Display(Name = "Pickup lon")]
        public double PickupLong { get; set; }

        [Display(Name = "Pickup formatted Address")]
        public string PickupFormattedAddress { get; set; }

        [Display(Name = "Pickup Contact Name First Name")]
        public string PickupContactNameFirstName { get; set; }

        [Display(Name = "Pickup Contact Name Last Name")]
        public string PickupContactNameLastName { get; set; }

        [Display(Name = "Pickup Contact Email")]
        public string PickupContactEmail { get; set; }

        [Display(Name = "Pickup Contact Mobile Number")]
        public string PickupContactMobileNumber { get; set; }

        [Display(Name = "Pickup Enable SMS Notification")]
        public int PickupEnableSMSNotification { get; set; }

        [Display(Name = "Pickup Time")]
        public string PickupTime { get; set; }

        [Display(Name = "Pickup tolerance (min)")]
        public string PickupTolerance { get; set; }

        [Display(Name = "Pickup Service Time")]
        public string PickupServiceTime { get; set; }

        [Display(Name = "Delivery store #")]
        public int DeliveryStoreNumber { get; set; }

        [Display(Name = "Delivery store Name")]
        public string DeliveryStoreName { get; set; }

        [Display(Name = "Delivery lat (req if adding new customer)")]
        public double DeliveryLat { get; set; }

        [Display(Name = "Delivery long (req if adding new customer)")]
        public double DeliveryLong { get; set; }

        [Display(Name = "Delivery formatted Address")]
        public string DeliveryFormattedAddress { get; set; }

        [Display(Name = "Delivery Contact First Name")]
        public string DeliveryContactFirstName { get; set; }

        [Display(Name = "Delivery Contact Last Name")]
        public string DeliveryContactLastName { get; set; }

        [Display(Name = "Delivery Contact Email")]
        public string DeliveryContactEmail { get; set; }

        [Display(Name = "Delivery Contact Mobile Number (need 0 at the front)")]
        public string DeliveryContactMobileNumber { get; set; }

        [Display(Name = "Delivery Enable SMS Notification (No=0/Yes=1)")]
        public int DeliveryEnableSMSNotification { get; set; }

        [Display(Name = "Delivery Time")]
        public string DeliveryTime { get; set; }

        [Display(Name = "Delivery Tolerance (Min past Delivery Time)")]
        public string DeliveryTolerance { get; set; }

        [Display(Name = "Delivery Service Time (min)")]
        public string DeliveryServiceTime { get; set; }

        [Display(Name = "Order Details")]
        public string OrderDetails { get; set; }

        [Display(Name = "Assigned Driver")]
        public string AssignedDriver { get; set; }

        [Display(Name = "Customer reference")]
        public string CustomerReference { get; set; }

        [Display(Name = "Payer")]
        public string Payer { get; set; }

        [Display(Name = "Vehicle")]
        public string Vehicle { get; set; }

        [Display(Name = "Weight")]
        public int Weight { get; set; }

        [Display(Name = "Price")]
        public int Price { get; set; }
    }
}
