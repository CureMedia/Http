// Copied from https://raw.githubusercontent.com/cake-build/example/master/build.cake

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var projects = new string[] { 
	"Http.OAuth", 
	"Http.OAuth.IdentityModel" 
	};

// Define directories.
var artifacts = Directory("./artifacts");
var tests = artifacts + Directory("test");
var buildDir =  artifacts + Directory("bin");
var solution = "Cure.Http.sln";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
	DotNetCoreClean(solution);
	CleanDirectory(artifacts);
    CleanDirectory(buildDir);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore();
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      var settings = new DotNetCoreBuildSettings
     {         
         Configuration = configuration,
		 NoRestore = true
     };     
      DotNetCoreBuild(solution, settings);
    }
    else
    {
      // Use XBuild
      XBuild(solution, settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Test")
    .IsDependentOn("Build")
  .Does(() =>
    {
		 var settings = new DotNetCoreTestSettings
        {
           NoBuild = true,
           Configuration = configuration,
           ArgumentCustomization = args => {
                args.Append("--logger:trx");
                args.Append("--results-directory:"+MakeAbsolute(File(tests)));
                return args;
           }
        };
        var projects = GetFiles("test/**/*.csproj");
        foreach(var project in projects)
        {
            DotNetCoreTest(project.FullPath, settings);
        }
    });

Task("Pack")
	.IsDependentOn("Test")
	.Does(() =>
		{
			 var settings = new DotNetCorePackSettings
		     {
				Configuration = configuration,
				OutputDirectory = artifacts,
				NoBuild = true
			};
			var projects = GetFiles("src/**/*.csproj");
			foreach(var project in projects)
			{
				DotNetCorePack(project.FullPath, settings);
			}			
		});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Pack");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);