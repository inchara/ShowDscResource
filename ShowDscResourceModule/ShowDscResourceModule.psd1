

@{


ModuleVersion = '1.0'
GUID = 'c55f556c-fadf-4d64-ad24-32b0a1013191'

Author = 'Inchara Shivalingaiah'

Description = 'Powershell ISE AddOn to show Dsc Resources'

PowerShellVersion = '4.0'

CLRVersion = '4.0'

NestedModules = @("ShowDscResourceModule.psm1")

FunctionsToExport = @("Install-DscResourceAddOn")

}
