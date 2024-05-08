$datap4k = "C:\Program Files\Roberts Space Industries\StarCitizen\EPTU\Data.p4k"
Write-Host "Extracting dcbs from Data.p4k"
unp4k $datap4k *.dcb
Write-Host "Extracting datafrom dcbs"
unforge .\Data\Game.dcb