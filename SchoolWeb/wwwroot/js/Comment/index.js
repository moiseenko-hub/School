$(document).ready(function () {
    $("#comment-submit").click(function (event) {
        event.preventDefault()

        const commentsSection = $(this).closest(".comments-section")
        const newCommentTag = commentsSection.find("#description")
        const newCommentValue = newCommentTag.val().trim()
        const lessonId = commentsSection.data("lesson-id")
        const userId = commentsSection.data("user-id")
        
        const comment = {
            LessonId: lessonId,
            UserId: userId,
            Description: newCommentValue
        };
        
        $.post("/api/comment/create", comment)
            .then(function (response) {
                newCommentTag.val('');
                $(".comments-list").append(
                    `
                    <div class="comment-item">
                        <div class="comment-description">${response.description}</div>
                        <div class="comment-username">${response.username}</div>
                        <div class="comment-created">${new Date(response.created).toLocaleString()}</div>
                    </div>
                    `
                );
            })
            .fail(function (error) {
                console.error("Ошибка при добавлении комментария:", error);
                alert("Произошла ошибка при добавлении комментария.");
            });
    });
});
