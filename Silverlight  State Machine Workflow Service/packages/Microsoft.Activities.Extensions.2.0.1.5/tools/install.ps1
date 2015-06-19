param($installPath, $toolsPath, $package, $project) 

#remove the designer
$ref = $project.Object.References.Find("Microsoft.Activities.Extensions.Design")
if ($ref) {	$ref.Remove() }

Import-Module(join-path $toolsPath "Microsoft.Activities.Extensions.NuGet.dll")

Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.DelayUntilTime" -Category "Microsoft.Activities.Extensions" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "DelayCheck"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.DelayUntilDateTime" -Category "Microsoft.Activities.Extensions" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "DelayCheck"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.LoadActivity" -Category "Microsoft.Activities.Extensions" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "dbGreenCheck16"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.InvokeWorkflow" -Category "Microsoft.Activities.Extensions" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "InvokeWorkflow"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.LoadAndInvokeWorkflow" -Category "Microsoft.Activities.Extensions" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "LoadAndInvoke"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.LoadAssembly" -Category "Microsoft.Activities.Extensions" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "LoadArrow16"

Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.AddToDictionary``2" -DisplayName "AddToDictionary<TKey TValue>"  -Category "Dictionary" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "AddToDict"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.ClearDictionary``2" -DisplayName "ClearDictionary<TKey TValue>" -Category "Dictionary" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "ClearDict"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.GetFromDictionary``2" -DisplayName "GetFromDictionary<TKey TValue>" -Category "Dictionary" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "DictGet"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.KeyExistsInDictionary``2" -DisplayName "KeyExistsInDictionary<TKey TValue>" -Category "Dictionary" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "ExistsDict"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.ValueExistsInDictionary``2" -DisplayName "ValueExistsInDictionary<TKey TValue>" -Category "Dictionary" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "ValExistsDict"
Add-ActivityToolbox -Project $project.Object -Activity "Microsoft.Activities.Extensions.Statements.RemoveFromDictionary``2" -DisplayName "RemoveFromDictionary<TKey TValue>" -Category "Dictionary" -ActivityAssembly "Microsoft.Activities.Extensions" -BitmapID "RemoveDict"

start http://wf.codeplex.com/wikipage?title=Microsoft.Activities%20Overview

# Update this URI to match the release page on CodePlex
start http://wf.codeplex.com/releases/view/78024

# # Future function to install with PowerShell and not the cmdlet
# function AddActivity (
# [string] $activity, 
# [string] $assemblyFullname,  
# [string] $name,  
# [string] $category, 
# [string] $bitmapPath)
# {
# 	Write-Host "Argument List"
# 	Write-Host $activity
# 	Write-Host $assemblyFullname 
# 	Write-Host $name  
# 	Write-Host $category 
# 	Write-Host $bitmapPath
# 	
# 	Write-Host "Loading assemblies"
# 	$assemblyFullname = "Microsoft.VisualStudio.Shell.Interop, Version=7.1.40304.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
# 	$assembly = [Reflection.Assembly]::Load("Microsoft.VisualStudio.Shell.Interop")
# 	
# 	Write-Host "get the toolbox service"
# 	$ServiceType = [System.Type]::GetType("Microsoft.VisualStudio.Shell.Interop.SVsToolbox,{0}" -f $assemblyFullname) 		
# 	$InterfaceType = [System.Type]::GetType("Microsoft.VisualStudio.Shell.Interop.IVsToolbox,{0}" -f $assemblyFullname) 
# 	$toolbox = Get-VSService $ServiceType $InterfaceType
# 	
# 	Write-Host "Add a Tab"
# 	$tlBoxTab = $toolbox.AddTab($category)
# 	
# 	Write-Host "Create the DataObject"
# 	$dataObject = New-Object Microsoft.VisualStudio.Shell.OleDataObject
# 	$dataObject.SetData("AssemblyName", $assemblyFullname)
# 	$dataObject.SetData("CF_WORKFLOW_4", $name) 
# 	$dataObject.SetData("WorkflowItemTypeNameFormat", ('{0}{1}' -f $activity, $assemblyFullname))
# 	    
# 	Write-Host "Load the bitmap {0}" $bitmapPath
# 	Write-Host "$bitmapPath"
# 	$bitmap = new-object System.Drawing.Bitmap $bitmapPath
# 	
# 	$toolboxItemInfo = new-object Microsoft.VisualStudio.Shell.Interop.TBXITEMINFO;
# 	$toolboxItemInfo.bstrText = $name
# 	$toolboxItemInfo.hBmp = $bitmap.GetHbitmap()
# 	$toolboxItemInfo.clrTransparent = [System.UInt32][System.Drawing.ColorTranslator]::ToWin32([System.Drawing.Color]::White)
# 	
# 	#Create an array with one element
# 	$tbiArray = [Microsoft.VisualStudio.Shell.Interop.TBXITEMINFO[]] ($toolboxItemInfo)
# 	
# 	Write-Host "Add the item - this will blow up"
# 	$toolbox.AddItem($dataObject, $tbiArray, $category)	
# 	
# 	# Exception calling "AddItem" with "3" argument(s): "Exception calling "InvokeMethod" with "3" argument(s): "Object must implement IConvertible.""
# 	# At C:\users\rojacobs\documents\visual studio 2010\Projects\WorkflowConsoleApplication24\packages\Microsoft.Activities.Extensions.1.8.4.630\tools\install.ps1:53 char:21
# 	# +     $toolbox.AddItem <<<< ($dataObject, $tbiArray, $category)    
# 	#     + CategoryInfo          : NotSpecified: (:) [], MethodInvocationException
# 	#     + FullyQualifiedErrorId : ScriptMethodRuntimeException
# }
# 
# # Invoke the function
# AddActivity "Microsoft.Activities.Extensions.Statements.DelayUntilTime" "Microsoft.Activities.Extensions Version=1.8.4.630 Culture=neutral PublicKeyToken=23b0c89d0d5ad43f" "DelayUntilTime" "Test Category" "D:\wf.codeplex.com\src\wf\labs\Microsoft.Activities.Extensions\Microsoft.Activities.Extensions.NuGet\Resources\Activity.bmp"
# 