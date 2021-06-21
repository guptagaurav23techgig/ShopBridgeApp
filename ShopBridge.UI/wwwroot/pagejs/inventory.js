$("[id*='btnEdit']").click(function () {
    //debugger;
    var mytr = $(this).closest("tr");
    var invid = mytr.find("[id*=hdnInvId]").val();
    window.location.href = getCookie("appBaseUrl") + "/Inventory/Edit?inventoryId=" + invid;
});

$("[id*='btnDel']").click(function () {
    //debugger;
    var mytr = $(this).closest("tr");
    var invid = mytr.find("[id*=hdnInvId]").val();
    var model = {};
    model.InventoryId = invid;    
    model.ip = myIP();

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            
            var res = RquestMVCToAPI('/Inventory/Delete', model);

            if (res.Type == "success") {
                Swal.fire({
                    icon: 'success',
                    title: 'Inventory',
                    text: 'Inventory deleted successfully!',
                    footer: ''
                }).then(function () {                    
                    window.location.reload();
                });

                getInventories();
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Inventory',
                    text: 'Inventory not deleted!',
                    footer: ''
                })
            }
        }
    })
});

//function resetInventory() {
//    $("#inventoryName").val("");
//    $("#inventoryDesc").val("");
//    $("#inventoryPrice").val("");
//    $("#invNo").prop("checked", true);
//}

function addInventory () {
    //alert("Go");
    var model = {};
    model.InventoryId = 0;
    model.InventoryName = $("#inventoryName").val();
    model.InventoryDescription = $("#inventoryDesc").val();
    model.InventoryPrice = $("#inventoryPrice").val();
    model.isAvailable = $("input[name='inventoryAvail']:checked").val();
    model.ip = myIP();
    var res = RquestMVCToAPI('/Inventory/Add', model);

    if (res.Type == "success") {
        Swal.fire({
            icon: 'success',
            title: 'Inventory',
            text: 'Inventory added successfully!',
            footer: ''
        }).then(function () {
            window.location.replace(getCookie("appBaseUrl") + "/Inventory/Index");
        });
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Inventory',
            text: 'Inventory not added!',
            footer: ''
        })
    }
}

function updateInventory () {
    //debugger;
    var model = {};
    model.InventoryId = $("#hdnInvId").val();
    model.InventoryName = $("#inventoryName").val();
    model.InventoryDescription = $("#inventoryDesc").val();
    model.InventoryPrice = $("#inventoryPrice").val();
    model.isAvailable = $("input[name='inventoryAvail']:checked").val();
    model.ip = myIP();

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Update it!'
    }).then((result) => {
        if (result.isConfirmed) {

            var res = RquestMVCToAPI('/Inventory/Edit', model);

            if (res.Type == "success") {
                Swal.fire({
                    icon: 'success',
                    title: 'Inventory',
                    text: 'Inventory updated successfully!',
                    footer: ''
                }).then(function () {
                    window.location.replace(getCookie("appBaseUrl") + "/Inventory/Index");
                });
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Inventory',
                    text: 'Inventory not updated!',
                    footer: ''
                })
            }
        }
    })    
}



$("input[type=radio][name=inventoryAvail]").change(function () {
    $("#hdnAvail").val(this.value);
    //$("#spnChk").text(this.value);
    //Swal.fire({
    //    icon: 'success',
    //    title: 'Inventory',
    //    text: $("#hdnAvail").val(),
    //    footer: ''
    //})
});
