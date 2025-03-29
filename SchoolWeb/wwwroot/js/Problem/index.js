$(document).ready(function () {
    $.get("http://localhost:5294/problems").then(function (response) {
        for (let item of response) {
            $(".problems").append(`
            <div class="problem template">
                <div class="id-container" style="display:none">
                    <p class="id">${item.id}</p>
                </div>
                <div class="name-container">
                    <p class="name">${item.name}</p>
                </div>
                <div class="description-container">
                    <p class="description">${item.description}</p>
                </div>
                <div class="theme-container">
                    <p class="theme">${item.theme}</p>
                </div>
                <button class="remove-button">Remove</button>
            </div>
            `);
        }
    });

    $(".new-problem-button").click(function () {
        const newProblemTag = $(this).closest(".new-problem");
        const name = newProblemTag.find(".name").val();
        const description = newProblemTag.find(".description").val();
        const theme = newProblemTag.find(".theme").val();

        $.post(`http://localhost:5294/addProblem?name=${name}&description=${description}&theme=${theme}`)
            .then(function () {
                $(".problems").append(`
                        <div class="problem template">
                            <div class="name-container">
                                <p class="name">${name}</p>
                            </div>
                            <div class="description-container">
                                <p class="description">${description}</p>
                            </div>
                            <div class="theme-container">
                                <p class="theme">${theme}</p>
                            </div>
                            <button class="remove-button">Remove</button>
                        </div>
                    `);
            });
    });

    $(".problems").on('click', '.remove-button', function () {
        const problemElement = $(this).closest(".problem");
        const id = problemElement.find(".id").text();

        $.ajax({
            url: `http://localhost:5294/problems?id=${id}`,
            type: 'DELETE',
            success: function () {
                problemElement.remove();
            },
            error: function (error) {
                alert("Error: " + error.statusText);
            }
        });
    });
});
