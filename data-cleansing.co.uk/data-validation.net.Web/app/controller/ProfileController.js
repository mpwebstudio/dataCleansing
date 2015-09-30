dataApp.controller('ProfileController',
  function ProfileController($scope, userData){

	 userData.getUser(1,function(data){
	     
		for (var i = data.length; i >= 0; i--) {
		    
		    $scope.userName = data[0].UserName;
		    $scope.FirstName = data[0].FirstName;
		    $scope.SurName = data[0].SurName;
		    $scope.Telephone = data[0].Telephone;
		    $scope.Email = data[0].Email;
		    $scope.WebPage = data[0].WebPage;
		}
	 })
	
  }



)


