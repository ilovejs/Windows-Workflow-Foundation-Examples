param($installPath, $toolsPath, $package, $project) 

Import-Module(join-path $toolsPath "Microsoft.Activities.Extensions.NuGet.dll")

Remove-ToolboxTab  -Category "Microsoft.Activities.Extensions"	
Remove-ToolboxTab  -Category "Dictionary"			
