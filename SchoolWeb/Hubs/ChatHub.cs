using Microsoft.AspNetCore.SignalR;
using StoreData.Models;
using StoreData.Repostiroties;
using StoreData.Repostiroties.School;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.Hubs;

public class ChatHub : Hub
{
    private readonly SchoolAuthService _authService;
    private readonly SchoolUserRepository _userRepository;
    private readonly MessageRepository _messageRepository;

    public ChatHub(SchoolAuthService authService, SchoolUserRepository userRepository, MessageRepository messageRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
        _messageRepository = messageRepository;
    }

    public async Task AddMessage(string message)
    {
        try
        {
            var username = _authService.GetUserName();
            if (string.IsNullOrEmpty(username))
                throw new Exception("Username is null or empty");

            var user = _userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception($"User not found for username: {username}");

            _messageRepository.Add(new MessageData { Message = message, User = user });
            await Clients.All.SendAsync("NewMessage", username, message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AddMessage: {ex.Message}");
        }
    }

}