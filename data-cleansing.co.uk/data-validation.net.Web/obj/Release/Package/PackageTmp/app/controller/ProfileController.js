dataApp.controller('ProfileController',
  function ProfileController($scope, userData){

	 userData.getUser(1,function(data){
		    $scope.FirstName = data.FirstName;
		    $scope.SurName = data.SurName;
		    $scope.Telephone = data.Telephone;
		    $scope.Email = data.Email;
		    $scope.WebPage = data.WebPage;
	 })
	
  }



)


