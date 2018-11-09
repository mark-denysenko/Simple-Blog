$(function () {
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.chatHub;

    // Create a function that the hub can call back to display messages.
    chat.client.addMessage = function (name, message) {
        // Add the message to the page.
        $('#discussion').append('<li><strong>' + htmlEncode(name)
            + '</strong>: ' + htmlEncode(message) + '</li>');
    };

    chat.client.onConnected = function (id, userName, allUsers) {

        // setting id
        $('#hdId').val(id);

        // Adding all users in list
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Nickname);
        }
    }

    // Adding new user
    chat.client.onNewUserConnected = function (id, name) {

        AddUser(id, name);
    }

    // Deleting user from list
    chat.client.onUserDisconnected = function (id, userName) {

        $('#' + id).remove();
    }

    // Set initial focus to message input box.
    $('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(function () {
        // connecting user
        chat.server.connect($('#displayname').attr('value'));

        $('#sendmessage').click(function () {
            // Call the Send method on the hub.
            chat.server.send($('#displayname').val(), $('#message').val());
            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });
    });
});
// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
};

function AddUser(id, name) {
    $("#user-list").append('<li id="' + id + '"><b>' + name + '</b></li>');
}