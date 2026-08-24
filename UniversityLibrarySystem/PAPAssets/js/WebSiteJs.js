function sendToServer() {
    var id = $('#txtId').val();
    var point = $('#food_quality').val();
    var title = $('#txtTitle').val();
    var description = $('#txtDesc').val();

    $.ajax({
        type: "Post",
        url: "LeaveComment.aspx/sendToServer",
        data: "{'id':'" + id + "','point':'" + point + "','title':'" + title + "','description':'" + description + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (response) {
            var result = response.d;
            if (result == "success") {
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 4000,
                    didOpen: (toast) => {
                        toast.addEventListener('mouseenter', Swal.stopTimer)
                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                    }
                })

                Toast.fire({
                    icon: 'success',
                    title: 'نظر شما با موفقیت ثبت شده است با تشکر از همکاری شما'
                })
            }

            if (result == "successf") {
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000,
                    didOpen: (toast) => {
                        toast.addEventListener('mouseenter', Swal.stopTimer)
                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                    }
                })

                Toast.fire({
                    icon: 'error',
                    title: 'مشکلی در اجرای درخواست شما به وجود آمده است لطفا مجدد تلاش نمایید'
                })
            }
        },

    });
}

//--------------------------------------------------------------

function AddToCart(x) {
    var id = x;

    $.ajax({
        type: "Post",
        url: "PartnerDetail.aspx/AddToCart",
        data: "{'id':'" + x + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (response) {
            var result = response.d;
            if (result == "success") {
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 2000,
                    didOpen: (toast) => {
                        toast.addEventListener('mouseenter', Swal.stopTimer)
                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                    }
                })

                Toast.fire({
                    icon: 'success',
                    title: 'به سبد خرید اضافه شد'
                })
            }

            if (result == "successf") {
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 2000,
                    didOpen: (toast) => {
                        toast.addEventListener('mouseenter', Swal.stopTimer)
                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                    }
                })

                Toast.fire({
                    icon: 'error',
                    title: 'کالای انتخابی ناموجود است'
                })
            }
        },

    });
}