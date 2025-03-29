namespace StoreData.Models;

public class MessageData : BaseModel
{
    public string Message { get; set; } = string.Empty;
    public virtual SchoolUserData? User { get; set; }
}