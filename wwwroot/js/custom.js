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

    $('#dependent_details_fields_blueprint').click(function () {
        console.log('a');
        var listCount = parseInt($('#tab4 .fields:last-child').attr('count')) + 1;
        listCount = parseInt(listCount);
        console.log(listCount);
        $("#tab4 .dependent-row").append(
            '<div class="fields_add fields" count='+listCount+'>'+
            '<input type="hidden" value="false" id="employee_dependent_details_attributes_0__destroy">'+
            '<a title="Remove Dependent" data-confirm="Are you sure want to delete?" class="remove_nested_fields" data-association="dependent_details" href="javascript:void(0)">'+
                '<i class="fa fa-times-circle"></i>'+
            '</a>' +
            '<input data-val="true" data-val-required="The Id field is required." id="Dependents_' + listCount + '__Id" name="Dependents['+listCount+'].Id" type="hidden" value="0">'+
            '<div class="form-row">'+
                '<div class="col-md-4 mb-3">'+
                    '<label class="form-label" for="dependent.DependentName">Dependent Name</label>'+
            '<input class="form-control text-box single-line" id="Dependents_' + listCount + '__DependentName" name="Dependents[' + listCount +'].DependentName" placeholder="Enter dependent name" type="text">'+
							'</div>'+
                    '<div class="col-md-4 mb-3">'+
                        '<label class="form-label" for="dependent.Relation">Relation</label>'+
            '<select class="form-control" data-val="true" data-val-required="The Relation field is required." id="Dependents_' + listCount + '__Relation" name="Dependents[' + listCount +'].Relation">'+
                            '<option value="1">Father</option>'+
                            '<option value="2">Mother</option>'+
                           ' <option value="3">Spouse</option>'+
                           ' <option value="4">Son</option>'+
                            '<option value="5">Daughter</option>'+
                        '</select>'+
                    '</div>'+
                    '<div class="col-md-4 mb-3">'+
                        '<label class="form-label" for="dependent.DateOfBirth">Date of Birth</label>'+
            '<input class="form-control datepicker text-box single-line" data-val="true" data-val-required="The DateOfBirth field is required." id="Dependents_' + listCount + '__DateOfBirth" name="Dependents[' + listCount +'].DateOfBirth" placeholder="dd-mm-yyyy" type="text">'+
							'</div>'+
                    '</div>'+
                '</div>'
            //'<div class="fields_add fields" count=' + listCount + '>' +
            //'<input type="hidden" value="false" id="employee_dependent_details_attributes_0__destroy">' +
            //'<a title="Remove Dependent" data-confirm="Are you sure want to delete?" class="remove_nested_fields" data-association="dependent_details" href="javascript:void(0)">' +
            //'<i class="fa fa-times-circle"></i>' +
            //'</a>' +
            //'<input data-val="true" data-val-required="The Id field is required." id="dep.Id" name="dep.Id" type="hidden" value="0">' +
            //'<div class="form-row">' +
            //'<div class="col-md-4 mb-3">' +
            //'<label class="form-label" for="dependent.DependentName">Dependent Name</label>' +
            //'<input class="form-control text-box single-line" id="dep.DependentName" name="dep.DependentName" placeholder="Enter dependent name" type="text">' +
            //'</div>' +
            //'<div class="col-md-4 mb-3">' +
            //'<label class="form-label" for="dependent.Relation">Relation</label>' +
            //'<select class="form-control" data-val="true" data-val-required="The Relation field is required." id="dep.Relation" name="dep.Relation">' +
            //'<option value="1">Father</option>' +
            //'<option value="2">Mother</option>' +
            //' <option value="3">Spouse</option>' +
            //' <option value="4">Son</option>' +
            //'<option value="5">Daughter</option>' +
            //'</select>' +
            //'</div>' +
            //'<div class="col-md-4 mb-3">' +
            //'<label class="form-label" for="dependent.DateOfBirth">Date of Birth</label>' +
            //'<input class="form-control datepicker text-box single-line" data-val="true" data-val-required="The DateOfBirth field is required." id="dep.DateOfBirth" name="dep.DateOfBirth" placeholder="dd-mm-yyyy" type="text">' +
            //'</div>' +
            //'</div>' +
            //'</div>'
        );
        $(".datepicker").datepicker();
    });

});
