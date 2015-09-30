dataApp.factory('userData', function($http) {
		
		return {
			getUser: function(id, successcb){			

			$http({method: 'GET', url: '/application/userdata'})
				.success(function(data, status, headers, config){
					successcb(data);
				})
				.error(function(data, status,headers,config) {
					$log.error(data);
					
				});

				

		}}

		
})

