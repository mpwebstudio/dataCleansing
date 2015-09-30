$('form').keypress(function (e) {
    if (e.which == 13) {
        getResults();
        $('form#search_form').submit();
        return false;
    }
});

function getResults(apiNumber, type) {
    clear();
    if(type == 1)
        var cardNumber = $('#cardNumb').val();
    if (type == 2)
        var cardNumber = $('#cardNumbT').val();
    if(cardNumber.length < 12)
    {
        alert('Card number is too short');
        return;
    }

    var url = '/CardValidation/CardValidation/';
    $.ajax({
        url: url,
        method: 'GET',
        dataType: 'json',
        data: { id: cardNumber, id2: apiNumber },
        success: function (data) {
            $('#cardResult').empty();
            document.getElementById('cardResult').style.display = "block";
            if (Object.keys(data).length == 0) {
                if(type == 1)
                    $('#cardResult').append('<div class="form-group" id="ss"><span style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">Wrong card number!</span></div>');
                else if(type == 2)
                    $('#cardResults').append('<div class="form-group" id="ss"><span style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">Wrong card number!</span></div>');
            }
            else {
                $.each(data, function (index, item) {
                    if (type == 1) {
                        if (item.IsValid === "Card is valid") {
                            if (item.CardIssue == "Visa") {
                                document.getElementById('cardPic').style.backgroundPosition = "0 258px";
                            }
                            else if (item.CardIssue == "MasterCard") {
                                document.getElementById('cardPic').style.backgroundPosition = "0 227px";
                            }
                            else if (item.CardIssue == "Maestro") {
                                document.getElementById('cardPic').style.backgroundPosition = "0 196px";
                            }
                            else if (item.CardIssue == "Visa Electron") {
                                document.getElementById('cardPic').style.backgroundPosition = "0 165px";
                            }
                            else if (item.CardIssue == "Ammerican Express") {
                                document.getElementById('cardPic').style.backgroundPosition = "0 134px";
                            }
                            else if (item.CardIssue == "Diners Club Internationa") {
                                document.getElementById('cardPic').style.backgroundPosition = "0 104px";
                            }
                            else if (item.CardIssue == "Discover") {
                                document.getElementById('cardPic').style.backgroundPosition = "0 72px";
                            }
                            else if (item.CardIssue == "JCB") {
                                document.getElementById('cardPic').style.backgroundPosition = "0 42px";
                            }

                            $('#cardNumber').append('<span id="status" style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">' + item.IsValid + '</span>');
                        }
                        else {
                            $('#cardNumber').append('<span id="status" style="font-size: 1.2e; font-weigh: 600; color:red;" id = "bank">Wrong card number!</span>');
                        }
                    }
                    else if (type == 2) {

                        if (item.IsValid === "Card is valid") {
                            if (item.CardIssue == "Visa") {
                                document.getElementById('visa').style.opacity = "1";
                            }
                            else if (item.CardIssue == "MasterCard") {
                                document.getElementById('mastercard').style.opacity = "1";
                            }
                            else if (item.CardIssue == "Maestro") {
                                document.getElementById('maestro').style.opacity = "1";
                            }
                            else if (item.CardIssue == "Visa Electron") {
                                document.getElementById('visaE').style.opacity = "1";
                            }
                            else if (item.CardIssue == "Ammerican Express") {
                                document.getElementById('amex').style.opacity = "1";
                            }
                            else if (item.CardIssue == "Diners Club Internationa") {
                                document.getElementById('diners').style.opacity = "1";
                            }
                            else if (item.CardIssue == "Discover") {
                                document.getElementById('discover').style.backgroundPosition = "1";
                            }
                            else if (item.CardIssue == "JCB") {
                                document.getElementById('jcb').style.opacity = "1";
                            }

                            document.getElementById('cardNumbT').style.backgroundColor = "#DAF2B2";
                        }
                        else {
                            document.getElementById('cardNumbT').style.backgroundColor = "#F45866";
                        }
                    }
                    
                })
            }
        }
    })
}

function clear() {
    document.getElementById('cardPic').style.backgroundPosition = "0 0";
    document.getElementById('visa').style.opacity = "0.2";
    document.getElementById('mastercard').style.opacity = "0.2";
    document.getElementById('maestro').style.opacity = "0.2";
    document.getElementById('visaE').style.opacity = "0.2";
    document.getElementById('amex').style.opacity = "0.2";
    document.getElementById('diners').style.opacity = "0.2";
    document.getElementById('discover').style.backgroundPosition = "0.2";
    document.getElementById('jcb').style.opacity = "0.2";
    document.getElementById('cardNumbT').style.backgroundColor = "#FFF";
    if ($('#status').val() != null) {
        $('#status').remove();
    }
}