﻿$(document).ready(function () {

    ShowText();
    topheadcart();
    cartitem();
    GetWishListItems();
    StockActiveColor();
    headertext();
    Topheadertext();

    CountDownTimer();


    var Currency;
    var Tags;
    var ShippingCharges;
    var Color;
    var Category;



    $(".addItemLS").click(function () {
        cartitem();
        topheadcart();
    });

});
//setting
function headertext() {

    $.ajax({
        type: "GET",
        url: '/Home/GetSetting',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {


            if (data.Facebook != 1 || data.Facebook == null) {
                $('#facebook').addClass('d-none');
            }
            else {
                $("#facebook a").attr("href", data.FacebookUrl);
            }
            if (data.Instagram != 1 || data.Instagram == null) {
                $('#instagram').addClass('d-none');
            }
            else {
                $("#instagram a").attr("href", data.InstagramUrl);
            }
            if (data.Twitter != 1 || data.Twitter == null) {
                $('#twitter').addClass('d-none');
            }
            else {
                $("#twitter a").attr("href", data.TwitterUrl);
            }
            if (data.ShopUrl == null || data.ShopUrl == "") {
                $('.Shop-Now-Button').attr("href", "/shop/shop?MinPrice=RS0&MaxPrice=RS50000&SortID=0");
            }
            else {
                $('.Shop-Now-Button').attr("href", data.ShopUrl);
            }

            var lstNotification = data.NotificationsList;
            var index = Math.floor(Math.random() * lstNotification.length);
            var _notificationls = sessionStorage.getItem("_notification");
            _notificationls = _notificationls == null ? '' : _notificationls;
            if (_notificationls == '' && lstNotification.length > 0) {
                $('#staticBackdrop').modal('show');
                sessionStorage.setItem("_notification", "true");


                $('#modalTitle').html(lstNotification[index].Title);
                $('#modalDescription').html(lstNotification[index].Description);
                $('#modalButton').attr('href', lstNotification[index].ButtonURL);
                if (lstNotification[index].Image == '' || lstNotification[index].Image == null) {
                    $('#modalImg').addClass("hide");
                }
                $('#modalImg').attr('src', "https://retail.premium-pos.com/" + lstNotification[index].Image)
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            //alert(xhr, textStatus, errorThrown);
        }
    });
}


function Topheadertext() {

    $.ajax({
        type: "GET",
        url: '/Home/GetSetting',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#TopHeadDescription').html(data.TopHeadDescription);

        },
        error: function (xhr, textStatus, errorThrown) {
            //alert(xhr, textStatus, errorThrown);
        }
    });
}
//Gift
var arrGift = [];
function addgift() {

    var getgiftLSarr = getgiftLS();
    if (getgiftLSarr != null) {
        for (var i = 0; i < arrGift.length; i++) {
            getgiftLSarr.push({
                ItemID: arrGift[i].ItemID,
                GiftID: arrGift[i].GiftID,
                Title: arrGift[i].Title,
                Image: arrGift[i].Image,
                DisplayPrice: arrGift[i].DisplayPrice,
                DiscountedPrice: arrGift[i].DiscountedPrice,
                Key: arrGift[i].Key,
                ItemKey: parseInt($('#hdnItemKey').val())

            });

            setgiftLS(getgiftLSarr);
        }
    } else { setgiftLS(arrGift); }
    $('#gift').modal('toggle');

}
function setgiftLS(arr) {
    var getgiftItem = localStorage.getItem("_giftitems");
    if (getgiftItem != null) {
        localStorage.setItem("_giftitems", JSON.stringify(arr));
    }
}
function getgiftLS() {
    var getgiftItem = localStorage.getItem("_giftitems");
    if (getgiftItem != null && getgiftItem != "")
        return JSON.parse(getgiftItem);
    else
        return JSON.parse("[]");
}
function addGiftItem(checkboxElem, ItemID, GiftID, Title, Image, DisplayPrice, DiscountedPrice) {
    //addgift();
    if (checkboxElem.checked) {
        arrGift.push({ ItemID: ItemID, GiftID: GiftID, Title: Title, Image: Image, DisplayPrice: DisplayPrice, DiscountedPrice: DiscountedPrice, Key: Math.floor((Math.random() * 1000) + 1) });
    } else {
        arrGift.splice(checkboxElem, 1);
    }
}



