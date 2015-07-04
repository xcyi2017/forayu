app.factory('ViewService', function () {
    var showing = {};

    showing.household = false;

    var show = function(viewName) {
        showing[viewName] = true;
    }

    return {
        show: show,
        isShowing: showing
    }
});