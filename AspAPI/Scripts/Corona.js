$(document).ready(function () {
    $('#DataTable1').dataTable({
        "ajax": loadDataCorona(),
        responsive: true,
    });

    function loadDataCorona() {
        $.ajax({
            url: "/Corona/LoadCorona",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                //debugger;
                var html = '';
                $.each(result, function (key, Corona) {
                    html += '<tr>';
                    html += '<td>' + Corona.country + '</td>';
                    html += '<td>' + Corona.cases + '</td>';
                    html += '<td>' + Corona.deaths + '</td>';
                    html += '<td>' + Corona.recovered + '</td>';
                    html += '<td><button type="button" class="btn btn-warning" id="update"">Edit</button>';
                    html += ' <button type="button" class="btn btn-warning" id="delete">Hapus</button> </td>';
                    html += '</tr>';
                });
                $('.BodyTable').html(html);
            },
            error: function (errormsg) {
                alert(errormessage.responseText);
            }
        });
    }
});