//toast
function toast(res, condition) {
    if (condition == 1) {
        $('.toast-body').html(res);
        $('.toast-head-text').html('Success').addClass(' text-success');
        $('.toast').addClass('bg-green text-success');
        $('.toast').toast({ delay: 3000 }).toast('show');
    }
    else if (condition == 2) {
        $('.toast-head-text').html('Warning');
        $('.toast-body').html(res);
        $('.toast').addClass('bg-warning text-dark');
        $('.toast').toast({ delay: 3000 }).toast('show');
    }
    else {
        $('.toast-body').html(res);
        $('.toast-head-text').html('Danger');
        $('.toast').addClass(' bg-danger text-white ');
        $('.toast').toast({ delay: 3000 }).toast('show');
    }

};

function toastOut(res, condition) {
    if (condition == 1) {
        $('.toast-body').html(res);
        $('.toast-head-text').html('Warning');
        $('.toast-body').html(res);
        $('.toast').addClass('bg-warning text-dark');
        $('.toast').toast({ delay: 3000 }).toast('show');
    }
    else if (condition == 2) {
        $('.toast-head-text').html('Warning');
        $('.toast-body').html(res);
        $('.toast').addClass('bg-warning text-dark');
        $('.toast').toast({ delay: 3000 }).toast('show');
    }
    else {
        $('.toast-body').html(res);
        $('.toast-head-text').html('Danger');
        $('.toast').addClass(' bg-danger text-white ');
        $('.toast').toast({ delay: 3000 }).toast('show');
    }

};

//header
function topheadcart() {
     
    var currency = localStorage.getItem("currency");
    var cart = "[]";
    var chkLScart = localStorage.getItem("_cartitems");
    if (chkLScart == null) {
        localStorage.setItem("_cartitems", cart);
    }

    var wishlist = "[]";
    var chkLSwishlist = localStorage.getItem("_Wishlistitems");
    if (chkLSwishlist == null) {
        localStorage.setItem("_Wishlistitems", wishlist);
    }


    var gift = "[]";
    var chkLSgift = localStorage.getItem("_giftitems");
    if (chkLSgift == null) {
        localStorage.setItem("_giftitems", gift);
    }

    var gifts = getgiftLS();
    var data = getCartLS();

    var html = '';
    var totalPrice = 0;
    var newPrice = 0;
    var totalQty = data.length;
    var totalQtywish = getWishlistLS().length;
   /* var lang = $.cookie("lang");*/
    /*lang === "en"? data[i].Name : data[i].ArabicName*/
    html += '<div class="cart-height scrollbar" id="style2" >'
    for (var i = 0; i < data.length; i++) {
        var giftPrice = 0;
        
        if (data[i].NewPrice == null || data[i].NewPrice == 0) {
            totalPrice += data[i].Qty * data[i].Price;
        }
        else {
            totalPrice += data[i].Qty * data[i].NewPrice;
        }
        
        html += '<li>'
            + '<div class="media">'
        if (data[i].Image == "" || data[i].Image == null) {
            html += '<a href="/Product/ProductDetails?ID=' + data[i].ID + '"><img class="me-3" alt="" src="/Content/assets/images/NA.png"></a>'
        }
        else {
            html += '<a href="/Product/ProductDetails?ID=' + data[i].ID + '"><img alt="" src="https://retail.premium-pos.com/' + data[i].Image + '"></a>'
        }
        html += '<div class="media-body">'
            + '<a href="/Product/ProductDetails?ID=' + data[i].ID + '">'
            + '<h6>' + data[i].Qty + ' X ' + data[i].Name + '</h6>'
            + '</a>'
        html += '<h4><span>' + currency + ' ' + ((data[i].Qty * data[i].Price) + giftPrice).toFixed(2) + '</span>'
            + '</h4>'
            + '</div>'
            + '</div>'
            + '<div class="close-circle"><a href="#" onclick="removeCartItem(' + data[i].Key + '); return false;"><i class="fa fa-times" aria-hidden="true"></i></a></div>'
            + '</li>'

    }

    html += '</div>'

        + '<li>'
        + '<div class="total">'
        + '<h5 data-translate="000co23"><b>Sub-Total :</b> <span>' + currency + ' ' + totalPrice.toFixed(2) + '</span></h5 > '
        + '</div>'
        + '</li>'
        + '<li class="mini-cart-btns">'
        + '<div class="buttons"><a href="/order/cart" class="view-cart _arabic" data-translate="000co73">View Cart</a>'
        + '<a href="/order/cart"  class="checkout _arabic" data-translate="00ch0">checkout</a></div >'
        + '</li>'

    if (data.length > 0) {
        $(".head-cart").show();
    }
    else {
        $(".head-cart").hide();
    }
    $(".head-cart").html(html);
    $("#cart-total").html(totalQty);
    $("#wish-total").html(totalQtywish);
};




