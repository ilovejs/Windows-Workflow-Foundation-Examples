param($installPath, $toolsPath, $package, $project) 

#remove the designer
$ref = $project.Object.References.Find("Microsoft.Activities.UnitTesting.Design")
if ($ref) {	$ref.Remove() }

Import-Module(join-path $toolsPath "Microsoft.Activities.Extensions.NuGet.dll")

Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.UnitTesting.Activities.DiagnosticTrace" -Category "Unit Testing" -ActivityAssembly "Microsoft.Activities.UnitTesting" -BitmapID "DiagTrace"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.UnitTesting.Activities.TestBookmark``1" -DisplayName "TestBookmark<T>" -Category "Unit Testing" -ActivityAssembly "Microsoft.Activities.UnitTesting" -BitmapID "bookmark"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.UnitTesting.Activities.TestAsync" -Category "Unit Testing" -ActivityAssembly "Microsoft.Activities.UnitTesting" -BitmapID "Activity"

#start the overview page
start http://wf.codeplex.com/wikipage?title=Microsoft.Activities.UnitTesting%20Overview

# Update this URI to match the release page on CodePlex
start http://wf.codeplex.com/releases/view/78025

