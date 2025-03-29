$(document).ready(function(){
    $.get("http://localhost:5294/")
        .then(result => {
            const methodsTag = $(this).find(".methods");
            console.log(result[0].name);
            for(let i in result){
                methodsTag.append(`
                    <div class="method">
                        <p class="method-name">${result[i].name}</p>
                    </div>  
            `)
            }
        })
})