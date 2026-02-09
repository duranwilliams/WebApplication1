# Define the path to the text file containing the list of URLs
$urlListPath = "C:\tmp\pdf_list.txt"

# Define the destination folder for the downloaded PDFs
$destinationFolder = "C:\tmp\"

# Ensure the destination folder exists
if (-not (Test-Path $destinationFolder)) {
    Write-Host "Destination folder not found. Creating $destinationFolder"
    New-Item -Path $destinationFolder -ItemType Directory | Out-Null
}

# Read the URLs from the text file
$urls = Get-Content -Path $urlListPath

# Loop through each URL in the list
foreach ($url in $urls) {
    # Trim any leading/trailing whitespace from the URL
    $url = $url.Trim()

    # Skip empty lines
    if ([string]::IsNullOrEmpty($url)) {
        continue
    }

    try {
        # Extract the filename from the URL for saving locally
        $fileName = Split-Path -Path $url -Leaf
        $outputPath = Join-Path -Path $destinationFolder -ChildPath $fileName

        Write-Host "Downloading: $url to $outputPath"

        # Use Invoke-WebRequest to download the file
        # -Uri specifies the source URL
        # -OutFile specifies the local destination path
        Invoke-WebRequest -Uri $url -OutFile $outputPath

        Write-Host "Successfully downloaded: $fileName" -ForegroundColor Green
    }
    catch {
        Write-Host "Error downloading $url: $_" -ForegroundColor Red
    }
}

Write-Host "Download process finished."
