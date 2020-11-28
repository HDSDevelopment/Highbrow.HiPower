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

    //employee form section
    $('.employee-tabs .continue').click(function () {
        var tabNav = $('.employee-tabs .nav-tabs .active').closest('li').next('li').find('a');

        $(tabNav).addClass('active show');
        var tabShowId = $(tabNav).attr('href');
        $(tabShowId).addClass('active show');
        
        var closeTabId = $(this).attr('tab-name');
        //console.log(closeTabId)
        $("a[href='#"+closeTabId+"']").removeClass('active show');
        $('#' + closeTabId).removeClass('show active');
        //console.log($('.employee-tabs .nav-tabs .active').closest('li').next('li').find('a').html())
        $("html, body").animate({ scrollTop: 0 }, "slow");
    });
    //$('.employee-tabs .back').click(function () {
    //    var tabNav = $('.employee-tabs .nav-tabs .active').closest('li').prev('li').find('a');
    //    $(tabNav).trigger('click');
    //    var tabShowId = $(tabNav).attr('href');
    //    $(tabShowId).tab('show');
    //});
});
