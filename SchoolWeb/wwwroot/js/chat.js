$(document).ready(function (){
    const url = "/hub/chat"
    
    const hub = new signalR
        .HubConnectionBuilder()
        .withUrl(url)
        .build()
    
    $(".send-new-message").click(function (){
        const closestTag = $(this).closest(".new-message-container")
        const newMessageText = closestTag.find(".new-message").val()
        hub.invoke("AddMessage", newMessageText)
    })
    
    hub.on("NewMessage", function (username, message) {
        
        $(".messages").append(`
            <div class="message template">
                <span class="author">${username}</span>
                <span class="text">${message}</span>
            </div>
        `)
    })
    
    hub.start()
})