//cart
function cartitem() {
     
    var currency = localStorage.getItem("currency");
    var total = 0;
    var data = getCartLS();
    var html = '';
    var totalPrice = 0;
    var totalQty = 0;


    for (var i = 0; i < data.length; i++) {
        var giftPrice = 0;

        var itemprice = 0;
        totalQty += Number(data[i].Qty);
        //totalPrice += data[i].Qty * data[i].Price;
        if (data[i].NewPrice == null || data[i].NewPrice == 0) {
            totalPrice += data[i].Qty * data[i].Price;

            itemprice = data[i].Price;
        }
        else {
            itemprice = data[i].NewPrice;
            totalPrice += data[i].Qty * data[i].NewPrice;
        }

        html += '<tr>'
            + '<td class="plantmore-product-remove"><button class="bg-transparent border-0 text-danger" onclick="removeCartItem(' + data[i].Key + '); return false;"><i class="h3 ion-trash-a mb-0"></i></button></td>'
        if (data[i].Image == "" || data[i].Image == null) {
            html += '<td class="plantmore-product-thumbnail"><a href="/Product/ProductDetails?ID=' + data[i].ID + '"><img class="cart-img" src="/Content/assets/images/NA.png" alt=""></a></td>'
        }
        else {
            html += '<td class="plantmore-product-thumbnail"><a href="/Product/ProductDetails?ID=' + data[i].ID + '"><img class="cart-img" src="https://retail.premium-pos.com/' + data[i].Image + '" alt=""></a></td>'
        }

        html += '<td class="plantmore-product-name">'
            + '<p><a href="/Product/ProductDetails?ID=' + data[i].ID + '">' + data[i].Name + '</a></p>'
        html += '</td>'
            + '<td class="plantmore-product-price"><span class=""><span class="currency-text mx-0"></span><h2 class="td-color">' + currency + ' ' + itemprice.toFixed(2) + '</h2></span></td>'

            + '<td class="plantmore-product-quantity">'
            + '<input id="qty' + data[i].Key + '"  name="qty' + data[i].Key + '" onchange="changeQty(' + data[i].Key + ',' + itemprice + '); return false;" class="Quantity" value="' + data[i].Qty + '" type="number">'
            + '</td>'
           

            + '<td class="product-subtotal"><h2 class="td-color">' + currency + ' ' + '<span class="amount totalprice"  id="tprice' + data[i].Key + '">' + ((data[i].Qty * itemprice) + giftPrice).toFixed(2) + '</span></td></h2>'

            + '</tr>'




    }

    if (data.length > 0) {
        $(".cart-items").html(html);
        $("#check-btn").show();
    }
    else {
        $(".cart-items").html("No Item added");
        $("#cart-table").html("No Item added");
        $("#check-btn").hide();
    }
    $(".subtotal").html(currency + ' ' + totalPrice.toFixed(2));
    $(".totalamount").html(currency + ' ' + totalPrice.toFixed(2));


}

