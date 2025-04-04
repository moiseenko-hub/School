using Microsoft.AspNetCore.Mvc;
using StoreData.Models;
using StoreData.Repostiroties;
using StoreData.Repostiroties.School;
using WebStoryFroEveryting.Models.Lessons;

namespace WebStoryFroEveryting.Controllers.ApiControllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ILessonCommentRepository _commentRepository;
    private readonly ISchoolUserRepository _userRepository;

    public CommentController(ILessonCommentRepository commentRepository, ISchoolUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }

    public LessonCommentViewModel Create([FromForm] ApiCommentViewModel viewModel)
    {
        var lessonCommentViewModel = new LessonCommentViewModel()
        {
            Created = DateTime.UtcNow,
            Description = viewModel.Description,
            Id = viewModel.LessonId,
            Username = _userRepository.Get(viewModel.UserId).Username
        };
        _commentRepository.AddComment(lessonCommentViewModel.Id,_userRepository.Get(viewModel.UserId).Id,viewModel.Description);
        return lessonCommentViewModel;
    }
}