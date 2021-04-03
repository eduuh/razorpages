var dataTable;

$(document).ready(function () {
    LoadList();
});

function LoadList() {
    dataTable = $('#Dt_Load').DataTable({
        "ajax": {
            "url": "/api/user",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fullName", "width": "25%" },
            { "data": "userName", "width": "15%" },
            { "data": "email", "width": "20%" },
            { "data": "phoneNumber", "width": "20%" },
            {
                "data": { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {

                        return `
                         <div class="text-center"> 
                           <a class="btn btn-success text-white" style="cussor:pointer, width:100px;" onclick="LockUnlock">
                             <i class="fas fa-lock-open"</i> Unlock
                    </a></div>`;
                    } else {
                        return `
                         <div class="text-center"> 
                           <a class="btn btn-success text-white" style="cussor:pointer, width:100px;" onclick="LockUnlock">
                             <i class="fas fa-lock"</i> LockUser
                         </div>`;
                    }
                }
                ,
                "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    })
}


function Delete(url) {
    swal({
        title: "Are You sure You Want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willdelet) => {
        if (willdelet) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message)
                    }

                }
            })
        }
    })
}