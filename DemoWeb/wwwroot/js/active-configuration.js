$(document).ready(function () {
    var pathName = window.location.pathname;
    var currentControllerName;
    var currentViewName;
    currentControllerName = pathName.split('/')[1];
    currentViewName = pathName.split('/')[2];
    switch (currentControllerName) {
        case '':
            document.getElementById('home-nav').classList.remove('collapsed');
            break;
        case 'Sales':
            document.getElementById('sales-nav').classList.remove('collapsed');
            break;
        case 'Expenses':
            document.getElementById('expenses-nav').classList.remove('collapsed');
            break;
        case 'Report':
            document.getElementById('report-nav').classList.remove('collapsed');
            break;
        default: break;
    }
})