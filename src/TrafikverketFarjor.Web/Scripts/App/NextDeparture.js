function NextDepartureViewModel() {
    var self = this;

    self.ferryInfos = ko.observableArray();
    self.ferryRoutes = ko.observableArray();
    self.selectedFerryInfo = ko.observable();
    self.selectedFerryRoute = ko.observable();
    self.nextDepartures = ko.observableArray();

    self.updateFerryRouteList = function() {
        var selectedFerryInfo = self.selectedFerryInfo();
        if (selectedFerryInfo) {
            self.ferryRoutes(selectedFerryInfo.DepartsFrom());
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
}
