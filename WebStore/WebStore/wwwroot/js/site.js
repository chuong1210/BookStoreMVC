// Write your Javascript code.

//kiểm tra số lượng khi thêm vào giỏ hàng
$('#add_to_cart_alert').hide();
$('#add_to_cart').submit(function () {
    if (!$('#quantity').val()) {
        $('#add_to_cart_alert').show();
        return false;
    }
    else {
        return true;
    }
});
// show password
$(function () {

    $("#showPass").change(function () {
        var checked = $(this).is(":checked");
        if (checked) {
            $("#Password").attr("type", "text");
        } else {
            $("#Password").attr("type", "password");
        }
    });

});
//Checkout
$('#Check_alert').hide();
$('#Checkout_submit').submit(function () {
    if (!$('#Checkout_name').val() || !$('#Checkout_email').val() ||
        !$('#Checkout_phone').val() || !$('#Checkout_address').val()) {
        $('#Check_alert').show();
        return false;
    }
    else {
        return true;
    }
});