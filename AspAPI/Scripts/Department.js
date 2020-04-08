$(document).ready(function () {
    $('#Edit').hide();

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })
});

document.getElementById("BtnAdd").addEventListener("click", function () {
    Clearall();
});

$('#DataTable1').dataTable({
    //"processing": true,
    //"serverSide": true,
    "ajax": loadDataDept(),
    responsive: true,
    "columns": [
        { "name": "first", "orderable": false },
        { "name": "second", "orderable": false },
        { "name": "third", "orderable": false },
        { "name": "fourth", "orderable": false },
        { "name": "fifth", "orderable": false }
    ]
});

function loadDataDept() {
    $.ajax({
        url: "/Department/LoadDepartment",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            var html = '';
            $.each(result, function (key, Dept) {
                html += '<tr>';
                html += '<td>' + Dept.Id + '</td>';
                html += '<td>' + Dept.Name + '</td>';
                html += '<td>' + moment(Dept.CreateDate).format('DD-MM-YYYY hh:mm:ss') + '</td>';
                html += '<td><button type="button" class="btn btn-warning" id="btnedit" data-toggle="tooltip" data-placement="top" title="Edit" onclick="return GetById(' + Dept.Id + ')"><i class="mdi mdi-pencil"></i></button>';
                html += ' <button type="button" class="btn btn-danger" id="btndelete" data-toggle="tooltip" data-placement="top" title="Hapus" onclick="return Delete(' + Dept.Id + ')"><i class="mdi mdi-delete"></i></button> </td>';
                html += '</tr>';
            });
            $('.BodyTable').html(html);
        },
        error: function (errormsg) {
            alert(errormessage.responseText);
        }
    });
}

function GetById(Id) {
    debugger;
    $.ajax({
        url: "/Department/GetById/" + Id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            const obj = JSON.parse(result);
            $('#Id').val(obj.Id);
            $('#Name').val(obj.Name);
            $('#Modal').modal('show');
            $('#Edit').show();
            $('#Save').hide();
        },
        error: function (errormsg) {
            alert(errormessage.responseText);
        }
    })
}

function Save() {
    debugger;
    var Department = new Object();
    Department.Name = $('#Name').val();
    if ($('#Name').val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Name Cannot be Empty',
        })
        return false;
    } else {
        $.ajax({
            type: 'POST',
            url: '/Department/InsertorUpdate/',
            data: Department
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    icon: 'success',
                    position: 'center',
                    title: 'Department Saved Successfully',
                    timer: 5000
                }).then(function () {
                    location.reload();
                    ClearScreen();
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Failed to Insert',
                })
                ClearScreen();
            }
        })
    }
}

function Edit() {
    debugger;
    var Department = new Object();
    Department.Id = $('#Id').val();
    Department.Name = $('#Name').val();
    if ($('#Name').val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Name Cannot be Empty',
        })
        return false;
    } else {
        $.ajax({
            type: 'POST',
            url: '/Department/InsertorUpdate/',
            data: Department
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    icon: 'success',
                    position: 'center',
                    title: 'Department Updated Successfully',
                    timer: 5000
                }).then(function () {
                    location.reload();
                    ClearScreen();
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Failed to Insert',
                })
                ClearScreen();
            }
        })
    }
}

function Delete(Id) {
    Swal.fire({
        title: "Are you sure ?",
        text: "You won't be able to Revert this!",
        showCancelButton: true,
        confirmButtonText: "Yes, Delete it!"
    }).then((result) => {
        if (result.value) {
            debugger;
            $.ajax({
                url: "Department/Delete/",
                data: {Id:Id}
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire({
                        icon: 'success',
                        position: 'center',
                        title: 'Department Saved Successfully',
                        timer: 5000
                    }).then(function () {
                        location.reload();
                        ClearScreen();
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Failed to Update',
                    })
                    ClearScreen();
                }
            })
        }
    });
}

function ClearScreen() {
    $('#Name').val('');
    $('#Update').hide();
    $('#Delete').hide();
    $('#Save').show();
    $('#Modal').modal('hide');
}

function Clearall() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Save').show();
    $('#Edit').hide();
}