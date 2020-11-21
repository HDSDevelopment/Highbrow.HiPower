$(document).ready(function () {
    //data tabes initialization
    $('table.display').DataTable();
    //menu active open code
    var menuOpen = $('#main-menu-wrapper li.open');
    $(menuOpen).closest('.collapse').addClass('show');
    $(menuOpen).closest('.collapse').parent('li').prev().attr('aria-expanded','true');
});