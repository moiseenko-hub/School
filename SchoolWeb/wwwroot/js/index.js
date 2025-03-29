$(document).ready(function (){
    const url = "/hub/lesson"
    const hub = new signalR
        .HubConnectionBuilder()
        .withUrl(url)
        .build()
    
    hub.on("NewLesson", function (viewModel){
        $("#notification").find(".description").text(viewModel.title)
    })
    
    hub.start()
})