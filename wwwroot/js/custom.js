$(document).ready(function () {
    //data tabes initialization
    $('table.display').DataTable();

    //menu active open code
    var menuOpen = $('#main-menu-wrapper li.open');
    $(menuOpen).closest('.collapse').addClass('show');
    $(menuOpen).closest('.collapse').parent('li').prev().attr('aria-expanded', 'true');

    //WHF check box on ready
    if ($('#IsUnlimited').is(':checked')) {
        $('#DaysPerMonth_section').css("display","none");
    } else {
        $('#DaysPerMonth_section').css("display", "block");
    }
    $(document).on('change', '#IsUnlimited', function () {
        var checkBox = document.getElementById("IsUnlimited");
        var text = document.getElementById("DaysPerMonth_section");
        if (checkBox.checked == true) {
            text.style.display = "none";
        } else {
            text.style.display = "block";
        }
    });
});
