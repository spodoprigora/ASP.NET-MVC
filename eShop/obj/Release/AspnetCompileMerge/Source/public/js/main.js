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


    /////////////////////////
    // изменяем статус
    //$(".sel").change(function (id) {
    //    var message = $(this).val();
    //    alert(id + " " + message);
    //document.location.href = "Admin/";

    //});



   
  
    

});


   