function changeQty(key, price) {
    debugger
    if ($('#qty' + key).val() > 0) {

        var cartItems = getCartLS();
        for (var i = 0; i < cartItems.length; i++) {
            if (cartItems[i].Key == key) {
                cartItems[i].Qty = $('#qty' + key).val();
                cartItems[i].Price = price;
                $('#tprice' + key).html((cartItems[i].Qty * cartItems[i].Price).toFixed(2));
            }
        }

        setCartLS(cartItems);
        cartitem();
        topheadcart();
    }
    else {
        $('#qty' + key).val(1);
    }


}

  
function removeCartItem(ele) {

    var chkLScart = getCartLS();
    var chkLSgift = getgiftLS();
    //var delRow = chkLScart.filter(obj => obj.Key === ele);

    chkLScart.forEach(function (item, index) {
        item.Key === ele && chkLScart.splice(index, 1);
    });

    //var delRow = chkLSgift.filter(obj => obj.ItemKey === ele);
    //chkLSgift.forEach(function (item, index) {
    //    item.ItemKey === ele && chkLSgift.splice(index, delRow.length);
    //});
    setCartLS(chkLScart);
    //setgiftLS(chkLSgift);

    cartitem();
    topheadcart();
}
function removeCartGift(ele) {

    var chkLSgift = getgiftLS();


    var delRow = chkLSgift.filter(obj => obj.Key === ele);
    chkLSgift.forEach(function (item, index) {
        item.Key === ele && chkLSgift.splice(index, delRow.length);
    });
    setgiftLS(chkLSgift);

    cartitem();
    topheadcart();

}
function addtocart(ID, Name, Image,NewPrice, Price, Qty, CurrentStock) {
     
    //ProNote = ProNote == undefined ? "" : ProNote;
    var _Key = Math.floor((Math.random() * 1000) + 1);
    $('#hdnItemKey').val(_Key);

    var arrTemp = [];
    arrTemp = getCartLS();
    arrTemp.push({ ID: ID, Name: Name, Image: Image,NewPrice: NewPrice, Price: Price, Qty: Qty, CurrentStock: CurrentStock, Key: _Key });
    setCartLS(arrTemp);
    topheadcart();
    toast('Item Added to Cart', 1)
}
function setCartLS(arr) {
    var getCartItem = localStorage.getItem("_cartitems");
    if (getCartItem != null) {

    }
    localStorage.setItem("_cartitems", JSON.stringify(arr));
}
function getCartLS() {
    var getCartItem = localStorage.getItem("_cartitems");
    if (getCartItem != null && getCartItem != "")
        return JSON.parse(getCartItem);
    else
        return JSON.parse("[]");
}
//Login
function login() {
     
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var arrTemp = [];
    arrTemp.push({ EMAIL: email, PASSWORD: password });
    localStorage.setItem("email", email);
    localStorage.setItem("password", password);
}
function logout() {
    debugger
    var email = localStorage.getItem("email", email);
    var password = localStorage.getItem("password", password)
    localStorage.setItem("email", "");
    localStorage.setItem("password", "");
}
//Wishlist
function addtoWishlist(ID, Name, Image, Price, StatusID, Qty) {

    var arrTemp = [];
    arrTemp = getWishlistLS();
    arrTemp.push({ ID: ID, Name: Name, Image: Image, Price: Price, StatusID: StatusID, Qty: Qty, Key: Math.floor((Math.random() * 1000) + 1) });
    setWishlistLS(arrTemp);
    topheadcart();
}
function setWishlistLS(arr) {
    var getWishlistItem = localStorage.getItem("_Wishlistitems");
    if (getWishlistItem != null) {
        localStorage.setItem("_Wishlistitems", JSON.stringify(arr));
    }
}
function getWishlistLS() {
    var getWishlistItem = localStorage.getItem("_Wishlistitems");
    if (getWishlistItem != null && getWishlistItem != "")
        return JSON.parse(getWishlistItem);
    else
        return JSON.parse("[]");
}
function removeWishlistitem(ele) {
    var chkLS = getWishlistLS();

    chkLS.splice(chkLS.filter(obj => obj.Key === ele), 1);
    setWishlistLS(chkLS);
    GetWishListItems();
}

