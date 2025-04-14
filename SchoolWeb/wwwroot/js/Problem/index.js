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
        <textarea type="text" class="answer-content" placeholder="Answer"></textarea>
        <button class="send-answer-button" data-id="${item.id}">Send</button>
        <button class="remove-button">Remove</button>
    </div>
`);
        }
    });

    $(".problems").on('click', '.send-answer-button', function () {
        const container = $(this).closest(".problem");
        const content = container.find(".answer-content").val();
        const id = $(this).data("id");

        fetch("http://localhost:5294/answer", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ content: content, id: id })
        })
            .then(res => res.text().then(text => {
                alert(text);

                if (res.ok && text.includes("Тест пройден")) {
                    // Дополнительный POST запрос
                    fetch("/api/ProblemApi/Passed", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify({ id: id, result: "passed"})
                    }).then(r => {
                        console.log("Доп. запрос отправлен", r.status);
                    }).catch(e => {
                        console.error("Ошибка доп. запроса", e);
                    });
                }
            }))
            .catch(err => alert("Fetch error: " + err));
    });



    $(".new-problem-button").click(function () {
        const newProblemTag = $(this).closest(".new-problem");
        const name = newProblemTag.find(".name").val();
        const description = newProblemTag.find(".description").val();
        const theme = newProblemTag.find(".theme").val();
        const testName = newProblemTag.find(".testName").val();

        $.post(`http://localhost:5294/addProblem?name=${name}&description=${description}&theme=${theme}&testName=${testName}`)
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
