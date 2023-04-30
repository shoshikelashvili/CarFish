$('#carty').click(function (e)
{
    console.log('test1')
    e.preventDefault();
    $("#sidemenu").toggleClass("on");
});

$('#closeslide').click(function (e) {
    console.log('test2');
    e.preventDefault();
    $("#sidemenu").toggleClass("on");
});