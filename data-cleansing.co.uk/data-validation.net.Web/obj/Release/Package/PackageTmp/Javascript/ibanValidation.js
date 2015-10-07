$('form').keypress(function (e) {
    if (e.which == 13) {
        getResults();
        $('form#search_form').submit();
        return false;
    }
});

function getResults(apiNumber) {
  
        var ibanNumber = $('#ibanNumb').val();
        if (ibanNumber.length < 15)
        {
            alert("Iban is too short");
            return;
        }

        var url = '/IbanValidation/IbanValidation/';
        $.ajax({
            url: url,
            method: 'GET',
            dataType: 'json',
            data: { id: ibanNumber, id2: apiNumber },
            success: function (data) {
                $("#ibanResult").empty();
                document.getElementById("ibanNumber").style.display = "block";
                document.getElementById("ibanResult").style.display = "block";
                if (Object.keys(data).length == 0) {

                    $("#ibanResult").append('<div class="form-group" id="ss"><span style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">Wrong IBAN number!</span></div>');
                }
                else {
                    $("#ibanResult").append('<table class="table table-striped table-hover"><thead><tr><th class="col-lg-7">Bank Name</th><th class="col-lg-5">City</th><th class="col-lg-5">Country</th><th class="col-lg-2">IsoCode</th></tr></thead><tbody id="bankForm"></tbody></table>');
                    $.each(data, function (index, item) {
                        if (item.BankName === undefined) {

                            $("#ibanResult").append('<div class="form-group" id="ss"><span style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">Wrong IBAN number!</span></div>');
                            return;
                        }
                        $("#bankForm").append('<tr onclick="showDetails(this)" style="cursor: pointer;" id="' + item.BankName + '" data-bank-type="' + item.BankName + ',' + item.BranchName + ',' + item.BranchAddress + ',' + item.City + ',' + item.Telephone + ',' + item.Fax + ',' + item.Country + ',' + item.IsoCode + ',' + item.Swift + ',' + item.Postcode + ',' + item.BankCode + ',' + item.BranchCode + '"><td>' + item.BankName + '</td><td>' + item.City + '</td><td>' + item.Country + '</td><td>' + item.IsoCode + '</td><td>Details</td></tr>');

                    });
                }
            }
        })
     
}

function getResult(apiNumber) {
    clearTextarea();
    var ibanNumber = $('#ibanNumbT').val();
    if (ibanNumber.length < 15)
    {
        $("#ibanResulta").append('<div class="form-group" id="ss"><span style="font-size: 1.2e; font-weigh: 600; color:red" id = "bank">IBAN is too short!</span></div>');
        return;
    }
    var url = '/BankValidation/CheckIban/';
    $.ajax({
        url: url,
        dataType: 'json',
        data: { id: ibanNumber, id2: apiNumber },
        success: function (data) {
            $("#myselecta").empty();
            document.getElementById("myselecta").style.display = "inline-block";
            
            $.each(data, function (index, item) {
                if (item.City == 'null')
                {
                    item.City == '';
                }
                $("#myselecta").append("<option value='"  + item.BankName + "," + item.BranchName + "," + item.BranchAddress + "," + item.City + "," + item.Telephone + "," + item.Fax + "," + item.Country + "," + item.IsoCode + "," + item.Swift + "," + item.Postcode + "," + item.BankCode + "," + item.BranchCode + "'>" + item.BankName + " " + item.City + "</option>");
            });
        }
    });

    function clearTextarea(){
        document.getElementById("bankD").style.display = "none";
        $("#bank").empty();
        document.getElementById("branchD").style.display = "none";
        $("#branch").empty();
        document.getElementById("addressD").style.display = "none";
        $("#address2").empty();
        document.getElementById("cityD").style.display = "none";
        $("#city").empty();
        document.getElementById("telephoneD").style.display = "none";
        $("#telephone").empty();
        document.getElementById("faxD").style.display = "none";
        $("#fax").empty();
        document.getElementById("countryD").style.display = "none";
        $("#country").empty();
        document.getElementById("isoCodeD").style.display = "none";
        $("#isoCode").empty();
        document.getElementById("swiftD").style.display = "none";
        $("#swift").empty();
        document.getElementById("postcodeD").style.display = "none";
        $("#postcode").empty();
        document.getElementById("bankCodeD").style.display = "none";
        $("#bankCode").empty();
        document.getElementById("branchCodeD").style.display = "none";
        $("#branchCode").empty();
        document.getElementById("isValidD").style.display = "none";
        $("#isValid").empty();
    }
}

