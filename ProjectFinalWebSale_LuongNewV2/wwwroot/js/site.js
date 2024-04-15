// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(() => {
    LoadProductData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    connection.on("LoadProducts", function () {
        LoadProductData();
    })
    LoadProductData();
    function LoadProductData() {
        var tr = '';
        $.ajax({
            url: '/Products/Index',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                     <td> ${v.productId} </td>
                    <td> ${v.productName} </td>
                    <td> ${v.categoryId} </td>
                    <td> ${v.userId} </td>
                    <td> ${v.unitStock} </td>
                    <td> ${v.unitPrice} </td>
                    <td> ${v.images} </td>
                    <td> ${v.status} </td>

                    <td>
                    <a href='../Products/Edit?id=${v.productId}'>Edit </a>
                    <a href='../Products/Details?id=${v.productId}'>Details </a>
                    <a href='../Products/Delete?id=${v.productId}'>Delete </a>
                    
                    </td>
                    </tr>`
                })
                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        });
    }
})
