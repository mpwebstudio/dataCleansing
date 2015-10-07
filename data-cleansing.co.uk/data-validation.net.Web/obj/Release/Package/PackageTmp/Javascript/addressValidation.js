function getResults(apiNumber) {
    clearAll();
    var postCode = $('#postCode').val();
    var url = "/AddressValidation/GetAddress/";
    $("#loading").css({ "display": "block" });
        $.ajax({
            url: url,
            dataType: 'json',
            data: { id: postCode,id2:apiNumber },
            success: function (data) {
                $("#myselect").empty();
                document.getElementById("myselect").style.display = "inline-block";
                $("#myselect").append('<option value="blank">Choise address</option>');
                $.each(data, function (index, item) {
                    $("#myselect").append("<option value='" + item.Id + "," + item.PostCode + "," + item.Street + "," + item.BuildingName + "," + item.BuildingNumber + "," + item.Flat + "," + item.TraditionalCounty + "," + item.AdministrativeCounty + "," + item.OrganisationName + "," + item.Locality + "," + item.City + "'>" + item.Flat + " " + item.BuildingNumber + " " + item.BuildingName + " " + item.Street + ", " + item.PostCode + "</option>");
                    $("#loading").css({ "display": "none" });
                });
            },
            error: (function (data) {
                alert('error');
            })
        });
    
    function clearAll() {
        document.getElementById("add").style.display = "none";
        $("#address1").empty();
        document.getElementById("add2").style.display = "none";
        $("#address2").empty();
        document.getElementById("add3").style.display = "none";
        $("#address3").empty();
        document.getElementById("str").style.display = "none";
        $("#street").empty();
        document.getElementById("city").style.display = "none";
        $("#town").empty();
        document.getElementById("coun").style.display = "none";
        $("#county").empty();
    }
}

function selectOnChange()
{
    var e = document.getElementById("myselect");
    var str = e.options[e.selectedIndex].value;
    var res = str.split(",");

    if (res[5] != 'undefined') {
        if (res[5] != '') {
            document.getElementById("add").style.display = "block";
            document.getElementById("address1").value = res[5];
        }
    }
    if (res[4] != 'undefined' && res[4] != '') {
        document.getElementById("add2").style.display = "block";
        document.getElementById("address2").value = res[4];
    }
    if (res[3] != 'undefined' && res[3] != '') {
        document.getElementById("add3").style.display = "block";
            document.getElementById("address3").value = res[3];
        }
    document.getElementById("street").value = res[2];
    document.getElementById("str").style.display = "block";
    document.getElementById("town").value = res[9];
    document.getElementById("city").style.display = "block";
    if (res[10] != 'undefined' && res[10] != '') {
        document.getElementById("county").value = res[10];
        document.getElementById("coun").style.display = "block";
    }
    document.getElementById("postCode").value = res[1];
    document.getElementById("myselect").style.display = "none";

}

//Textarea example
function getResultsa(apiNumber) {
    clearAlla();
    var postCode = $('#postCodea').val();
    var Url = "/AddressValidation/GetAddress/";
    $("#loading2").css({ "display": "block" });
        $.ajax({
            url: Url,
            dataType: 'json',
            data: { id: postCode, id2: apiNumber },
            success: function (data) {
                $("#myselecta").empty();
                document.getElementById("myselecta").style.display = "inline-block";
                $("#myselecta").append('<option value="blank">Chouse address</option>');
                $.each(data, function (index, item) {
                    $("#myselecta").append("<option value='" + item.Id + "," + item.PostCode + "," + item.Street + "," + item.BuildingName + "," + item.BuildingNumber + "," + item.Flat + "," + item.TraditionalCounty + "," + item.AdministrativeCounty + "," + item.OrganisationName + "," + item.Locality + "," + item.City + "'>" + item.Flat + " " + item.BuildingNumber + " " + item.BuildingName + " " + item.Street + ", " + item.PostCode + "</option>");
                    $("#loading2").css({ "display": "none" });
                });
            }
        });
    
    function clearAlla() {
        document.getElementById("adda").style.display = "none";
        $("#addressa").empty();
        document.getElementById("add2a").style.display = "none";
        $("#address2a").empty();
        document.getElementById("add3a").style.display = "none";
        $("#address3a").empty();
        document.getElementById("stra").style.display = "none";
        $("#streeta").empty();
        document.getElementById("citya").style.display = "none";
        $("#towna").empty();
        document.getElementById("couna").style.display = "none";
        $("#countya").empty();
    }
}

function selectOnChangea() {
    var e = document.getElementById("myselecta");
    var str = e.options[e.selectedIndex].value;
    var res = str.split(",");
    if (res[5] != 'undefined') {
        if (res[5] != '') {
            document.getElementById("adda").style.display = "block";
            document.getElementById("addressa").value = res[5];
        }
    }
    if (res[4] != 'undefined') {
        if (res[4] != '') {
            document.getElementById("add2a").style.display = "block";
            document.getElementById("address2a").value = res[4];
        }
    }
    if (res[3] != 'undefined'){
        if (res[3] != '') {
            document.getElementById("add3a").style.display = "block";
            document.getElementById("address3a").value = res[3];
        }
    }
    document.getElementById("streeta").value = res[2];
    document.getElementById("stra").style.display = "block";
    document.getElementById("towna").value = res[9];
    document.getElementById("citya").style.display = "block";
    if (res[10] != 'undefined' && res[10] != '') {
        document.getElementById("countya").value = res[10];
        document.getElementById("couna").style.display = "block";
    }
    document.getElementById("postCodea").value = res[1];
    document.getElementById("myselecta").style.display = "none";
}