function GetWishListItems() {
    var currency = localStorage.getItem("currency");
    var total = 0;
    var chkLSWishlist = localStorage.getItem("_Wishlistitems");
    var data = JSON.parse(chkLSWishlist);
    var html = '';
    var totalPrice = 0;
    var totalQty = 0;


    for (var i = 0; i < data.length; i++) {
        totalQty += Number(data[i].Qty);
        totalPrice += data[i].Price;
        html += '<tr>'
        if (data[i].Image == "" || data[i].Image == null) {
            html += '<td class="plantmore-product-thumbnail"><a href="/Product/ProductDetails?ID=' + data[i].ID + '"><img class="wishlist-img" src="/Content/assets/images/NA.png" alt=""></a></td>'
        }
        else {
            html += '<td class="plantmore-product-thumbnail"><a href="/Product/ProductDetails?ID=' + data[i].ID + '"><img class="wishlist-img" src="https://retail.premium-pos.com/' + data[i].Image + '" alt=""></a></td>'
        }

        html += '<td class="plantmore-product-name"><a href="#">' + data[i].Name + '</a></td>'
            + '<td class="plantmore-product-price"><span class="currency-text mx-0">' + currency + ' ' + data[i].Price.toFixed(2) + '</span></td>'
            + '<td class="plantmore-product-stock-status"><span class="stockcheck">' + data[i].StatusID + '</span></td>'
            + '<td class="plantmore-product-add-cart"><a style="background-color:var(--theme-color);color:white;" class="btn btn-default btn-small" href="/Product/ProductDetails?ID=' + data[i].ID + '">Add to Cart</a></td>'
            + '<td class="plantmore-product-remove"><button class="bg-transparent border-0 text-danger" onclick="removeWishlistitem(' + data[i].Key + '); return false;"><i class="h5 ion-trash-a mb-0"></i></a></td>'
            + '</tr>'

    }
    if (data.length > 0) {
        $(".wishlist-items").html(html);
    }
    else {
        $("#ytdTable").html("You donot have any item in favourites ");
    }


};

function StockActiveColor() {

    $("#ytdTable span.stockcheck").each(function () {

        var stock = $(this).html();

        if (stock == "True") {
            $(this).html("In Stock");
            $(this).css("color", "green");
        }
        else {
            $(this).html("In Stock");
            $(this).css("color", "black");
        }
    });
};

//Currency
var currency = "BHD";
var currencyLS = localStorage.getItem("currency");
if (currencyLS == null) {
    localStorage.setItem("currency", currency);
}
else {
    localStorage.setItem("currency", "BHD");
}
function ShowText() {
    var currency = localStorage.getItem("currency");
    $(".currency-text").text(currency);
};


//form validation
(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Get the forms we want to add validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {

            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();


//Index - Home

function getmail() {
    var email = $(".SubscribeEmail").val();
    $('.SubscribeEmail').val("");

    $.ajax({
        type: "Get",
        url: '/Home/Subscribe?email=' + email,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (res) {


        },
        error: function (xhr, textStatus, errorThrown) {
            //
        }
    });
};










