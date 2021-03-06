function Confirm-Admin
{
    # Get the ID and security principal of the current user account
    $myWindowsID = [System.Security.Principal.WindowsIdentity]::GetCurrent();
    $myWindowsPrincipal = New-Object System.Security.Principal.WindowsPrincipal($myWindowsID);

    # Get the security principal for the administrator role
    $adminRole = [System.Security.Principal.WindowsBuiltInRole]::Administrator;

    # Check to see if we are currently running as an administrator
    if ($myWindowsPrincipal.IsInRole($adminRole)) 
    {
        return 1;
    }
    return 0;
}

function Add-User
{
    param 
    (
        [Parameter(Mandatory=$True)][PSCustomObject]$user
    )

    begin {}
    process
    {
        if (Test-User -Username $user.username)
        {
            Write-Verbose "User '$($user.username)' already exists"
			Restrict-Permissions $($user.username)
        }
        else 
        {
            $secpasswd = ConvertTo-SecureString $user.password -AsPlainText -Force
            $mycreds = New-Object System.Management.Automation.PSCredential ($user.username, $secpasswd)
            Install-User -Credential $mycreds -Description $user.description -FullName $user.fullname
            Write-Verbose "User '$($user.username)' created"
			Restrict-Permissions $($user.username)
        }
        if (Test-GroupMember -GroupName $user.defaultGroupMembership -Member $user.username)
        {
            Write-Verbose "User '$($user.username)' already member of '$($user.defaultGroupMembership)'"
        }
        else 
        {
            Add-GroupMember -Name $user.defaultGroupMembership -Member $user.username
            Write-Verbose "User '$($user.username)' added to '$($user.defaultGroupMembership)' group"
        }

        return 0;
    }
    end {}
}

function Remove-User
{
    param 
    (
        [Parameter(Mandatory=$True)][string]$username
    )

    begin {}
    process
    {
        if (Test-User -Username $username)
        {
            Uninstall-User -Username $username
            Write-Verbose "User $username deleted"
        }
        else 
        {
            Write-Verbose "User $username does not exist"
        }
    }
    end {}
}

function Restrict-Permissions
{
	param
	(
		[Parameter(Mandatory=$True)][string]$username
	)
	begin {}
	process
	{
			Write-Verbose "Restricting rights for '$($username)"
			Grant-UserRight -Account "$username" -Right SeServiceLogonRight
			Grant-UserRight -Account "$username" -Right SeDenyRemoteInteractiveLogonRight
	}
	end {}
}

Export-ModuleMember Confirm-Admin, Add-User, Remove-User, Restrict-Permissions