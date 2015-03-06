<#
 # Function to Install the Show-DscResource Addon . 
 #>

function Install-DscResourceAddOn
{
   $AddonName = 'Show-DscResource'
   Write-Host  " Thank you for installing the add-on. Please wait a few seconds while ISE loads all the resources (It's a one time wait).Thanks!"
   $addonAlreadyExists = $false;
   Add-type -path "$PSScriptRoot/Show-DscResources.dll"
   foreach($verticalAddon in $psISE.CurrentPowerShellTab.VerticalAddOnTools)
   {
        if($verticalAddon.Name -eq $AddonName)
        {
            $verticalAddon.IsVisible=$true
            $addonAlreadyExists=$true
            break;
        }
   }
   if(!$addonAlreadyExists)
   {
        $psISE.CurrentPowerShellTab.VerticalAddOnTools.Add(‘Show-DscResource’, [Show_DscResources.DscResourceAddOn], $true)
   }
   Write-Host "Installed the add-on successfully! "
}