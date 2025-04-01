$(document).ready(function(){
    $.get("http://localhost:5294/api-docs")
        .then(result => {
            for (let i of result) {
                let methodInfo = {
                    route: i.route || '',
                    method: i.methods ? i.methods[0] : '',
                    returnType: i.returnType || '',
                    params: []
                };

                if (i.parameters) {
                    for (let parameter of i.parameters) {
                        methodInfo.params.push({
                            name: parameter.name,
                            type: parameter.type
                        });
                    }
                }

                console.log(methodInfo);

                let paramsHtml = '';
                for (let i = 1; i < methodInfo.params.length; i++) {
                    let param = methodInfo.params[i];
                    paramsHtml += `<div>${param.name} (${param.type})</div>`;
                    paramsHtml += `<div>
                        <input name="${param.name}" id="${methodInfo.route + methodInfo.method + param.name}" class="${param.name}"/>
                    </div>`
                }


                $(".methods").append(`
                    <div class="method">
                        <div class="route">${methodInfo.route}</div>
                        <div class="method">${methodInfo.method}</div>
                        <div class="returnType">${methodInfo.returnType}</div>
                        <div class="params">${paramsHtml}</div>
                        <button class="send-button">Send</button>
                    </div>
                `);
            }
        })
        .catch(error => {
            console.error('Ошибка при получении данных:', error);
        });

    $(".methods").on('click', '.send-button',function () {
        const methodTag = $(this).closest(".method");
        const routeName = methodTag.find(".route").text();
        const methodName = methodTag.find(".method").text();
        const paramsHtml = methodTag.find(".params");

        let queryParams = '';
        paramsHtml.find('input').each(function() {
            if (queryParams !== '') {
                queryParams += '&';
            }
            queryParams += $(this).attr('name') + '=' + $(this).val();
        });

        const fullUrl = `http://localhost:5294${routeName}?${queryParams}`;

        $.ajax({
            url: `http://localhost:5294${routeName}?${queryParams}`,
            type: `${methodName}`,
            success: function (result) {
                console.log(result);
            },
            error: function (error) {
                alert("Error: " + error.statusText);
            }
        });

    });
});
