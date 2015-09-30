function Mouse() {
    var iev = 0;
    var ieold = (/MSIE (\d+\.\d+);/.test(navigator.userAgent));
    var trident = !!navigator.userAgent.match(/Trident\/7.0/);
    var rv = navigator.userAgent.indexOf("rv:11.0");
    var height = parseInt($('#top-overlay').css('margin-top'), 10);
    if (ieold) iev = new Number(RegExp.$1);
    if (navigator.appVersion.indexOf("MSIE 10") != -1) iev = 10;
    if (trident && rv != -1) iev = 11;
    // Firefox or IE 11
    if (typeof InstallTrigger !== 'undefined' || iev == 11) {
        var lastScrollTop = 0;
        $(window).on('scroll', function () {
            st = $(this).scrollTop();
            if (st < lastScrollTop) {
                if (height >= 500)
                    height = 480;
                $('#top-overlay').css('height', (height + 10) + 'px');
            }
            else if (st > lastScrollTop) {
                if (height <= 0)
                    height = 0;
                $('#top-overlay').css('height', (height - 10) + 'px');
            }
            lastScrollTop = st;
        });
    }
        // Other browsers
    else {
        $('#top-overlay').bind('mousewheel', function (e) {
            if (e.originalEvent.wheelDelta > 0) {
                if (height >= 500)
                    height = 480;
                $('#top-overlay').css('margin-top', (height + 10) + 'px');
            } else {
                $('#top-overlay').css('margin-top', (height - 10) + 'px');
            }
        });
    }
}

function mobileMenu() {
    $('nav#mobile').scotchPanel({
        containerSelector: 'body',
        direction: 'left',
        duration: 300,
        transition: 'ease',
        clickSelector: '.toggle-menu',
        distanceX: '30%',
        enableEscapeKey: true
    });
}

function subscribe() {
    var email = $('#subscribe_mail').val();
    var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    var Url = '/Home/Subscribe';
    
    if(re.test(email))
    {
        $.ajax({
            url: Url,
            dataType: 'json',
            data: { id: email },
            success: function (data) {
                if (data == 'good') {
                    $('#subscribe_mail').val('Thank you!');
                }
                else if (data == 'exist') {
                    $('#subscribe_mail').val('You are subscribed!');
                }
                else {
                    $('#subscribe_mail').val('Email is invalid!');
                }
            }
        })
    }
    else {
        $('#subscribe_mail').val('Email is invalid!');
    }
}

function price() {
    var inputNumber = $('#inputNumber').val();

    inputNumber = inputNumber * 12;

    var total = '';
    if (document.getElementById("myonoffswitch").checked) {
        if (inputNumber >= 1 && inputNumber <= 500) {
            total = '£25  per calendar year';
        }
        else if (inputNumber >= 501 && inputNumber <= 1200) {
            total = '£50  per calendar year';
        }
        else if (inputNumber >= 1201 && inputNumber <= 2600) {
            total = '£100  per calendar year';
        }
        else if (inputNumber >= 2601 && inputNumber <= 4300) {
            total = '£150  per calendar year';
        }
        else if (inputNumber >= 4301 && inputNumber <= 6900) {
            total = '£200 per calendar year';
        }
        else if (inputNumber >= 6901 && inputNumber <= 20000) {
            total = '£500 per calendar year';
        }
        else if(inputNumber >= 20001){
            total = 'Call us for special price!';
        }
        document.getElementById("totalPerYear").innerHTML = total;
    }
    else {
        if (inputNumber >= 1 && inputNumber <= 3000) {
            total = '£25  per calendar year';
        }
        else if (inputNumber >= 3001 && inputNumber <= 7200) {
            total = '£50  per calendar year';
        }
        else if (inputNumber >= 7201 && inputNumber <= 15600) {
            total = '£100  per calendar year';
        }
        else if (inputNumber >= 15601 && inputNumber <= 25800) {
            total = '£150  per calendar year';
        }
        else if (inputNumber >= 25801 && inputNumber <= 41400) {
            total = '£200 per calendar year';
        }
        else if (inputNumber >= 41401 && inputNumber <= 126000) {
            total = '£500 per calendar year';
        }
        else if(inputNumber >= 126001){
            total = 'Call us for special price!';
        }
        document.getElementById("totalPerYear").innerHTML = total;
    }
}

function bulk() {
    document.getElementById("single").style.display = "none";
    document.getElementById("bulk").style.display = "block";
}

function lookUp() {
    document.getElementById("single").style.display = "block";
    document.getElementById("bulk").style.display = "none";
}