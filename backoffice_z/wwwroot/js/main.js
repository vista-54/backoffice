$(document).ready(function(){
    $('.open-block').on('click', function(){
        $(this).toggleClass('active');
        $(this).next().slideToggle();
    });

    $('.open-form').on('click', function(){
        if($(this).prop("checked")) {
            $(this).closest('.form-item-title').next('.hidden-form').slideDown();
        }
        else {
            $(this).closest('.form-item-title').next('.hidden-form').slideUp();
        }
    });

    $('.logout-btn').on('click', function(){
        $('.user-options-block').stop().slideToggle();
    });
});