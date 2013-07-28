function NextDepartureViewModel() {
    var self = this;

    self.ferryInfos = ko.observableArray();
    self.ferryRoutes = ko.observableArray();
    self.selectedFerryInfo = ko.observable();
    self.selectedFerryRoute = ko.observable();
    self.nextDepartures = ko.observableArray();
    self.version = ko.observable();

    self.updateFerryRouteList = function() {
        var selectedFerryInfo = self.selectedFerryInfo();
        if (selectedFerryInfo) {
            var departsFrom = selectedFerryInfo.DepartsFrom();
            self.ferryRoutes(departsFrom);
            if (departsFrom.length == 1) {
                self.selectedFerryRoute(departsFrom[0]);
                self.getNextDepartures();
            }
        } else {
            self.ferryRoutes([]);
        }
    };

    self.getNextDepartures = function() {
        var selectedFerryInfo = self.selectedFerryInfo();
        if (!selectedFerryInfo) {
            return;
        }
        var selectedFerryRoute = self.selectedFerryRoute();
        if (!selectedFerryRoute) {
            return;
        }

        $.get("/api/1.0/nextDeparture/" + selectedFerryInfo.Name() + "/" + selectedFerryRoute)
            .done(function (data) {
                var nextDepartures = [];
                data.NextDepartures.forEach(function(it) {
                    nextDepartures.push(ko.mapping.fromJS(it));
                });
                self.nextDepartures(nextDepartures);
            });
    };

    $.get("/api/1.0/info")
        .done(function (data) {
            var infos = [];
            data.Info.forEach(function(it) {
                infos.push(ko.mapping.fromJS(it));
            });
            self.ferryInfos(infos);
        });

    $.get("/api/1.0/version")
        .done(function(data) {
            self.version(ko.mapping.fromJS(data));
        });
}
