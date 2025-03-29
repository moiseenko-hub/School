$(document).ready(function (){
    $(".role-update-submit").click(function (event){
        event.preventDefault();
        const roleSection = $(this).closest(".role")
        const roleId = roleSection.attr("user-role-id")
        const permissions = roleSection.find(".permissions:checked").map(function() {
            return $(this).val();
        }).get().map(Number);
        console.log(permissions)
        console.log(roleId)
        const newRole = {
            roleId : roleId,
            permissions : permissions
        }
        $.post("/api/Roles/UpdateRole", newRole).then(function (response){
            console.log(response)
        })
    })
})