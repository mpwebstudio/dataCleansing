function getResults(apiNumber) {
    var emailAddress = $('#emailAddr').val();
    var url = "/Email/EmailValidation/";
    $("#loading").css({ "display": "block" });
    $.ajax({
        url: url,
        dataType: 'json',
        data: { id: emailAddress, id2: apiNumber },
        success: function (data) {
            var tbl = $("#emailTable");
            $.each(data, function (index, item) {
                $("<tr><td class='col-lg-1'>" + item.Email + "</td><td class='col-lg-1'>" + item.IsValid + "</td><td class='col-lg-1 delRowBtn' style='cursor: pointer; cursor: hand;'>Delete</td></tr>").appendTo(tbl);
                $("#loading").css({ "display": "none" });
            })
        },
        error: function (data) {
            alert('error');
        }
    });

    $(document.body).delegate(".delRowBtn", "click", function () {
        $(this).closest("tr").remove();
    });

}

