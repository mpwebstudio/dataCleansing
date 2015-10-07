$('form').keypress(function (e) {
    if (e.which == 13) {
        getResults();
        $('form#search_form').submit();
        return false;
    }
});

function getResults(apiNumber,type) {
    clear(type);
    if (type == 1) {
        var phoneNumber = $('#telNumb').val();
    }
    if (type == 2) {
        var phoneNumber = $('#telNumbT').val();
    }

    if(phoneNumber.length <= 10)
    {
        alert('Telephone number is too short!');
        return;
    }

    var url = '/TelephoneValidation/TelephoneValidation/';
    $.ajax({
        url: url,
        method: 'GET',
        dataType: 'json',
        data: { id: phoneNumber, id2: apiNumber },
        success: function (data) {
            $('#telResult').empty();
            document.getElementById('telResult').style.display = "block";
            if (Object.keys(data).length == 0) {
                if (type == 1) {
                    $('#telResult').append('<div class="form-group" id="ss"><span style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">Wrong telephone number!</span></div>')
                }
                else if (type == 2) {
                    $('#telResultT').append('<div class="form-group" id="ss"><span style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">Wrong telephone number!</span></div>')
                }
            }
            else {
                $.each(data, function (index, item) {
                    if (type == 1) {
                        if (item.IsValid == "Valid") {
                            document.getElementById('telPic').style.backgroundPosition = "38px 1px";
                        }
                        else if (item.IsValid == "Phone is invalid") {
                            document.getElementById('telPic').style.backgroundPosition = "67px 1px";
                        }
                    }
                    else if (type == 2) {
                        if (item.IsValid == "Valid") {
                            $('#telNumberT').effect("shake");
                            $('#telNumbT').css('background-color', '#DAF2B2');
                        }
                        else if (item.IsValid = 'Phone is invalid') {
                            $('#telNumberT').effect("shake");
                            $('#telNumbT').css('background-color', '#F45866');
                        }
                    }
                })
            }
        }
    })
}

function clear(type) {
    $('#telNumbT').css('background-color', '#FFF');
    $('#telNumb').css('background-color', '#FFF');
    if ($('#telNumbT').val() != null) {
        $('#ss').remove();
        
    }
    if ($('#telNumb').val() != null) {
        $('#telPic').css('background-position', '0 0');
        $('#ss').remove();
    }

    if (type == 1) {
        $('#telNumbT').val('');
    }
    else if (type == 2) {
        $('#telNumb').val('');
    }
}