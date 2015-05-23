$(function () {

    var auctionsHub = $.connection.auctionsHub;

    auctionsHub.client.test = function () {
        alert("test");
        console.log("Called test");
    };

    auctionsHub.client.newAuction = function (id) {
        alert("newAuction" + id);
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        alert("connected");
        console.log(auctionsHub.client);
        console.log($.connection.hub);
    });

    $("#testSignalR").click(function() {
        alert("Hallo");
        auctionsHub.server.testNotify();
    });

});