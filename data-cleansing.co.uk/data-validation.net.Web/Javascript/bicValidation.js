$('form').keypress(function (e) {
    if (e.which == 13) {
        getResults();
        $('form#search_form').submit();
        return false;
    }
});

function getResults(apiNumber) {
    var bicNumber = $('#bicNumb').val();
    var url = "/BicValidation/CheckBic";
    if (bicNumber.length == 8 || bicNumber.length == 11) { 
       $.ajax({
            url: url,
            dataType: 'json',
            data: { id: bicNumber, id2: apiNumber },
            success: function (data) {
                $("#bicResult").empty()
                document.getElementById("bicNumber").style.display = "block";
                document.getElementById("bicResult").style.display = "block";
                if (Object.keys(data).length == 0) {
                    $("#bicResult").append('<div class="form-group" id="ss"><span style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">Wrong Swift / BIC code  </span></div>');
                }
                else {
                    $('#bicResult').append('<table class="table table-striped table-hover"><thead><tr><th class="col-lg-7">Bank Name</th><th class="col-lg-5">Branch</th><th class="col-lg-5">City</th><th class="col-lg-5">Country</th><th class="col-lg-2">SWIFT</th></tr></thead><tbody id="bankForm"></tbody></table>');
                    $.each(data, function (index, item) {
                        $("#bankForm").append('<tr><td>' + item.BankOrInstitution + '</td><td>' + item.Branch + '</td><td>' + item.City + '</td><td>' + item.Country + '</td><td>' + item.SwiftCode + '</td><tr>');
                    });
                }
            }
        })
    }
    else {
        $("#bicResult").append('<div class="form-group" id="ss"><span style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">Swift / BIC code must be 8 or 11 letters long</span></div>');
    }
}

function getResult(apiNumber) {
    clearTextarea();
    var bicNumber = $('#numbT').val();
    if(bicNumber.length == 8 || bicNumber.length == 11 )
    {
        var url =  "/BicValidation/CheckBic";
        $.ajax({
            url: url,
            dataType: 'json',
            data: { id: bicNumber, id2: apiNumber },
            success: function (data) {
                $.each(data, function (index, item) {
                    document.getElementById('bankD').style.display = "block";
                    document.getElementById('bank').value = item.BankOrInstitution;
                    if (item.Branch != null) {
                        document.getElementById('branchD').style.display = "block";
                        document.getElementById('branch').value = item.Branch;
                    }
                    document.getElementById('cityD').style.display = "block";
                    document.getElementById('city').value = item.City;
                    document.getElementById('countryD').style.display = "block";
                    document.getElementById('country').value = item.Country;
                    document.getElementById('swiftD').style.display = "block";
                    document.getElementById('swift').value = item.SwiftCode;
                })
            }
        })
    }
    else
    {
        document.getElementById('bicResult').style.display = 'block';
        $('#bicResult').append('<div class="form-group" id="ss"><span style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">Bic / Swift is too short!</span></div>');
    }

    function clearTextarea() {
        document.getElementById('bankD').style.display = "none";
        $('#bank').empty();
        document.getElementById('branchD').style.display = "none";
        $('#branch').empty();
        document.getElementById('cityD').style.display = "none";
        $('#city').empty();
        document.getElementById('countryD').style.display = "none";
        $('#country').empty();
        document.getElementById('swiftD').style.display = "none";
        $('#swift').empty();
    }
}

