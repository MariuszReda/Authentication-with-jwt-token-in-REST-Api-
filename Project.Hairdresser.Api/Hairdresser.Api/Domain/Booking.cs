namespace Hairdresser.Api.Domain
{
    public class Booking
    {
        TimeSpan BookingTime { get; set; }
        DateTime BookingDate { get; set; }  
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; } // Account 
        DateTime? ModifiedOn { get; set; }
        string ModifiedBy { get; set; } // Account
    }
}
