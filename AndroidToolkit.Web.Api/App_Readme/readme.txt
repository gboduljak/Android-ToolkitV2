To install, add this line in the Ninject boostrapper file, generally in your App_Start folder :    
	 
	  System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);
     
I will be adding automation for this in the future.