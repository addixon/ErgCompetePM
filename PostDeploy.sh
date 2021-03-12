sudo cp DLLs/PM3DDICP.dll /lib/PM3DDICP.dll
sudo cp DLLs/PM3CsafeCP.dll /lib/PM3CsafeCP.dll
sudo cp DLLs/PM3USBCP.dll /lib/PM3USBCP.dll
sudo cp Erg.service /etc/systemd/system/Erg.service
sudo systemctl daemon-reload
sudo systemctl start Erg
sudo chmod +x ErgCompetePM;