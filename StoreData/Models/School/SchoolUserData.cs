using Enums.SchoolUser;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreData.Repostiroties;

namespace StoreData.Models;

public class SchoolUserData : BaseModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<LessonCommentData> Comments { get; set; } = [];
    public SchoolRoleData? Role { get; set; }
    
    public List<MessageData> Messages { get; set; }

    public Locale Locale { get; set; }
}