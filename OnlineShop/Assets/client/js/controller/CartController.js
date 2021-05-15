var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        //id bắt đầu bằng#, gọi off để clear dấu click
        //Định nghĩa cho Button tiếp tục mua hàng
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/Cart/Payment";
        });
        // Định nghĩa cho Button Update của từng sản phẩm
        $('#btnUpdate').off('click').on('click', function () {
            //lấy tất cả các dòng câu lệnh có class là txtSoLuong
            var listProduct = $('.txtSoLuong');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    SoLuong: $(this).val(),
                    SanPham: {
                        MaSach: $(item).data('id'),
                    }
                });
            });
            $.ajax({
                // gọi link thực hiện trong controller
                url: '/Cart/Update',
                // truyền dữ liệu vào link
                data: {
                    cartModel: JSON.stringify(cartList)
                },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart/Index";
                    }
                }
            })
        });
        //Định nghĩa cho Button Update giỏ hàng 
        $('#btnUpdateAll').off('click').on('click', function () {
            //lấy tất cả các dòng câu lệnh có class là txtSoLuong
            var listProduct = $('.txtSoLuong');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    SoLuong: $(this).val(),
                    SanPham: {
                        MaSach: $(item).data('id'),
                    }
                });
            });
            $.ajax({
                // gọi link thực hiện 
                url: '/Cart/Update',
                // truyền dữ liệu vào link
                data: {
                    cartModel: JSON.stringify(cartList)
                },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart/Index";
                    }
                }
            })
        });
        //Định nghĩa cho Button Delete toàn giỏ hàng
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart/Index";
                    }
                }
            });
        });
        //Dấu chấm là lấy class
        $('.Delete').off('click').on('click', function (e) {
            // Cái link # bên thẻ a sẽ k ảnh hưởng
            e.preventDefault();
            $.ajax({
                url: '/Cart/Delete',
                data: { id:$(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart/Index";
                    }
                }
            });
        });
        
    }
}
cart.init();