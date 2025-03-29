namespace StoreData.Models;

public class BannedUserData : BaseModel
{
    public string Email { get; set; }
    public string BanDescription { get; set; }
    public DateTime BanTime { get; set; }
}