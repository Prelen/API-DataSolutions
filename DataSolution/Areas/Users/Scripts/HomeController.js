angular.module('HomeApp', [])
    .controller('HomeController', function ($scope, $http, $timeout) {
        
        userprofile = '';
        $scope.ticks = [];
        $scope.datapoints = [];
        $scope.activities = [];
        $scope.activity = {};
        $scope.activity[
            {
                activityType: '',
                description: '',
                day: '',
                month: ''
            }
        ];

        $scope.notifications = [];
        $scope.notification = {};
        $scope.notification[
            {
                notificationID: '',
                message: '',
                day: '',
                month: ''
            }
        ];

        $scope.userID = '';
        function formatter(val, axis) {
            return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        };

        var options = {
            legend: {
                show: true,
                margin: 10,
                backgroundOpacity: 0.9
            },
            points: {
                show: true,
                radius: 3
            },
            lines: {
                show: true
            },
            grid: { hoverable: true, clickable: false },
            yaxis: { min: 0, tickFormatter: formatter },
            xaxis: { ticks: $scope.ticks }

        };

        $scope.InitPage = function () {
          
           
            GetHomePageInfo();
            
        };


        function GetHomePageInfo() {
             
             $http(
                 {
                     method: 'GET',
                     url: '/Users/User/GetHomePageInfo'
                 }
             ).then(function (response) {
                
                 if (response.data !== null) {
                     
                     var json = JSON.parse(response.data);
                     var chartVals = json.ChartValues;
                     var activityJson = json.AuditInfo;
                     var notificationJson = json.Notifications;

                     $scope.fullName = json.UserInfo.FirstName + " " + json.UserInfo.Surname;
                     $scope.userID = json.UserInfo.UserID;
                     userprofile = $scope.userID;

                     //Chart
                     for (var i = 0; i < chartVals.length; i++) {
                         $scope.ticks.push([i, chartVals[i].label]);
                         $scope.datapoints.push([i, chartVals[i].value]);
                     }

                     //Audit
                     for (var j = 0; j < activityJson.length; j++) {
                         if (activityJson[j] != null) {
                             $scope.activity = {
                                 activityType: 'Activity',
                                 description: activityJson[j].AuditInfo.ActivityDescription,
                                 day: activityJson[j].FormattedDay,
                                 month: activityJson[j].FormattedMonth
                             };
                             $scope.activities.push($scope.activity);
                         }
                     }

                     //Notification
                     LoadNotifications(notificationJson,false);
                     //for (var k = 0; k < notificationJson.length; k++) {
                     //    if (notificationJson[k] != null) {
                     //        $scope.notification = {
                     //            notificationID: notificationJson[k].NotificationInfo.NotificationID,
                     //            message: notificationJson[k].NotificationInfo.NotificationMessage,
                     //            day: notificationJson[k].FormattedDay,
                     //            month: notificationJson[k].FormattedMonth
                     //        };
                     //        $scope.notifications.push($scope.notification);
                     //    }
                     //}

                        
                     var dataset = [{ label: "Transactions", data: $scope.datapoints, color: 'purple' }]; 
                     $.plot($("#graph"), dataset, options);
                    
                 }
             });
        };

        $scope.DeleteNotification = function (notID) {
            $http(
                {
                    method: 'GET',
                    url: '/Users/User/DeleteNotification',
                    params: {
                        NotificationID: notID
                    }
                }
            ).then(function (response) {
                LoadNotifications(response.data, true);
                                
            });

        };


        function LoadNotifications(noteData,isReload) {
            if (noteData != null) {
                var newjson = JSON.stringify(noteData);
                var json = JSON.parse(newjson);
                $scope.notifications = [];
                for (var i = 0; i < json.length; i++) {
                    if (json[i] != null) {
                        $scope.notification = {
                            notificationID: json[i].NotificationInfo.NotificationID,
                            message: json[i].NotificationInfo.NotificationMessage,
                            day: json[i].FormattedDay,
                            month: json[i].FormattedMonth
                        };
                        $scope.notifications.push($scope.notification);

                    }
                }
            }

            if (isReload) {
                $scope.$apply();
            }
        };
    });