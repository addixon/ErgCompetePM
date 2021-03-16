sudo systemctl stop Erg
sudo rm -f /etc/systemd/system/Erg.service
sudo cp Erg.service /etc/systemd/system/Erg.service
sudo update-rc.d Erg defaults
sudo update-rc.d Erg enable
sudo systemctl daemon-reload
sudo chmod +x ErgCompetePM;