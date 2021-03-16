sudo systemctl stop Erg
sudo rm -f /etc/systemd/system/Erg.service
sudo cp Erg.service /etc/systemd/system/Erg.service
sudo systemctl enable Erg
sudo systemctl daemon-reload
sudo chmod +x ErgCompetePM;