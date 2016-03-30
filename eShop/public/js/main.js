$(document).ready(function(){
		
    $("#AddtoBasket").click(function () {
        alert('Товар добавлен в корзину');
    });
    
 //табы      
    $(function () {
        //$('ul.tabs li:first').addClass('active');
        //$('.block article').hide();
        //$('.block article:first').show();
        $('ul.tabs li').on('click', function () {
            $('ul.tabs li').removeClass('active');
            $(this).addClass('active');
            $('.block article').hide();
            var activeTab = $(this).find('a').attr('href');
            $(activeTab).show();
            return false;
        });
    });

    $('#close').on("click", function () {
        $('.characteristics').detach();
    });

    //календарь
    $('INPUT.datepicker').datepicker($.datepicker.regional["ru"]);
    $('#act').on("click", function () {
        $('#flag').attr('checked', 'checked');
    });
    

    $('#reset').on("click", function () {
        $('#flag').removeAttr('checked');
    });
});
