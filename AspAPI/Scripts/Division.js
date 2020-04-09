$(document).ready(function () {
    LoadDataDivision();
    LoadDataDepartment('#Department');
    $('#Edit').hide();

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })
});

document.getElementById("BtnAdd").addEventListener("click", function () {
    Clearall();
});

function ClearScreen() {
    $('#Name').val('');
    $('#Save').show();
    $('#Update').hide();
    $('#Delete').hide();
    $('#Modal').modal('hide');
}

function Clearall() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Save').show();
    $('#Edit').hide();
}

function LoadDataDivision() {
    debugger;
    $.fn.dataTable.ext.errMode = 'none';
    $('#DataTable1').dataTable({
        "ajax": {
            url: "/Division/LoadDivision",
            type: "GET",
            dataType: "json",
            dataSrc: ""
        },
        "columns": [
            { "data": "DivisionName" },
            { "data": "DepartmentName" },
            {
                "data": "CreateDate", "render": function (data) {
                    return moment(data).tz('Asia/Jakarta').format('DD/MM/YYYY');
                }
            },
            {
                "data": "UpdateDate", "render": function (data) {
                    var text = "Not Update Yet";
                    if (data == null) {
                        return text;
                    } else {
                        return moment(data).tz('Asia/Jakarta').format('DD/MM/YYYY');
                    }
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return '<button type="button" class="btn btn-warning" id="BtnEdit" data-toggle="tooltip" data-placement="top" title="Edit" onclick="return GetById(' + row.Id + ')"><i class="mdi mdi-pencil"></i></button> &nbsp; <button type="button" class="btn btn-danger" id="BtnDelete" data-toggle="tooltip" data-placement="top" title="Hapus" onclick="return Delete(' + row.Id + ')"><i class="mdi mdi-delete"></i></button>';
                }, "orderable": false
            }
        ]
    });
}

var Departments = []
function LoadDataDepartment(element) {
    if (Departments.length == 0) {
        $.ajax({
            type: "GET",
            url: "Department/LoadDepartment",
            success: function (data) {
                Departments = data;
                renderDepartment(element);
            }
        })
    } else {
        renderDepartment(element);
    }
}

function renderDepartment(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select Department').hide());
    $.each(Departments, function (i, val) {
        $ele.append($('<option/>').val(val.Id).text(val.Name));
    })
}

function GetById(Id) {
    debugger;
    $.ajax({
        url: "/Division/GetById/" + Id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            const obj = JSON.parse(result);
            $('#Id').val(obj.Id);
            $('#Name').val(obj.DivisionName);
            $('#Department').val(obj.DepartmentId);
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
    var table = $('#DataTable1').DataTable({
        "ajax": {
            url: "/Division/LoadDivision"
        }
    });
    var Division = new Object();
    Division.DivisionName = $('#Name').val();
    Division.DepartmentId = $('#Department').val();
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
            url: '/Division/InsertorUpdate/',
            data: Division
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    icon: 'success',
                    position: 'center',
                    title: 'Division Saved Successfully',
                    timer: 5000
                }).then(function () {
                    $('#Modal').modal('hide');
                    table.ajax.reload();
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
    var table = $('#DataTable1').DataTable({
        "ajax": {
            url: "/Division/LoadDivision"
        }
    });
    var Division = new Object();
    Division.Id = $('#Id').val();
    Division.DivisionName = $('#Name').val();
    Division.DepartmentId = $('#Department').val();
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
            url: '/Division/InsertorUpdate/',
            data: Division
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    icon: 'success',
                    position: 'center',
                    title: 'Division Updated Successfully',
                    timer: 5000
                }).then(function () {
                    $('#Modal').modal('hide');
                    table.ajax.reload();
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
}

function Delete(Id) {
    var table = $('#DataTable1').DataTable({
        "ajax": {
            url: "/Division/LoadDivision"
        }
    });

    Swal.fire({
        title: "Are you sure ?",
        text: "You won't be able to Revert this!",
        showCancelButton: true,
        confirmButtonText: "Yes, Delete it!"
    }).then((result) => {
        if (result.value) {
            debugger;
            $.ajax({
                url: "Division/Delete/",
                data: { Id: Id }
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire({
                        icon: 'success',
                        position: 'center',
                        title: 'Division Delete Successfully',
                        timer: 5000
                    }).then(function () {
                        table.ajax.reload();
                        ClearScreen();
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Failed to Delete',
                    })
                    ClearScreen();
                }
            })
        }
    })
}