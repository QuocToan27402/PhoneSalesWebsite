$(document).ready(function () {
    $("#DATHANG").click(function () {
        if ($("#district").val() != "" && $("#province").val() != "" &&
            $("#ward").val() != "") {
            var address = $("#street") + ", " + $("#ward option:selected").text() +
                ", " + $("#district option:selected").text() + ", " +
                $("#province option:selected").text();
        }
        var name = $("#Name").val();
        var phone = $("#PhoneNumber").val();
        $.ajax({
            type: 'POST',
            url: '/Cart/Order',
            data: {
                name: name,
                phonenumber: phone,
                address: address
            },
            success: function (result) {
                // Xử lý kết quả trả về từ controller
                console.log(result);
            },
            error: function (error) {
                // Xử lý lỗi nếu có
                console.log(error);
            }
        });
    });
})