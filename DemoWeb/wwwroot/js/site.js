$(document).ready(function () {
    configureSelect2();
    configureDataTable();
    configureFormSubmit();
});
$(document).ajaxComplete(function (event, xhr, options) {
    if (xhr.status === 401) {
        location.reload();
    }
});
function configureSelect2() {
    $(".form-select.form-select2").select2({
        placeholder: "Please Select",
        allowClear: true,
        theme: "bootstrap-5",
        width: "100%",
    });
}

function configureDataTable() {
    $('.data-table').DataTable({
        scrollY: "450px",
        fixedColumns: true,
        lengthChange: false, // Disable the entries per page dropdown
        searching: false, // Disable searching
        paging: false, // Disable paging
        ordering: false,
        info: false
    });
}
function configureFormSubmit() {
    var forms = document.querySelectorAll('.form-submit');
    forms.forEach(function (form) {
        form.addEventListener('submit', function (event) {
            swal.showLoading();
        });
    });
}