function selectOnChangea() {
    var e = document.getElementById("myselecta");
    var str = e.options[e.selectedIndex].value;
    var res = str.split(",");
    document.getElementById("bankD").style.display = "block";
    document.getElementById("bank").value = res[0];
    if (res[1] != 'undifined') {
        if (res[1] != 'null' && res[1] != '') {
            document.getElementById("branchD").style.display = 'block';
            document.getElementById('branch').value = res[1];
        }
    }
    if (res[2] != 'undifined') {
        if (res[2] != 'null' && res[2] != '') {
            document.getElementById("addressD").style.display = 'block';
            document.getElementById('address2').value = res[2];
        }
    }
    if (res[3] != 'undifined') {
        if (res[3] != 'null' && res[3] != '') {
            document.getElementById("cityD").style.display = 'block';
            document.getElementById('city').value = res[3];
        }
    }
    if (res[4] != 'undifined') {
        if (res[4] != 'null' && res[4] != '') {
            document.getElementById("telephoneD").style.display = 'block';
            document.getElementById('telephone').value = res[4];
        }
    }
    if (res[5] != 'undifined') {
        if (res[5] != 'null' && res[5] != '') {
            document.getElementById("faxD").style.display = 'block';
            document.getElementById('fax').value = res[5];
        }
    }
    if (res[6] != 'undifined') {
        if (res[6] != 'null' && res[6] != '') {
            document.getElementById("countryD").style.display = 'block';
            document.getElementById('country').value = res[6];
        }
    }
    if (res[7] != 'undifined') {
        if (res[7] != 'null' && res[7] != '') {
            document.getElementById("isoCodeD").style.display = 'block';
            document.getElementById('isoCode').value = res[7];
        }
    }
    if (res[8] != 'undifined') {
        if (res[8] != 'null' && res[8] != '') {
            document.getElementById("swiftD").style.display = 'block';
            document.getElementById('swift').value = res[8];
        }
    }
    if (res[9] != 'undifined') {
        if (res[9] != 'null' && res[9] != '') {
            document.getElementById("postcodeD").style.display = 'block';
            document.getElementById('postcode').value = res[9];
        }
    }
    if (res[10] != 'undifined') {
        if (res[10] != 'null' && res[10] != '') {
            document.getElementById("bankCodeD").style.display = 'block';
            document.getElementById('bankCode').value = res[10];
        }
    }
    if (res[11] != 'undifined') {
        if (res[11] != 'null' && res[11] !='') {
            document.getElementById("branchCodeD").style.display = 'block';
            document.getElementById('branchCode').value = res[11];
        }
    }
    document.getElementById("isValidD").style.display = "block";
    document.getElementById("isValid").value = 'The IBAN checksum,bank code and length are correct.';
}


function showDetails(bank) {

    var bankDetails = bank.getAttribute("data-bank-type").split(",");
    
    alert('Bank: ' + bankDetails[0] + '\n' + 'Branch: ' + bankDetails[1] + '\n' + 'Address: ' + bankDetails[2] + '\n' + 'City: ' + bankDetails[3] + '\n' + 'Post Code: ' + bankDetails[9]  + '\n' + 'Country: ' + bankDetails[6] + '\n' + 'Telephone: ' + bankDetails[4] + '\n' + 'Fax: ' + bankDetails[5] + '\n' + 'Country Code:' + bankDetails[7] + '\n' + 'Swift:' + bankDetails[8] + '\n' + 'Bank Code: ' + bankDetails[10] + '\n' + 'Branch Identi Code: ' + bankDetails[11] + '\n');
}

