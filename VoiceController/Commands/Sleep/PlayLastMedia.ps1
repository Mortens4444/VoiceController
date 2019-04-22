param([string] $mediafileParts)
$mediafile = "$($mediafileParts) " + $args -join " "

[void] [System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") 

function Set-DisplayOff([string] $file)
{
	$extension = [IO.Path]::GetExtension($file)
	$musicExtensions = ".wav", ".mp3", ".flac"
	if ($musicExtensions.Contains($extension))
	{
		$code = @"
using System;
using System.Runtime.InteropServices;
public class API
{
	[DllImport("user32.dll")]
	public static extern
	int SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
}
"@
		
		$t = Add-Type -TypeDefinition $code -PassThru
		$t::SendMessage(0xffff, 0x0112, 0xf170, 2)
	}
}

function PlayMediaAndWaitForEnd([string] $file)
{
	Set-DisplayOff($file)
	$objShell = New-Object -ComObject Shell.Application
	$path = Split-Path -Path $file
	$objFolder = $objShell.Namespace($path)
	$filename = [IO.Path]::GetFileName($file)
	$objFile = $objFolder.ParseName($filename)
	$lengthColumn = 27
	$length = $objFolder.GetDetailsOf($objFile, $lengthColumn)

	&'C:\Program Files (x86)\VideoLAN\VLC\vlc.exe' --qt-start-minimized --qt-notification=0 $file
	
	$hours, $minutes, $seconds = $length.Split(':')
	$duration = [int]$hours * 60 * 60 + [int]$minutes * 60 + [int]$seconds
	Start-Sleep -s $duration
}

function Suspend()
{
	$powerState = [System.Windows.Forms.PowerState]::Suspend;
	[System.Windows.Forms.Application]::SetSuspendState($powerState, $true, $false);
}

if (![System.IO.File]::Exists($mediafile))
{
	Write-Error "File does not exists: $($mediafile)"
	exit(1)
}

#PlayMediaAndWaitForEnd $mediafile

#Suspend